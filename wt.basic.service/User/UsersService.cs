using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt.basic.dataAccess.Repository.Favourites;
using wt.basic.dataAccess.Repository.User;
using wt.basic.dataAccess.Repository.Ppt;
using wt.basic.dataAccess.Repository.Advice;
using wt.basic.db.DBModels;
using wt.basic.service.Model;
using wt.basic.dataAccess.Repository.Tag;
using wt.basic.dataAccess.Repository.HistoryDownload;
using wt.basic.service.Common;
using Microsoft.Extensions.Options;

namespace wt.basic.service.User
{
    public class UsersService
    {
        private IUsersRepository repository = null;
        private IFavrRepository favrRepository = null;
        private IPptRepository pptRepository = null;
        private ITagRepository tagRepository = null;
        private ITagPptRepository tagPptRepository = null;
        private IHisDownRepository hisDownRepository = null;
        private IAdviceRepository adviceRepository = null;
        private readonly wtConfig config;
        public UsersService(IUsersRepository repository, IFavrRepository favrRepository, IPptRepository pptRepository, ITagPptRepository tagPptRepository, ITagRepository tagRepository, IHisDownRepository hisDownRepository,IAdviceRepository adviceRepository, IOptions<wtConfig> options)
        {
            this.repository = repository;
            this.favrRepository = favrRepository;
            this.pptRepository = pptRepository;
            this.tagPptRepository = tagPptRepository;
            this.tagRepository = tagRepository;
            this.hisDownRepository = hisDownRepository;
            this.adviceRepository = adviceRepository;
            this.config = options.Value;
        }

        /// <summary>
        /// 通过用户名查询user表
        /// </summary>
        /// <param name="userName"></param>
        public void QueryUser(string userName)
        {
            //通过名字去users表进行查询，如果有记录则说明用户已存在
            //如果没有记录则需要添加记录，并将Headshot设置为默认值
            if (repository.RetrieveAll().Where(t => t.AccountName == userName).FirstOrDefault() == null)
            {
                var createUser = new Tb_users();
                createUser.Headshot = "headshot/default_headshot.png";
                createUser.Name = userName;
                createUser.AccountName = userName;
                createUser.Password = null;
                createUser.Telephone = null;
                createUser.State = 1;
                createUser.CreatedOn = DateTime.Now;
                createUser.ModifiedOn = DateTime.Now;
                repository.Add(createUser);
            }
        }

        /// <summary>
        /// 对当前登陆用户信息进行修改
        /// </summary>
        /// <param name="userName"></param>
        public void UpdateUser111(string userName, string name, string telephone)
        {
            //通过名字去users表进行查询user记录
            var user = repository.RetrieveByAccountName(userName);
            user.Name = name;
            user.Telephone = telephone;
            user.ModifiedOn = DateTime.Now;
            repository.Update(user);
        }

        /// <summary>
        /// 对当前登陆用户的昵称进行修改
        /// </summary>
        /// <param name="name">用户昵称</param>
        public void UpdateUserName(string userName, string name)
        {
            //通过名字去users表进行查询user记录
            var user = repository.RetrieveByAccountName(userName);
            user.Name = name;
            user.ModifiedOn = DateTime.Now;
            repository.Update(user);
        }

        /// <summary>
        /// 通过userName获取user对象
        /// </summary>
        /// <param name="userName">用户名字</param>
        /// <returns></returns>
        public Tb_users QueryByAccountName(string userName)
        {
            var user = repository.RetrieveByAccountName(userName);
            return user;
        }

        /// <summary>
        /// 点击收藏
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="pptID">pptID</param>
        public void FavrAction(string userName, int pptID)
        {
            //通过userName获取用户ID
            int userID = repository.RetrieveByAccountName(userName).ID;
            //点击收藏后，查询favr表是否有对应数据userID+pptID
            var favr = favrRepository.RetrieveByUserAndPpt(userID, pptID);
            if (favr != null)
            //有：之前收藏过，此时要取消收藏
            {
                favrRepository.Delete(favr);
            }
            else
            //无：之前没有收藏过，此时需要添加收藏
            {
                var favr1 = new Tb_userPPt_Favr();
                favr1.UserId = userID;
                favr1.PPTId = pptID;
                favr1.CreateTime = DateTime.Now;
                favrRepository.Add(favr1);
            }
        }

        /// <summary>
        /// 点击下载
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="pptID">pptID</param>
        public void DownAction(string userName, int pptID)
        {
            //通过userName获取用户ID
            int userID = repository.RetrieveByAccountName(userName).ID;
            //点击下载后，查询down表是否有对应数据userID+pptID
            var down = hisDownRepository.RetrieveByUserAndPpt(userID, pptID);
            if (down == null)
            //无：之前没有下载过，此时需要添加下载记录
            {
                var down1 = new Tb_userPPt_Down();
                down1.UserId = userID;
                down1.PPTId = pptID;
                down1.CreateTime = DateTime.Now;
                hisDownRepository.Add(down1);
            }
        }

        /// <summary>
        /// 通过ppt的ID查询ppt的标签，以string类型展示
        /// </summary>
        /// <param name="pptID">ppt的ID</param>
        public List<string> GetPptTags(int pptID)
        {
            //获取ppt所有的tag记录
            List<Tb_tagPPt> tagPPts = tagPptRepository.RetrieveByPpt(pptID);
            //根据tag记录得到tag的ID，获取tag，再以string类型存储tag的name
            int tagID = 0;
            string tagName;
            List<string> tagNameList = new List<string>();
            for (int i = 0; i < tagPPts.Count(); i++)
            {
                //获取到tag的ID
                tagID = tagPPts[i].TagId;
                tagName = tagRepository.RetrieveById(tagID).Result.Name;
                tagNameList.Add(tagName);
            }
            return tagNameList;
        }

        /// <summary>
        /// 我的收藏
        /// </summary>
        /// <param name="userName">当前用户名</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页展示</param>
        /// <param name="searchKey">关键字搜索</param>
        /// <param name="total">查询总条数</param>
        /// <returns></returns>
        public List<PptShowModel> MyFavorite(string userName, int pageIndex, int pageSize, string searchKey, ref int total)
        {
            List<PptShowModel> pptList = new List<PptShowModel>();
            //通过userName获取用户ID
            int userID = repository.RetrieveByAccountName(userName).ID;
            //通过用户ID查询所有的收藏记录
            List<Tb_userPPt_Favr> favrUserList = favrRepository.RetrieveByUser(userID);
            //遍历收藏集合查询收藏的所有ppt的ID
            int id = 0;
            for (int i = 0; i < favrUserList.Count; i++)
            {
                //获取到pptID
                id = favrUserList[i].PPTId;
                //通过ID查询ppt
                var pptObj = pptRepository.RetrieveById(id).GetAwaiter().GetResult();
                if(pptObj != null)
                {
                    PptShowModel ppt = new PptShowModel();
                    ppt.ID = pptObj.ID;
                    ppt.MinPicture = pptObj.MinPicture;
                    ppt.Name = pptObj.Name;
                    ppt.Tags = GetPptTags(id);
                    ppt.Headshot = pptObj.CreatedBy?.Headshot;
                    ppt.CreatedBy = pptObj.CreatedBy?.Name;
                    ppt.Path = pptObj.Download_path;
                    ppt.CreatedTime = pptObj.CreatedOn;
                    ppt.FavrState = true;
                    pptList.Add(ppt);
                }
            }
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                pptList = pptList.Where(t => t.Name.Contains(searchKey)).ToList();
            }
            //记录查询总数
            int Total = pptList.Count();
            total = Total;
            //按照页码来截取需要的收藏记录
            pptList = pptList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return pptList;
        }

        /// <summary>
        /// 我的下载
        /// </summary>
        /// <param name="userName">当前用户名</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页展示</param>
        /// <param name="searchKey">关键字搜索</param>
        /// <param name="total">查询总条数</param>
        /// <returns></returns>
        public List<PptShowModel> MyDownload(string userName, int pageIndex, int pageSize, string searchKey, ref int total)
        {
            List<PptShowModel> pptList = new List<PptShowModel>();
            //通过userName获取用户ID
            int userID = repository.RetrieveByAccountName(userName).ID;
            //通过用户ID查询所有下载记录
            List<Tb_userPPt_Down> downList = hisDownRepository.RetrieveByUser(userID);
            //遍历收藏集合查询下载的所有ppt的ID
            int id = 0;
            for (int i = 0; i < downList.Count; i++)
            {
                //获取到pptID
                id = downList[i].PPTId;
                //通过ID查询ppt
                var pptObj = pptRepository.RetrieveById(id).GetAwaiter().GetResult();
                if (pptObj != null)
                {
                    PptShowModel ppt = new PptShowModel();
                    ppt.ID = pptObj.ID;
                    ppt.MinPicture = pptObj.MinPicture;
                    ppt.Name = pptObj.Name;
                    ppt.Tags = GetPptTags(id);
                    ppt.Headshot = pptObj.CreatedBy?.Headshot;
                    ppt.CreatedBy = pptObj.CreatedBy?.Name;
                    ppt.Path = pptObj.Download_path;
                    ppt.CreatedTime = pptObj.CreatedOn;
                    if (favrRepository.RetrieveByUserAndPpt(userID, pptObj.ID) != null)
                    {
                        ppt.FavrState = true;
                    }
                    else
                    {
                        ppt.FavrState = false;
                    }
                    pptList.Add(ppt);
                } 
            }
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                pptList = pptList.Where(t => t.Name.Contains(searchKey)).ToList();
            }
            //记录查询总数
            int Total = pptList.Count();
            total = Total;
            //按照页码来截取展示的ppt
            pptList = pptList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return pptList;
        }

        /// <summary>
        /// 我的上传
        /// </summary>
        /// <param name="userName">当前用户名</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页展示</param>
        /// <param name="searchKey">关键字搜索</param>
        /// <param name="total">查询总条数</param>
        /// <returns></returns>
        public List<PptShowModel> MyUpload(string userName, int pageIndex, int pageSize, string searchKey, ref int total)
        {
            List<PptShowModel> pptList = new List<PptShowModel>();
            //通过userName获取用户ID
            int userID = repository.RetrieveByAccountName(userName).ID;
            //通过用户ID查询上传的所有ppt
            List<Tb_ppt> upList = pptRepository.RetrieveAll().Where(t => t.CreatedBy.ID == userID).OrderByDescending(t => t.CreatedOn).ToList();
            for (int i = 0; i < upList.Count; i++)
            {
                var pptObj = upList[i];
                PptShowModel ppt = new PptShowModel();
                ppt.ID = pptObj.ID;
                ppt.MinPicture = pptObj.MinPicture;
                ppt.Name = pptObj.Name;
                ppt.Tags = GetPptTags(pptObj.ID);
                ppt.Headshot = pptObj.CreatedBy?.Headshot;
                ppt.CreatedBy = pptObj.CreatedBy?.Name;
                ppt.Path = pptObj.Download_path;
                ppt.CreatedTime = pptObj.CreatedOn;
                if (favrRepository.RetrieveByUserAndPpt(userID, pptObj.ID) != null)
                {
                    ppt.FavrState = true;
                }
                else
                {
                    ppt.FavrState = false;
                }
                pptList.Add(ppt);
            }
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                pptList = pptList.Where(t => t.Name.Contains(searchKey)).ToList();
            }
            //记录查询总数
            int Total = pptList.Count();
            total = Total;
            //按照页码来截取展示的ppt
            pptList = pptList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return pptList;
        }

        /// <summary>
        /// 新增advice
        /// </summary>
        public void AddAdvice(string userName,string advice,int type)
        {
            Tb_advice newAdvice = new Tb_advice();
            newAdvice.Advice = advice;
            newAdvice.CreatedBy = repository.RetrieveByAccountName(userName);
            newAdvice.type = type;
            newAdvice.CreateTime = DateTime.Now;
            adviceRepository.Add(newAdvice);
        }

    }
}