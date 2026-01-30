
using DailyUpdates.Models;

namespace DailyUpdates.Services
{
    public interface IJwtService
    {
        TokenResponse GenerateTokens(User user);
        string GenerateRefreshToken();

}
    }
