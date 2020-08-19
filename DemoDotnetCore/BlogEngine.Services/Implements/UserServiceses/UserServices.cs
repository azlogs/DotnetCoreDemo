using BlogEngine.DataModels.Models;
using BlogEngine.DataRepositories.Interfaces.IUserRepositories;
using BlogEngine.Services.IUserServiceses.Interfaces;
using BlogEngine.Utils;
using BlogEngine.ViewModels.UserViewmodels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Services.UserServices.Implements
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly SecuritySetting _securitySetting;
        public UserServices(IUserRepository userRepository, IOptions<SecuritySetting> securitySetting)
        {
            _userRepository = userRepository;
            _securitySetting = securitySetting.Value;
        }

        #region Login
        public async Task<UserLoginResultViewModel> Login(UserLoginViewModel userLogin)
        {
            // Check password
            var passwordEncrypt = StringCipher.Encrypt(userLogin.Password, _securitySetting.PasswordSalt);
            if (!await _userRepository.CheckUsernamePassword(userLogin.Username, passwordEncrypt))
            {
                return null;
            }

            var user = await _userRepository.GetUserByUsername(userLogin.Username);

            if (user == null)
            {
                return null;
            }

            // Generate Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_securitySetting.SecrectKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userResult = new UserLoginResultViewModel
            {
                Token = tokenHandler.WriteToken(token)
            };

            return userResult;
        }
        #endregion

        #region Register

        public async Task<UserViewModel> RegisterUser(RegisterUserViewModel userViewModel)
        {
            var passwordEncrypt = StringCipher.Encrypt(userViewModel.Password, _securitySetting.PasswordSalt);
            userViewModel.Password = passwordEncrypt;

            return await _userRepository.RegisterUser(userViewModel);
        }
        #endregion

        #region Update password
        public async Task<bool> UpdatePassword(string username, string password)
        {
            var user = await _userRepository.GetEntity().FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
            {
                return false;
            }

            var passwordEncrypt = Utils.StringCipher.Encrypt(password, _securitySetting.PasswordSalt);

            user.Password = passwordEncrypt;

            return await _userRepository.UpdateAsync(user);
        }
        #endregion
    }
}