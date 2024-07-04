using MorningIntegration.Models;

namespace MorningIntegration.Interface
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(User user);
    }
}
