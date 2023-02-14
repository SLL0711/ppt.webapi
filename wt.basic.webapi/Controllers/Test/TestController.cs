using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Novell.Directory.Ldap;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using wt.basic.service.Test;
using wt.basic.service.Common;
using wt.basic.db.Migrations;

namespace wt.basic.webapi.Controllers.Test
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ApiBaseController
    {
        private TestService testService = null;
        private wtJsonResult json = null;
        private readonly ILogger logger;

        public TestController(TestService testService, wtJsonResult json, ILogger<TestController> logger)
        {
            this.testService = testService;
            this.json = json;
            this.logger = logger;
        }

        [HttpGet]
        [Route("test")]
        [AllowAnonymous]
        public void test()
        {
            logger.LogInformation("test");
            //return "123";
            json.obj = new
            {
                value = "test"
            };
            json.success = true;
        }

        [HttpGet]
        [Route("test2")]
        public void test2()
        {
            //return "123";
            json.obj = new
            {
                value = "test2"
            };
            json.success = true;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public void TestLogin(string userid, string password)
        {

            var claim = Request.HttpContext.User.Identity;

            json.success = true;
            PrincipalContext adcontext = new PrincipalContext(ContextType.Domain, "192.168.20.130", userid, password);

            using (adcontext)
            {
                if (adcontext.ValidateCredentials(userid, password))
                {
                    //存在用户
                    json.obj = new
                    {
                        ret = "验证通过"
                    };
                }
                else
                {
                    //用户不存在
                    json.obj = new
                    {
                        ret = "验证失败"
                    };
                }
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("loginLdap")]
        public void TestLogin2(string userid, string password)
        {
            json.success = true;
            try
            {
                using (var conn = new LdapConnection())
                {
                    conn.Connect("192.168.20.130", 389);

                    userid = "wingtech\\" + userid;

                    conn.Bind(userid, password);

                    //存在用户
                    json.obj = new
                    {
                        ret = "验证通过"
                    };
                }
            }
            catch (System.Exception ex)
            {
                json.success = false;
                //用户不存在
                json.obj = new
                {
                    ret = "验证失败"
                };
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("AddTags")]
        public void AddTag()
        {
            json.success = true;
            testService.AddTags();
            json.msg = "添加成功";
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetTags")]
        public void GetTag()
        {
            json.success = true;
            json.list = testService.GetTags();
            json.msg = "获取成功";
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Deletetags")]
        public async Task DelTag(int ID)
        {
            json.success = true;
            await testService.DelTags(ID);
            json.msg = "删除成功";
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Updatetags")]
        public async Task UpdTag(int ID)
        {
            json.success = true;
            await testService.UpdateTags(ID);
            json.msg = "修改成功";
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("File")]
        public FileResult GetFile()
        {
            FileStream fs = new FileStream("\\\\Mac\\Home\\Documents\\project\\wt.basic.platform\\wt.basic.webapi\\Log\\202209\\2022-09-28.txt", FileMode.Open, FileAccess.Read);

            var ret = new FileStreamResult(fs, new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream"));

            ret.FileDownloadName = "1.txt";

            return ret;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("PostTest")]
        public void post(Model1 model1)
        {
            json.success = true;
            json.obj = model1;
            json.msg = "测试成功";
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("PostUrlencode")]
        public void post2(string name, string tall)
        {
            //string name = model1.Name, tall = model1.Tall;
            json.success = true;
            json.obj = new
            {
                name = name,
                tall = tall
            };
            json.msg = "测试成功";
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("TestGdip")]
        public void TestGdip(string path)
        {
            testService.TestGdipIssue(path);
            json.success = true;
            json.msg = "上传成功！";
        }

    }

    public class Model1
    {
        public string Name { get; set; }
        public string Tall { get; set; }
    }
}
