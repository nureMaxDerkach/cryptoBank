using CryptoBank.Application.Services.ExchangeRateService;
using CryptoBank.Contracts.Requests;
using CryptoBank.Contracts.Responses;
using CryptoBank.Domain.Enums;
using CryptoBank.Domain.Interfaces;
using CryptoBank.Domain.Models;

namespace CryptoBank.Application.Services.TransactionService;

public class TransactionService(
    IUnitOfWork unitOfWork, 
    IWalletRepository walletRepository, 
    ICardRepository cardRepository,
    ITransactionRepository transactionRepository,
    IExchangeRateService exchangeRateService) : ITransactionService
{
    public async Task<TransactionResponse> ConvertAsync(long userId, ConvertRequest request)
    {
        if (request.Amount <= 0)
        {
            throw new Exception("Amount must be positive");
        }

        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var wallet = await walletRepository.GetByIdAsync(request.WalletId);

            if (wallet == null)
            {
                throw new Exception("Wallet not found");
            }

            var card = await cardRepository.GetByIdAsync(request.CardId);

            if (card is null)
            {
                throw new Exception("Card not found");
            }

            if (wallet.UserId != userId || card.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not own this wallet or card");
            }
            
            decimal fromAmount, toAmount;
            long fromCurrencyId, toCurrencyId;

            if (request.Direction == ConversionDirection.CRYPTO_TO_FIAT)
            {
                if (wallet.Balance < request.Amount)
                {
                    throw new Exception("Insufficient wallet balance");
                }

                fromAmount = request.Amount;
                fromCurrencyId = wallet.CurrencyId;
                toCurrencyId = card.CurrencyId;
                toAmount = await exchangeRateService.ConvertAsync(wallet.CurrencyId, card.CurrencyId, request.Amount);

                wallet.Balance -= fromAmount;
                card.Balance += toAmount; 
            }
            else
            {
                if (card.Balance < request.Amount)
                {
                    throw new Exception("Insufficient card balance");
                }

                fromAmount = request.Amount;
                fromCurrencyId = card.CurrencyId;
                toCurrencyId = wallet.CurrencyId;
                toAmount = await exchangeRateService.ConvertAsync(card.CurrencyId, wallet.CurrencyId, request.Amount);

                card.Balance -= fromAmount;
                wallet.Balance += toAmount;
            }
            
            var transactionRecord = new Transaction
            {
                UserId = userId,
                Type = TransactionType.CONVERT,
                Status = TransactionStatus.COMPLETED,
                FromCurrencyId = fromCurrencyId,
                FromAmount = fromAmount,
                ToCurrencyId = toCurrencyId,
                ToAmount = toAmount,
                RecipientUserId = null,
                CreatedAt = DateTime.UtcNow
            };

            await transactionRepository.AddAsync(transactionRecord);
            await unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();

            return MapToResponse(transactionRecord);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<TransactionResponse> SendMoneyAsync(long userId, SendMoneyRequest request)
    {
        if (request.Amount <= 0)
        {
            throw new Exception("Amount must be positive");
        }
        
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var fromCard = await cardRepository.GetByIdAsync(request.FromCardId);

            if (fromCard is null)
            {
                throw new Exception("Source card not found");
            }
            
            if (fromCard.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not own the source card");
            }

            var toCard = await cardRepository.GetByCardNumberAsync(request.RecipientCardNumber);

            if (toCard?.Id == fromCard.Id)
            {
                throw new Exception("Cannot send money to the same card");
            }
            
            if (toCard is not null && toCard.CurrencyId != fromCard.CurrencyId)
            {
                throw new Exception("Cards must be in the same currency for a direct transfer");
            }
            
            if (fromCard.Balance < request.Amount)
            {
                throw new Exception("Insufficient balance");
            }
            
            fromCard.Balance -= request.Amount;
            
            var isInternal = toCard is not null;

            if (isInternal)
            {
                toCard!.Balance += request.Amount;
            }

            var transactionRecord = new Transaction
            {
                UserId = userId,
                Type = TransactionType.SEND_MONEY,
                Status = TransactionStatus.COMPLETED,
                FromCurrencyId = fromCard.CurrencyId,
                FromAmount = request.Amount,
                ToCurrencyId = fromCard.CurrencyId,
                ToAmount = request.Amount,
                RecipientUserId = toCard?.UserId,
                RecipientReference = request.RecipientCardNumber,
                IsExternal = !isInternal,
                CreatedAt = DateTime.UtcNow

            };
            
            await transactionRepository.AddAsync(transactionRecord);
            await unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();

            return MapToResponse(transactionRecord);
        }
        catch 
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

  public async Task<TransactionResponse> SendCryptoAsync(long userId, SendCryptoRequest request)
{
    if (request.Amount <= 0)
    {
        throw new Exception("Amount must be positive");
    }

    if (string.IsNullOrWhiteSpace(request.RecipientAddress))
    {
        throw new Exception("Recipient address is required");
    }

    await using var transaction = await unitOfWork.BeginTransactionAsync();

    try
    {
        var fromWallet = await walletRepository.GetByIdAsync(request.FromWalletId)
            ?? throw new Exception("Source wallet not found");

        if (fromWallet.UserId != userId)
        {
            throw new UnauthorizedAccessException("You do not own the source wallet");
        }

        if (fromWallet.Balance < request.Amount)
        {
            throw new Exception("Insufficient balance");
        }

        var toWallet = await walletRepository.GetByAddressAsync(request.RecipientAddress);

        if (toWallet is not null && toWallet.Id == fromWallet.Id)
        {
            throw new Exception("Cannot send crypto to the same wallet");
        }

        if (toWallet is not null && toWallet.CurrencyId != fromWallet.CurrencyId)
        {
            throw new Exception("Wallets must be in the same currency for a direct transfer");
        }

        fromWallet.Balance -= request.Amount;

        var isInternal = toWallet is not null;

        if (isInternal)
        {
            toWallet!.Balance += request.Amount;
        }

        var transactionRecord = new Transaction
        {
            UserId = userId,
            Type = TransactionType.SEND_CRYPTO,
            Status = TransactionStatus.COMPLETED,
            FromCurrencyId = fromWallet.CurrencyId,
            FromAmount = request.Amount,
            ToCurrencyId = fromWallet.CurrencyId,
            ToAmount = request.Amount,
            RecipientUserId = toWallet?.UserId,
            RecipientReference = request.RecipientAddress,
            IsExternal = !isInternal,
            CreatedAt = DateTime.UtcNow
        };

        await transactionRepository.AddAsync(transactionRecord);
        await unitOfWork.SaveChangesAsync();

        await transaction.CommitAsync();

        return MapToResponse(transactionRecord);
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
}

    public async Task<List<TransactionResponse>> GetUserTransactionsAsync(long userId)
    {
        var transactions = await transactionRepository.GetAllByUserIdAsync(userId);
        return transactions.Select(MapToResponse).ToList();
    }
    
    private static TransactionResponse MapToResponse(Transaction t) => new(
        t.Id,
        t.Type,
        t.Status,
        t.FromCurrencyId,
        t.FromAmount,
        t.ToCurrencyId,
        t.ToAmount,
        t.IsExternal,
        t.RecipientReference,
        t.CreatedAt);
}