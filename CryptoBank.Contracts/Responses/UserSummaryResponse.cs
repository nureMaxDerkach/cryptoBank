namespace CryptoBank.Contracts.Responses;

public record UserSummaryResponse(
    long Id,
    string FirstName,
    string LastName,
    string CountryName);