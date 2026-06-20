using System.Security.Cryptography;

namespace CryptoBank.Application.Services.WalletService;

public static class WalletAddressGenerator
{
    public static string GenerateAddress()
    {
        var bytes = new byte[20];
        RandomNumberGenerator.Fill(bytes);
        return "0x" + Convert.ToHexString(bytes).ToLowerInvariant();
    }
}