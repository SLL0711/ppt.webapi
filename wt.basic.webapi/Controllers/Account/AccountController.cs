using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using System.DirectoryServices.AccountManagement;
using wt.basic.service.Common;
using wt.basic.service.User;

namespace wt.basic.webapi.Controllers.Account
{
    [Route("api/[controller]")]
    public class AccountController : ApiBaseController
    {
        private UsersService usersService = null;
        private wtJsonResult json;
        public wtConfig config { get; set; }
        public ILogger logger { get; set; }
        public AccountController(wtJsonResult jsonRet, IOptions<wtConfig> wtConfig, ILogger<AccountController> ilogger, UsersService usersService)
        {
            json = jsonRet;
            this.config = wtConfig.Value;
            this.logger = ilogger;
            this.usersService = usersService;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public void Login(string userName, string passWord)
        {
            //usersService.QueryUser(userName);

            json.success = true;
            try
            {
                using (var conn = new LdapConnection())
                {
                    conn.Connect("192.168.20.130", 389);
                    string newUserName = "wingtech\\" + userName;
                    conn.Bind(newUserName, passWord);

                    //存在用户
                    var token = generateToken(userName);
                    json.success = true;
                    json.obj = new
                    {
                        token
                    };
                    logger.LogInformation("登入成功");
                    usersService.QueryUser(userName);
                }
            }
            catch (System.Exception ex)
            {
                //用户不存在
                json.success = false;
                json.msg = $" 用户登入失败 Message:{ex.Message}";
                logger.LogError(json.msg);
                json.obj = new
                {
                    ret = "验证失败"
                };
            }

        }

        [Route("Auth")]
        public void AuthTest()
        {

            json.obj = new
            {
                name = userName
            };

            json.msg = "认证成功";
        }
    }
}
