using Microsoft.AspNetCore.Identity;
using TodoApp.Models.Dtos;

namespace TodoApp.Contracts.Services
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto requestDto);
    }
}
