namespace CryptoBank.Application.Services.CardService;

public static class CardNumberGenerator
{
    private const string Bin = "400000"; 

    public static string GenerateCardNumber()
    {
        var digits = new int[16];

        for (var i = 0; i < 6; i++)
            digits[i] = Bin[i] - '0';

        for (var i = 6; i < 15; i++)
            digits[i] = Random.Shared.Next(0, 10);

        digits[15] = CalculateLuhnCheckDigit(digits);

        return string.Concat(digits);
    }
    
    public static string GenerateCvv()
    {
        return Random.Shared.Next(0, 1000).ToString("D3");
    }
    
    private static int CalculateLuhnCheckDigit(int[] digits)
    {
        var sum = 0;

        for (var i = 0; i < 15; i++)
        {
            var digit = digits[i];

            if (i % 2 == 0)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
        }

        return (10 - (sum % 10)) % 10;
    }
}