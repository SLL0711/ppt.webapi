using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using wt.basic.service.File;
using wt.basic.service.Common;
using Microsoft.Extensions.Logging;

namespace wt.basic.webapi.Controllers.File
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileUploadController : ApiBaseController
    {
        private readonly wtJsonResult json;
        private readonly wtConfig config;
        private readonly FileService fileService;
        private readonly ILogger logger;
        public FileUploadController(wtJsonResult wtJson, IOptions<wtConfig> options, FileService fileService, ILogger<FileService> logger)
        {
            this.json = wtJson;
            this.config = options.Value;
            this.fileService = fileService;
            this.logger = logger;
        }

        /// <summary>
        /// PPT附件上传
        /// </summary>
        /// <param name="formCollection">files:Stream|typeID:int|tags:Array</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Upload")]
        public async Task PPtUpload(IFormCollection formCollection)
        {
            logger.LogInformation($"{userName}执行ppt上传动作！");

            var files = formCollection.Files;
            //var pptName = formCollection["name"];
            var typeID = formCollection["typeID"];//1
            var tags = formCollection["tags"];//1|2|3

            #region 数据有效性判断

            string tageIds = tags;

            if (string.IsNullOrWhiteSpace(tageIds))
            {
                json.success = false;
                json.msg = "至少选择一个标签！！！";
                return;
            }

            string[] tagIds2 = tageIds.Split('|');
            if (tagIds2.Length < 2)
            {
                json.success = false;
                json.msg = "至少选择两个标签！！！";
                return;
            }
            else if (tagIds2.Length > 4)
            {
                json.success = false;
                json.msg = "不能超过四个标签！！！";
                return;
            }

            if (files == null || files.Count == 0)
            {
                json.success = false;
                json.msg = "请选择文件进行上传";
                return;
            }

            if (string.IsNullOrWhiteSpace(typeID))
            {
                json.success = false;
                json.msg = "请选择ppt所属类别";
                return;
            }

            if (files.Count > 1)
            {
                json.success = false;
                json.msg = "仅支持单附件上传";
                return;
            }

            var file = files[0];
            //var pptName = file.FileName;
            string fileExt = Path.GetExtension(file.FileName).ToLower();

            //文件大小限制50MB
            if (file.Length > config.pptMaxLeng * 1024 * 1024)
            {
                json.success = false;
                json.msg = $"文件大小超出最大{config.pptMaxLeng}MB限制";
                return;
            }

            if (fileExt != ".ppt" && fileExt != ".pptx")
            {
                json.success = false;
                json.msg = "文件类型不符合要求";
                return;
            }

            #endregion

            logger.LogInformation($"{userName}执行ppt上传动作，有效性判断PASS！");

            await fileService.AddPpt(file, Convert.ToInt32(typeID), tagIds2, userName);

            logger.LogInformation($"{userName}执行ppt上传动作，上传成功！");

            //json.obj = await fileService.AddPpt(Convert.ToInt32(typeID), file, "zhangsan", tags);
            json.msg = "上传成功";
        }

        /// <summary>
        /// 用户头像修改接口
        /// </summary>
        /// <param name="formCollection">用户头像文件</param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateHeadshot")]
        public async Task UpdateHeadshot(IFormCollection formCollection)
        {
            var files = formCollection.Files;
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
            string pictureExt = Path.GetExtension(picture.FileName).ToLower();
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
            json.success = true;
            json.msg = "用户头像修改成功";
        }
    }
}

