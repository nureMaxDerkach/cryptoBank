namespace CryptoBank.Contracts.Requests;

public class RegisterRequest
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public required string Email { get; set; }
    
    public long CountryId { get; set; }

    public required string Password { get; set; }
    
    public required string ConfirmPassword { get; set; }
}