namespace CryptoBank.Contracts.Responses;

public record UserDetailsResponse(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    string CountryName,
    DateTime CreatedAt);