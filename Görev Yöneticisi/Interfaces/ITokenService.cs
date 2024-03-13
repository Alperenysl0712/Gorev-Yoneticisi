using Görev_Yöneticisi.Models;

namespace Görev_Yöneticisi.Interfaces
{
    public interface ITokenService
    {
        Task<string> generateJWTTokens(string userName);
    }
}
