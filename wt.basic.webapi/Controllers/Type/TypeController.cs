using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Novell.Directory.Ldap;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using wt.basic.service.Type;
using wt.basic.service.Common;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace wt.basic.webapi.Controllers.Home
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TypeController : ApiBaseController
    {
        private TypeService typeService = null;
        private wtJsonResult json = null;
        private readonly ILogger logger;

        public TypeController(TypeService typeService, wtJsonResult json, ILogger<TypeController> logger)
        {
            this.typeService = typeService;
            this.json = json;
            this.logger = logger;
        }

        /// <summary>
        /// 查询所有类型
        /// </summary>
        [HttpGet]
        [Route("typeShow")]
        public void TypeShow()
        {
            json.success = true;
            json.list = typeService.QueryAll();
            json.msg = "type类型查询成功";
            logger.LogInformation("type类型查询成功");
        }

        /// <summary>
        /// 增加类型
        /// </summary>
        /// <param name="name">类型名称</param>
        [HttpGet]
        [Route("typeAdd")]
        public void TypeAdd(string name)
        {
            json.success = true;
            typeService.AddType(name, userName);
            json.msg = "type添加成功";
            logger.LogInformation("type类型添加成功");
        }

        /// <summary>
        /// 删除类型
        /// </summary>
        /// <param name="id">类型ID</param>
        [HttpGet]
        [Route("typeDelete")]
        public void TypeDelete(int id)
        {
            json.success = true;
            typeService.DeleteType(id);
            json.msg = "type删除成功";
            logger.LogInformation("type删除成功");
        }

    }
}

