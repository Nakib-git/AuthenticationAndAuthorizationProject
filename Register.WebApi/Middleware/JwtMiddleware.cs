using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Register.Application.Service.IContracts;
using Register.Domain.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Register.WebApi.Middleware {
    public class JwtMiddleware {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings) {
            _next = next;
            _appSettings = appSettings.Value;

        }

        public async Task Invoke(HttpContext context, IUserService userService) {

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                //Validate Token
                attachUserToContext(context, userService, token);
            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IUserService userService, string token) {
            try {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost",
                }, out SecurityToken validateToken);


                var jwtToken = (JwtSecurityToken)validateToken;
                var userId = int.Parse(jwtToken.Claims.FirstOrDefault(_ => _.Type == "id").Value);
                context.Items["User"] = userService.GetById(userId);

            } catch (Exception ex) {


            }
        }
    }
}
