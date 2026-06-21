using CryptoBank.Contracts.Responses;
using CryptoBank.Domain.Interfaces;

namespace CryptoBank.Application.Services.UserService;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserDetailsResponse> GetMyDetailsAsync(long id)
    {
        var user = await userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        return new UserDetailsResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.DateOfBirth,
            user.Country!.Name,
            user.CreatedAt);
    }

    public async Task<List<UserSummaryResponse>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllAsync();

        return users.Select(u => new UserSummaryResponse(
            u.Id,
            u.FirstName,
            u.LastName,
            u.Country!.Name)).ToList();
    }
}