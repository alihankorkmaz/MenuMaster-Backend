using MenuMaster.Models;

namespace MenuMaster.Services
{
    public interface ITokenService
    {
        string GenerateToken(Restaurant restaurant);
        string GenerateToken(User user);
    }
}
