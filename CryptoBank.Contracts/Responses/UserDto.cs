namespace CryptoBank.Contracts.Responses;

public sealed record UserDto(long Id, string FirstName, string LastName, string Email);