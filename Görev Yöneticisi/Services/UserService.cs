using Görev_Yöneticisi.Interfaces;
using Görev_Yöneticisi.Models;

namespace Görev_Yöneticisi.Services
{
    public class UserService : IUserService
    {
        readonly IRepository<User> _userRepository;

        readonly ITokenService _tokenService;

        public UserService(IRepository<User> userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenService = tokenService;
        }

        public async Task<string> registerUser(string username, string password, string passwordA)
        {
            if (username.Trim() == null || password.Trim() == null || passwordA.Trim() == null)
            {
                throw new ArgumentException("All data is necessary for register.");
            }

            if (!password.Equals(passwordA))
            {
                throw new ArgumentException("Passwords are not equal.\nPlease check and try again.");
            }

            User newUser = new User
            {
                Username = username,
                Password = password
            };

            _userRepository.Add(newUser);

            return "New user added successfully.";
        }

        public async Task<UserTokenInfo> loginUser(string username, string password)
        {
            if (username.Trim() == null || password.Trim() == null)
            {
                throw new ArgumentException("All data is necessary for login.");
            }

            User user = _userRepository.Get(k => k.Username.Equals(username) && k.Password.Equals(password));

            if (user == null)
            {
                throw new Exception("There is no user with this username and password.");
            }

            UserTokenInfo userTokenInfo = new UserTokenInfo
            {
                userId = user.Id,
                Username = username,
                Token = await _tokenService.generateJWTTokens(username),
                loginDate = DateTime.Now
            };

            return userTokenInfo;
        }
    }
}
