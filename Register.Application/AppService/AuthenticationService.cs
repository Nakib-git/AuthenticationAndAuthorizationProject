
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Register.Application.Service.IContracts;
using Register.Domain.Helper;
using Register.Domain.Models;
using Register.Infrastructure.AppContext;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Register.Application.AppService {
    public class AuthenticationService : IAuthenticationService {
        public readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        public AuthenticationService(ApplicationDbContext context, IOptions<AppSettings> appSettings) {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(Login model) {
            var user = _context.Users.Include(c => c.Role).FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (user == null) return null;
            var token = generateToken(user);
            return new AuthenticateResponse() { Token = token };

        }

        private string generateToken(User user) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));
            var credetial = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>(){
                    new Claim("id",Convert.ToString(user.Id)),
                    new Claim(JwtRegisteredClaimNames.Sub, user?.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user?.Email),
                    new Claim("role", Convert.ToString(user.Role?.Name??string.Empty)),
                    new Claim("userName", Convert.ToString(user?.UserName)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };


            //foreach (var role in user.Role) {

            //    claims.Add(new Claim("Role", Convert.ToString(role)));
            //}

            var token = new JwtSecurityToken("localhost", "localhost", claims, expires: DateTime.Now.ToLocalTime().AddDays(7), signingCredentials: credetial);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
