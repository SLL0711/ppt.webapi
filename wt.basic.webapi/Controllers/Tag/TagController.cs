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
using wt.basic.service.Tag;
using wt.basic.service.Common;
using wt.basic.service.Type;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace wt.basic.webapi.Controllers.Tag
{

    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TagController : ApiBaseController
    {
        private TagService tagService = null;
        private wtJsonResult json = null;
        private readonly ILogger logger;

        public TagController(TagService tagService, wtJsonResult json, ILogger<TagController> logger)
        {
            this.tagService = tagService;
            this.json = json;
            this.logger = logger;
        }

        /// <summary>
        /// 查询所有标签
        /// </summary>
        [HttpGet]
        [Route("tagShow")]
        public void TagShow()
        {
            json.success = true;
            json.list = tagService.QueryAll();
            json.msg = "所有tag查询成功";
            logger.LogInformation("tag查询成功");
        }

        /// <summary>
        /// 增加标签
        /// </summary>
        /// <param name="name">标签名称</param>
        [HttpGet]
        [Route("tagAdd")]
        public void TypeAdd(string name)
        {
            json.success = true;
            tagService.AddTag(name, userName);
            json.msg = "tag添加成功";
            logger.LogInformation("tag标签查询成功");
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id">标签ID</param>
        [HttpGet]
        [Route("typeDelete")]
        public void TypeDelete(int id)
        {
            json.success = true;
            tagService.DeleteTag(id);
            json.msg = "tag删除成功";
            logger.LogInformation("tag标签删除成功");
        }
    }
}

