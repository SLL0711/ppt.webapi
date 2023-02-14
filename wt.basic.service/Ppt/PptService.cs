using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt.basic.dataAccess.Repository.Picture;
using wt.basic.dataAccess.Repository.Ppt;
using wt.basic.dataAccess.Repository.User;
using wt.basic.dataAccess.Repository.Tag;
using wt.basic.dataAccess.Repository.Favourites;
using wt.basic.dataAccess.Repository.HistoryDownload;
using wt.basic.db.DBModels;
using wt.basic.service.Model;
using wt.basic.service.Common;
using wt.basic.dataAccess;

namespace wt.basic.service.Ppt
{
    public class PptService
    {
        private IPptRepository repository = null;
        private ITagPptRepository tagPptRepository = null;
        private IPictureRepository pictureRepository = null;
        private ITagRepository tagRepository = null;
        private IFavrRepository favrRepository = null;
        private IHisDownRepository hisDownRepository = null;
        private IUsersRepository usersRepository = null;

        public PptService(IPptRepository repository, ITagPptRepository tagPptRepository, IPictureRepository pictureRepository, ITagRepository tagRepository, IFavrRepository favrRepository, IHisDownRepository hisDownRepository, IUsersRepository usersRepository)
        {
            this.repository = repository;
            this.tagPptRepository = tagPptRepository;
            this.pictureRepository = pictureRepository;
            this.tagRepository = tagRepository;
            this.favrRepository = favrRepository;
            this.hisDownRepository = hisDownRepository;
            this.usersRepository = usersRepository;
        }

        public void AddPpt(Tb_ppt t)
        {
            repository.Add(t);
        }

        /// <summary>
        /// 通过id获取ppt对象
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pptID">ppt的id</param>
        /// <returns></returns>
        public PptDetailsModel GetPpt(string userName, int pptID)
        {
            var pptObj = repository.RetrieveById(pptID).Result;
            PptDetailsModel ppt = new PptDetailsModel();
            ppt.ID = pptObj.ID;
            ppt.Name = pptObj.Name;
            ppt.CreatedBy = pptObj.CreatedBy?.Name;
            ppt.Page = pptObj.Page;
            ppt.MinPicture = pptObj.MinPicture;
            ppt.Size = pptObj.Size;
            ppt.Turn_picture = GetPicturePath(pptID);
            ppt.Path = pptObj.Download_path;
            ppt.FavrState = false;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                int userID = usersRepository.RetrieveByAccountName(userName).ID;
                if (favrRepository.RetrieveByUserAndPpt(userID, pptID) != null)
                {
                    ppt.FavrState = true;
                }
            }
            return ppt;
        }

        /// <summary>
        /// 通过ppt的ID查询ppt的轮播图路径，以string类型展示
        /// </summary>
        /// <param name="pptID">ppt的ID</param>
        /// <returns></returns>
        public List<string> GetPicturePath(int pptID)
        {
            //获取ppt对应的所有轮播图记录
            List<Tb_picture> turnPicture = pictureRepository.RetrieveByPpt(pptID);
            List<string> turnPicPath = new List<string>();
            string path;
            for (int i = 0; i < turnPicture.Count(); i++)
            {
                path = turnPicture[i].Path;
                turnPicPath.Add(path);
            }
            return turnPicPath;
        }

        /// <summary>
        /// 通过ID获取对应PPT的下载路径
        /// </summary>
        /// <param name="pptID">PPT的ID</param>
        /// <returns></returns>
        public List<string> GetPPtPath(int pptID)
        {
            var pptObj = repository.RetrieveById(pptID).Result;
            List<string> PPtPath = new List<string>();
            PPtPath.Add(pptObj.Download_path);
            return PPtPath;
        }

        public async Task DelPpt(int id)
        {
            await repository.DeleteById(id);
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
        /// 查询type对应的ppt
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="id">type对应的ID</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页展示</param>
        /// <param name="searchKey">搜索关键字</param>
        /// <param name="total">查询总条数</param>
        /// <returns></returns>
        public List<PptShowModel> GetTypePpt(string userName, int id, int pageIndex, int pageSize, string searchKey, ref int total)
        {
            List<PptShowModel> pptList = new List<PptShowModel>();
            List<Tb_ppt> typePptList = repository.RetrieveByType(id);
            for (int i = 0; i < typePptList.Count(); i++)
            {
                var pptObj = typePptList[i];
                PptShowModel ppt = new PptShowModel();
                ppt.ID = pptObj.ID;
                ppt.MinPicture = pptObj.MinPicture;
                ppt.Name = pptObj.Name;
                ppt.Tags = GetPptTags(pptObj.ID);
                ppt.Headshot = pptObj.CreatedBy?.Headshot;
                ppt.CreatedBy = pptObj.CreatedBy?.Name;
                ppt.Path = pptObj.Download_path;
                ppt.CreatedTime = pptObj.CreatedOn;
                ppt.FavrState = false;
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    int userID = usersRepository.RetrieveByAccountName(userName).ID;
                    if (favrRepository.RetrieveByUserAndPpt(userID, pptObj.ID) != null)
                    {
                        ppt.FavrState = true;
                    }
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
            //按照页码来截取要展示的ppt
            pptList = pptList.OrderByDescending(t => t.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return pptList;
        }

        /// <summary>
        /// 查询收藏量前二十的ppt
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public List<PptShowModel> GetRecommendPpt(string userName)
        {
            //获取排名收藏量前20的ppt的<pptID,num>
            var favr = favrRepository.RetrieveAll().GroupBy(t => t.PPTId)
                .Select(a => new
                {
                    pptID = a.Key,
                    num = a.Count()
                }).OrderByDescending(b => b.num).ToList();
            for (int i = 0; i < favr.Count; i++)
            {
                if (repository.RetrieveById(favr[i].pptID).Result == null)
                {
                    favr.RemoveAt(i);
                }
            }
            favr = favr.Take(20).ToList();
            //如果ppt不够20，则用最新上传补齐
            if (favr.Count < 20)
            {
                List<int> favrList = favr.Select(a => a.pptID).ToList();
                //获取20个最新上传
                var uploadNew = repository.RetrieveAll()
                    .OrderByDescending(b => b.CreatedOn).Take(20).Select(a => new
                    {
                        pptID = a.ID,
                        num = 1
                    }).ToList();

                for (int i = 0; i < uploadNew.Count(); i++)
                {
                    //判断最新上传和收藏是否重合
                    if (!favrList.Contains(uploadNew[i].pptID))
                    {
                        favr.Add(uploadNew[i]);
                    }
                }

                favr = favr.Take(20).ToList();
            }

            List<PptShowModel> pptList = new List<PptShowModel>();
            int id;
            for (int i = 0; i < favr.Count; i++)
            {
                id = favr[i].pptID;
                Tb_ppt pptObj = repository.RetrieveById(id).Result;
                PptShowModel ppt = new PptShowModel();
                ppt.ID = pptObj.ID;
                ppt.MinPicture = pptObj.MinPicture;
                ppt.Name = pptObj.Name;
                ppt.Tags = GetPptTags(id);
                ppt.Headshot = pptObj.CreatedBy?.Headshot;
                ppt.CreatedBy = pptObj.CreatedBy?.Name;
                ppt.Path = pptObj.Download_path;
                ppt.FavrState = false;
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    int userID = usersRepository.RetrieveByAccountName(userName).ID;
                    if (favrRepository.RetrieveByUserAndPpt(userID, pptObj.ID) != null)
                        ppt.FavrState = true;
                }

                pptList.Add(ppt);
            }
            pptList = pptList.OrderByDescending(b => b.CreatedTime).ToList();
            return pptList;
        }

        /// <summary>
        /// 查询下载量前十的ppt
        /// </summary>
        /// <returns></returns>
        public List<PptShowModel> GetHotPpt()
        {
            var hisDown = hisDownRepository.RetrieveAll().GroupBy(t => t.PPTId).Select(a =>
            new
            {
                pptID = a.Key,
                num = a.Count()
            }).OrderByDescending(b => b.num).ToList();
            for (int i = 0; i < hisDown.Count; i++)
            {
                if (repository.RetrieveById(hisDown[i].pptID).Result == null)
                {
                    hisDown.RemoveAt(i);
                }
            }
            hisDown = hisDown.Take(10).ToList();
            List<PptShowModel> pptList = new List<PptShowModel>();
            int id;
            for (int i = 0; i < hisDown.Count; i++)
            {
                id = hisDown[i].pptID;
                Tb_ppt pptObj = repository.RetrieveById(id).Result;
                PptShowModel ppt = new PptShowModel();
                ppt.ID = pptObj.ID;
                ppt.MinPicture = pptObj.MinPicture;
                ppt.Name = pptObj.Name;
                ppt.Tags = GetPptTags(id);
                ppt.Headshot = pptObj.CreatedBy?.Headshot;
                ppt.CreatedBy = pptObj.CreatedBy?.Name;
                ppt.Path = pptObj.Download_path;
                pptList.Add(ppt);
            }
            return pptList;
        }

        /// <summary>
        /// 查询新增的十个ppt
        /// </summary>
        /// <returns></returns>
        public List<PptShowModel> GetNewPpt()
        {
            List<PptShowModel> pptList = new List<PptShowModel>();
            List<Tb_ppt> newList = repository.RetrieveAll().OrderByDescending(t => t.CreatedOn).Take(10).ToList();
            for (int i = 0; i < newList.Count(); i++)
            {
                var pptObj = newList[i];
                PptShowModel ppt = new PptShowModel();
                ppt.ID = pptObj.ID;
                ppt.MinPicture = pptObj.MinPicture;
                ppt.Name = pptObj.Name;
                ppt.Tags = GetPptTags(newList[i].ID);
                ppt.Headshot = pptObj.CreatedBy?.Headshot;
                ppt.CreatedBy = pptObj.CreatedBy?.Name;
                ppt.Path = pptObj.Download_path;
                ppt.CreatedTime = pptObj.CreatedOn;
                pptList.Add(ppt);

            }
            return pptList;
        }

        /// <summary>
        /// 查询关键字相关的最新ppt
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页展示</param>
        /// <param name="searchKey">搜索关键字</param>
        /// <param name="total">查询总条数</param>
        /// <returns></returns>
        public List<PptShowModel> SearchNewPpt(string userName, int pageIndex, int pageSize, string searchKey, ref int total)
        {
            List<PptShowModel> pptList = new List<PptShowModel>();
            List<Tb_ppt> allList = repository.RetrieveAll().ToList();
            for (int i = 0; i < allList.Count(); i++)
            {
                var pptObj = allList[i];
                //判断ppt的名字和标签是否含有关键字
                string tags = string.Join("", GetPptTags(pptObj.ID).ToArray());
                if (pptObj.Name.Contains(searchKey) || tags.Contains(searchKey) || pptObj.CreatedBy.Name.Contains(searchKey) || pptObj.CreatedBy.AccountName.Contains(searchKey))
                {
                    PptShowModel ppt = new PptShowModel();
                    ppt.ID = pptObj.ID;
                    ppt.MinPicture = pptObj.MinPicture;
                    ppt.Name = pptObj.Name;
                    ppt.Tags = GetPptTags(pptObj.ID);
                    ppt.Headshot = pptObj.CreatedBy?.Headshot;
                    ppt.CreatedBy = pptObj.CreatedBy?.Name;
                    ppt.Path = pptObj.Download_path;
                    ppt.CreatedTime = pptObj.CreatedOn;
                    ppt.FavrState = false;
                    if (!string.IsNullOrWhiteSpace(userName))
                    {
                        int userID = usersRepository.RetrieveByAccountName(userName).ID;
                        if (favrRepository.RetrieveByUserAndPpt(userID, pptObj.ID) != null)
                            ppt.FavrState = true;
                    }

                    pptList.Add(ppt);
                }
            }
            //记录查询总数
            int Total = pptList.Count();
            total = Total;
            //按照页码来截取要展示的ppt
            pptList = pptList.OrderByDescending(t => t.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return pptList;
        }

        /// <summary>
        /// 查询关键字相关的最热ppt
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页展示</param>
        /// <param name="searchKey">搜索关键字</param>
        /// <param name="total">查询总条数</param>
        /// <returns></returns>
        public List<PptShowModel> SearchHotPpt(string userName, int pageIndex, int pageSize, string searchKey, ref int total)
        {
            List<PptShowModel> pptList = new List<PptShowModel>();
            List<Tb_ppt> allList = repository.RetrieveAll().ToList();
            for (int i = 0; i < allList.Count(); i++)
            {
                var pptObj = allList[i];
                //判断ppt的名字和标签是否含有关键字
                string tags = string.Join("", GetPptTags(pptObj.ID).ToArray());
                if (pptObj.Name.Contains(searchKey) || tags.Contains(searchKey) || pptObj.CreatedBy.Name.Contains(searchKey) || pptObj.CreatedBy.AccountName.Contains(searchKey))
                {
                    PptShowModel ppt = new PptShowModel();
                    ppt.ID = pptObj.ID;
                    ppt.MinPicture = pptObj.MinPicture;
                    ppt.Name = pptObj.Name;
                    ppt.Tags = GetPptTags(pptObj.ID);
                    ppt.Headshot = pptObj.CreatedBy?.Headshot;
                    ppt.CreatedBy = pptObj.CreatedBy?.Name;
                    ppt.Path = pptObj.Download_path;
                    ppt.CreatedTime = pptObj.CreatedOn;
                    ppt.FavrState = false;
                    if (!string.IsNullOrWhiteSpace(userName))
                    {
                        int userID = usersRepository.RetrieveByAccountName(userName).ID;
                        if (favrRepository.RetrieveByUserAndPpt(userID, pptObj.ID) != null)
                            ppt.FavrState = true;
                    }
                    //ppt收藏量
                    ppt.FavrNum = favrRepository.RetrieveAll().Where(t => t.PPTId == pptObj.ID).Count();
                    //ppt下载量
                    ppt.DownNum = hisDownRepository.RetrieveAll().Where(t => t.PPTId == pptObj.ID).Count();

                    pptList.Add(ppt);
                }
            }
            //记录查询总数
            int Total = pptList.Count();
            total = Total;
            //按照页码来截取要展示的ppt
            pptList = pptList.OrderByDescending(t => t.FavrNum).ThenByDescending(t => t.DownNum)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return pptList;
        }
    }
}