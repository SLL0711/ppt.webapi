using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using wt.basic.service.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace wt.basic.webapi.Controllers
{
    [Authorize]
    public class ApiBaseController : ControllerBase
    {

        //private readonly static string SECRETKEY = "wingtechSasd@!23d421s";//至少16为

        /// <summary>
        /// 登入用户名
        /// </summary>
        protected string userName
        {
            get
            {
                return Request?.HttpContext?.User?.Identity?.Name;
            }
        }

        /// <summary>
        /// 用户是否认证
        /// </summary>
        protected bool? isAuthentication
        {
            get
            {
                return Request?.HttpContext?.User?.Identity?.IsAuthenticated;
            }
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>token</returns>
        public string generateToken(string userName)
        {
            var config = ((IOptions<wtConfig>)Request.HttpContext.RequestServices.GetRequiredService(typeof(IOptions<wtConfig>))).Value;

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.token.SecretKey));//对称加密Key

            //一组Claims
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,userName),
                //new Claim(JwtRegisteredClaimNames.Email,email)
            };

            var Token = new JwtSecurityToken(
                issuer: config.token.Issuer,
                audience: config.token.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(config.token.TimeExpiresHour),
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(Token);

            return jwtToken;
        }
    }
}
