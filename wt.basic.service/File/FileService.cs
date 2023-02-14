using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using wt.basic.dataAccess.Repository.Type;
using wt.basic.dataAccess.Repository.Picture;
using wt.basic.dataAccess.Repository.Ppt;
using wt.basic.dataAccess.Repository.User;
using wt.basic.db.DBModels;
using wt.basic.service.Model;
using wt.basic.service.Ppt;
using wt.lib.Office;
using wt.basic.service.Common;
using Microsoft.Extensions.Options;

namespace wt.basic.service.File
{
    public class FileService
    {
        private readonly IPictureRepository pictureRepository;
        private readonly IUsersRepository userRepository;
        private readonly IPptRepository pptRepository;
        private readonly ITypeRepository typeRepository;
        private readonly ILogger logger;
        private readonly wtConfig config;

        public FileService(IPictureRepository pictureRepository, IUsersRepository userRepository,
            IPptRepository pptRepository, ITypeRepository typeRepository, ILogger<FileService> logger, IOptions<wtConfig> options)
        {
            this.pictureRepository = pictureRepository;
            this.userRepository = userRepository;
            this.pptRepository = pptRepository;
            this.typeRepository = typeRepository;
            this.logger = logger;
            this.config = options.Value;
        }


        /// <summary>
        /// 上传PPT
        /// </summary>
        /// <param name="typeId">类型ID</param>
        /// <param name="file">文件</param>
        /// <param name="UserName">用户名</param>
        /// <param name="tagIds">标签列表</param>
        /// <returns></returns>
        public async Task<PPtModel> AddPpt(IFormFile file, int typeId, string[] tagIds, string UserName)
        {
            string newFileName = Guid.NewGuid().ToString(), fileExt = fileExt = Path.GetExtension(file.FileName);
            string name = newFileName + fileExt;//t.pptx

            string pptName = Path.GetFileNameWithoutExtension(file.FileName);

            //1、存储ppt到指定路径
            string fileUploadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StaticFiles");
            if (!string.IsNullOrWhiteSpace(config.fileserver?.address))
                fileUploadPath = config.fileserver?.address;

            var direName = newFileName;//Path.GetFileName(name);
            string path = Path.Combine(fileUploadPath, direName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            logger.LogInformation("指定文件存储目录已存在...");

            string filePath = Path.Combine(path, name);
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var stream = file.OpenReadStream())
                {
                    await stream.CopyToAsync(fs);//文件存储成功
                    logger.LogInformation("文件存储成功");
                }
            }

            //2、解析ppt文件生成多个picture 
            List<string> imgNameList = PPtConvert.TransferPPT2ImgReturnList(filePath, path);
            logger.LogInformation("缩略图生成成功");

            var userObj = userRepository.RetrieveByAccountName(UserName);
            //var userObj = userRepository.RetrieveByAccountName("zhangsan");
            if (userObj == null)
                logger.LogError($"用户名：{UserName} 不存在");

            //var pptObj = await pptRepository.RetrieveById(pptId);

            //4、创建ppt记录并关联对应的tags
            var typeObj = await typeRepository.RetrieveById(typeId);
            if (typeObj == null)
                logger.LogError($"类型ID：{typeId} 不存在");

            Tb_ppt pptObj2 = new Tb_ppt();
            pptObj2.State = 1;
            pptObj2.CreatedBy = userObj;
            pptObj2.CreatedOn = DateTime.Now;
            pptObj2.ModifiedOn = DateTime.Now;
            pptObj2.ModifiedBy = userObj;
            pptObj2.Download_path = Path.Combine(direName, name);
            pptObj2.MinPicture = Path.Combine(direName, imgNameList[0]);
            pptObj2.Name = pptName;
            pptObj2.Page = imgNameList.Count;
            pptObj2.Size = file.Length.ToString();
            pptObj2.Type = typeObj;

            //5、维护tag、ppt 多对多映射关系
            for (int i = 0; i < tagIds.Length; i++)
            {
                var tagId = tagIds[i];
                pptObj2.Tag2.Add(new Tb_tagPPt()
                {
                    //PPTId = pptObj2.ID,
                    TagId = Convert.ToInt32(tagId),
                    CreateTime = DateTime.Now
                });
            }

            await pptRepository.Add(pptObj2);

            logger.LogInformation("PPT实体创建成功");

            //3、存储picture到指定路径并生成对应的picture记录信息
            imgNameList.ForEach(item =>
            {
                Tb_picture picObj = new Tb_picture();
                picObj.Name = pptName;
                picObj.State = 1;
                picObj.Path = Path.Combine(direName, item);
                picObj.CreatedOn = DateTime.Now;
                picObj.ModifiedOn = DateTime.Now;
                picObj.CreatedBy = userObj;
                picObj.ModifiedBy = userObj;
                picObj.Ppt = pptObj2;

                pictureRepository.Add(picObj);
            });

            logger.LogInformation("picture实体创建成功");

            PPtModel m = new PPtModel()
            {
                downLoadPath = pptObj2.Download_path,
                minPicDownPath = pptObj2.MinPicture
            };

            return m;
        }

        public async Task AddHeadshot(IFormFile picture, string userName)
        {
            string newPictureName = Guid.NewGuid().ToString(), pictureExt = pictureExt = Path.GetExtension(picture.FileName);
            string name = newPictureName + pictureExt;//t.png

            //1、存储heasdshot到指定路径
            string pictureUploadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StaticFiles/headshot/other");
            string absolutePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StaticFiles");
            logger.LogInformation($"项目的上传路径为：{pictureUploadPath} ");
            if (!string.IsNullOrWhiteSpace(config.fileserver?.address))
            {
                pictureUploadPath = config.fileserver?.address + "/headshot/other";
                logger.LogInformation($"filesever中address的上传路径为：{pictureUploadPath} ");
                absolutePath = config.fileserver?.address;
            }
            if (!Directory.Exists(pictureUploadPath))
                Directory.CreateDirectory(pictureUploadPath);
            logger.LogInformation($"指定文件存储目录{pictureUploadPath}已存在...");

            string picturePath = Path.Combine(pictureUploadPath, name);
            using (FileStream fs = new FileStream(picturePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var stream = picture.OpenReadStream())
                {
                    await stream.CopyToAsync(fs);//文件存储成功
                    logger.LogInformation("文件存储成功");
                }
            }

            //2、headshot关联对应的user
            var direName = "headshot/other";
            var userObj = userRepository.RetrieveByAccountName(userName);
            if (userObj == null)
                logger.LogError($"用户名：{userName} 不存在");

            string oldHeadshot = userObj.Headshot;
            if (!oldHeadshot.Contains("default_headshot"))
            {
                //例如headshot/other/d64c1dcd-1821-4e40-8119-d0250dc0e070.jpeg

                logger.LogInformation($"获取到绝对路径为：{absolutePath} ");
                string pictureSavePath = Path.Combine(absolutePath, oldHeadshot);
                logger.LogInformation($"删除路径为：{pictureSavePath}的原头像");
                System.IO.File.Delete(pictureSavePath);
            }

            userObj.Headshot = Path.Combine(direName, name);
            userObj.ModifiedOn = DateTime.Now;
            await userRepository.Update(userObj);
        }
    }
}
