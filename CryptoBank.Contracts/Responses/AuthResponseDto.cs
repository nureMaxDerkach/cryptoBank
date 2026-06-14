namespace CryptoBank.Contracts.Responses;

public sealed record AuthResponseDto(string AccessToken, UserDto User);