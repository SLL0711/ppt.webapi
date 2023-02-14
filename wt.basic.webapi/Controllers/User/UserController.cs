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
using wt.basic.service.User;
using wt.basic.service.Common;
using wt.basic.service.File;
using static System.Net.WebRequestMethods;
using System;
using Microsoft.Extensions.Options;
using System.Drawing.Text;

namespace wt.basic.webapi.Controllers.User
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        private UsersService usersService = null;
        private wtJsonResult json = null;
        private readonly wtConfig config;
        private readonly ILogger logger;
        private readonly FileService fileService;

        public UserController(UsersService usersService, wtJsonResult json, ILogger<UserController> logger, IOptions<wtConfig> options, FileService fileService)
        {
            this.usersService = usersService;
            this.json = json;
            this.config = options.Value;
            this.logger = logger;
            this.fileService = fileService;
        }

        /// <summary>
        /// 点击收藏
        /// </summary>
        /// <param name="pptID">操作的pptID</param>
        [Authorize]
        [HttpPost]
        [Route("favorite")]
        public void ClickFavr(int pptID)
        {
            json.success = true;
            usersService.FavrAction(userName, pptID);
            json.msg = "收藏操作执行成功";
            logger.LogInformation("收藏操作执行成功");
        }

        /// <summary>
        /// 点击下载
        /// </summary>
        /// <param name="pptID">操作的pptID</param>
        [Authorize]
        [HttpPost]
        [Route("download")]
        public void ClickDown(int pptID)
        {
            json.success = true;
            usersService.DownAction(userName, pptID);
            json.msg = "下载操作执行成功";
            logger.LogInformation("下载操作执行成功");
        }

        /// <summary>
        /// 获取我的收藏信息
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示</param>
        /// <param name="searchKey">关键字搜索</param>
        [Authorize]
        [HttpGet]
        [Route("myFavorite")]
        public void myFavorite(int pageIndex, int pageSize, string searchKey)
        {
            json.success = true;
            int total = 0;
            json.list = usersService.MyFavorite(userName, pageIndex, pageSize, searchKey, ref total);
            json.msg = "我的收藏查询成功";
            json.obj = new { Total = total };
            logger.LogInformation("我的收藏查询成功");
        }

        /// <summary>
        /// 获取我的下载信息
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示</param>
        /// <param name="searchKey">关键字搜索</param>
        [Authorize]
        [HttpGet]
        [Route("myDownload")]
        public void myDownload(int pageIndex, int pageSize, string searchKey)
        {
            json.success = true;
            int total = 0;
            json.list = usersService.MyDownload(userName, pageIndex, pageSize, searchKey, ref total);
            json.msg = "我的下载查询成功";
            json.obj = new { Total = total };
            logger.LogInformation("我的下载查询成功");
        }

        /// <summary>
        /// 获取账户资料信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("showAccountInfo")]
        public async Task ShowAccountInfo()
        {
            json.success = true;
            json.obj = usersService.QueryByAccountName(userName);
            json.msg = "账户资料查询成功";
            logger.LogInformation("账户资料查询成功");
        }

        /// <summary>
        /// 修改用户账号信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="picture">用户头像文件</param>
        /// <param name="name">用户昵称</param>
        /// <param name="telephone">用户电话号码</param>
        /// <returns></returns>
        //[Authorize]
        [Authorize]
        [HttpPost]
        [Route("updateAccountInfo111")]
        public async Task UpdateAccountInfo111(IFormCollection formCollection)
        {
            var files = formCollection.Files;
            var userName = formCollection["userName"];//1
            var name = formCollection["name"];//1|2|3
            var telephone = formCollection["telephone"];//1
            #region 数据有效性判断
            if (files == null)
            {
                json.success = false;
                json.msg = "请选择头像进行上传";
                return;
            }
            if (files.Count > 1)
            {
                json.success = false;
                json.msg = "仅支持单附件上传";
                return;
            }
            var picture = files[0];
            string pictureExt = Path.GetExtension(picture.FileName);
            //判断后缀是否是图片
            if (pictureExt != ".gif" && pictureExt != ".jpg" && pictureExt != ".jpeg" && pictureExt != ".png")
            {
                json.success = false;
                json.msg = "图片类型不符合要求";
                return;
            }

            //文件大小限制2MB
            if (picture.Length > config.headshotMaxLeng * 1024 * 1024)
            {
                json.success = false;
                json.msg = $"文件大小超出最大{config.headshotMaxLeng}MB限制";
                return;
            }
            #endregion
            await fileService.AddHeadshot(picture, userName);
            usersService.UpdateUser111(userName, name, telephone);
            json.success = true;
            json.msg = "用户账号信息修改成功";
            logger.LogInformation("用户账号信息修改成功");
        }

        /// <summary>
        /// 更换昵称
        /// </summary>
        /// <param name="name">用户昵称</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("updateUserName")]
        public void updateUserName(string name)
        {
            json.success = true;
            usersService.UpdateUserName(userName, name);
            json.msg = "用户昵称修改成功";
            logger.LogInformation("用户昵称修改成功");
        }

        /// <summary>
        /// 获取我的上传
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示</param>
        /// <param name="searchKey">关键字搜索</param>
        [Authorize]
        [HttpGet]
        [Route("myUpload")]
        public void myUpload(int pageIndex, int pageSize, string searchKey)
        {
            json.success = true;
            int total = 0;
            json.list = usersService.MyUpload(userName, pageIndex, pageSize, searchKey, ref total);
            json.msg = "我的上传查询成功";
            json.obj = new { Total = total };
            logger.LogInformation("我的上传查询成功");
        }

        /// <summary>
        /// 提交issue或advice
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("advice")]
        public void AddAdvice(string advice,int type)
        {
            if (string.IsNullOrWhiteSpace(advice)) {
                json.success = false;
                json.msg = "建议不可为空！！";
                logger.LogInformation("建议不可为空！！");
                return;
            }
            json.success = true;
            usersService.AddAdvice(userName,advice,type);
            json.msg = "建议提交成功";
            logger.LogInformation("建议提交成功");
        }
    }
}