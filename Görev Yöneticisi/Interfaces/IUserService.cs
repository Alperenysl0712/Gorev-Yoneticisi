using Görev_Yöneticisi.Models;

namespace Görev_Yöneticisi.Interfaces
{
    public interface IUserService
    {
        Task<string> registerUser(string username, string password, string passwordA);

        Task<UserTokenInfo> loginUser(string username, string password);
    }
}
