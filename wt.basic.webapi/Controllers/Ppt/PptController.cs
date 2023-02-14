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
using wt.basic.service.Type;
using wt.basic.service.Ppt;
using wt.basic.service.Common;

namespace wt.basic.webapi.Controllers.Ppt
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PptController : ApiBaseController
    {
        private PptService pptService = null;
        private wtJsonResult json = null;
        private readonly ILogger logger;

        public PptController(PptService pptService, wtJsonResult json, ILogger<PptController> logger)
        {
            this.pptService = pptService;
            this.json = json;
            this.logger = logger;
        }

        /// <summary>
        /// 获取ppt详情
        /// </summary>
        /// <param name="id">ppt的id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("showPpt")]
        public async Task ShowPpt(int id)
        {
            json.success = true;
            json.obj = pptService.GetPpt(userName, id);
            json.msg = "PPT详情获取成功";
            logger.LogInformation("PPT详情获取成功");
        }

        /// <summary>
        /// 获取PPT的下载路径
        /// </summary>
        /// <param name="id">PPT的ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("downloadPpt")]
        public async Task DownloadPpt(int id)
        {
            json.success = true;
            json.list = pptService.GetPPtPath(id);
            json.msg = "PPT下载成功";
        }

        /// <summary>
        /// 查询type对应的ppt
        /// </summary>
        /// <param name="id">type的ID</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页展示</param>
        /// <param name="searchKey">搜索关键字</param>
        [HttpGet]
        [Route("typePptShow")]
        public void TypePptShow(int id, int pageIndex, int pageSize, string searchKey)
        {
            json.success = true;
            int total = 0;
            json.list = pptService.GetTypePpt(userName, id, pageIndex, pageSize, searchKey, ref total);
            json.msg = "type对应ppt内容查询成功";
            json.obj = new { Total = total };
            logger.LogInformation("type对应ppt内容查询成功");
        }

        /// <summary>
        /// 推荐ppt的展示
        /// </summary>
        [HttpGet]
        [Route("recommendShow")]
        public void recommendShow()
        {
            json.success = true;
            json.list = pptService.GetRecommendPpt(userName);
            json.msg = "推荐的ppt内容查询成功";
            logger.LogInformation("推荐的ppt内容查询成功");
        }

        /// <summary>
        /// 热门下载ppt展示
        /// </summary>
        [HttpGet]
        [Route("hotPptShow")]
        public void hotPptShow()
        {
            json.success = true;
            json.list = pptService.GetHotPpt();
            json.msg = "热门下载的ppt内容查询成功";
            logger.LogInformation("热门下载的ppt内容查询成功");
        }

        /// <summary>
        /// 最新上线ppt展示
        /// </summary>
        [HttpGet]
        [Route("newPptShow")]
        public void newPptShow()
        {
            json.success = true;
            json.list = pptService.GetNewPpt();
            json.msg = "最新上线ppt内容查询成功";
            logger.LogInformation("最新上线ppt内容查询成功");
        }

        /// <summary>
        /// 关键字查询最新ppt
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页展示</param>
        /// <param name="searchKey">搜索关键字</param>
        [HttpGet]
        [Route("searchNewPpt")]
        public void searchNewPpt(int pageIndex, int pageSize, string searchKey)
        {
            if (string.IsNullOrWhiteSpace(searchKey)) {
                json.success = false;
                json.msg = "关键字不可为空";
                logger.LogInformation("关键字为空，查询失败");
                return;
            }
            json.success = true;
            int total = 0;
            json.list = pptService.SearchNewPpt(userName, pageIndex, pageSize, searchKey, ref total);
            json.msg = "关键字相关的最新ppt内容查询成功";
            json.obj = new { Total = total };
            logger.LogInformation("关键字相关的最新ppt内容查询成功");
        }


        /// <summary>
        /// 关键字查询最热ppt
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页展示</param>
        /// <param name="searchKey">搜索关键字</param>
        [HttpGet]
        [Route("searchHotPpt")]
        public void searchHotPpt(int pageIndex, int pageSize, string searchKey)
        {
            if (string.IsNullOrWhiteSpace(searchKey))
            {
                json.success = false;
                json.msg = "关键字不可为空";
                logger.LogInformation("关键字为空，查询失败");
                return;
            }
            json.success = true;
            int total = 0;
            json.list = pptService.SearchHotPpt(userName, pageIndex, pageSize, searchKey, ref total);
            json.msg = "关键字相关的最热ppt内容查询成功";
            json.obj = new { Total = total };
            logger.LogInformation("关键字相关的最热ppt内容查询成功");
        }
    }
}
