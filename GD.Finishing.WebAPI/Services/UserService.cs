using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GD.Finishing.WebAPI.Services
{
    public interface IUserService
    {
        Task<ResultModel<UserModel>> AuthenticateAsync(string userName, string password);
    }
    public class UserService : IUserService
    {
        readonly AppSettings appSettings;
        FinishingSystemFactory finishingSystemFactory;
        public UserService(IOptions<AppSettings> options)
        {
            this.appSettings = options.Value;
            finishingSystemFactory = new FinishingSystemFactory(this.appSettings.ConnectionString);
        }

        public async Task<ResultModel<UserModel>> AuthenticateAsync(string userName, string password)
        {
            ResultModel<UserModel> resultModel = new ResultModel<UserModel>();
            var userResult = await finishingSystemFactory.Users.CheckLoginUser(userName, password);

            if (userResult == null || userResult.Data == null)
            {
                resultModel.Message = "El usuario no existe";
                resultModel.ResultType = -1;

                return resultModel;
            }

            var user = (User)userResult.Data;
            UserModel userModel = new UserModel(user);

            if (!user.PasswordControl(password))
            {
                resultModel.Message = "La contraseña no es correcta";
                resultModel.ResultType = 0;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userModel.UserID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            userModel.Token = tokenHandler.WriteToken(token);

            resultModel.Data = userModel;
            resultModel.IsSuccess = userModel.IsActive;
            resultModel.Message = userModel.IsActive ? "El usuario puede iniciar sesión" : "El usuario está desactivado";
            resultModel.ResultType = userModel.IsActive ? 1 : 0;

            return resultModel;
        }
    }
}
