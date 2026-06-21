using CryptoBank.Contracts.Responses;

namespace CryptoBank.Application.Services.UserService;

public interface IUserService
{
    Task<UserDetailsResponse> GetMyDetailsAsync(long id);
    Task<List<UserSummaryResponse>> GetAllUsersAsync();
}