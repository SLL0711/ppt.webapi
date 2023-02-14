using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wt.basic.db.DBContexts;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.HistoryDownload
{
    public class HisDownRepository : IHisDownRepository
    {
        private readonly BasicContext db;

        public HisDownRepository(BasicContext basicContext)
        {
            this.db = basicContext;
        }

        /// <summary>
        /// 新增下载记录
        /// </summary>
        /// <param name="t">下载记录</param>
        /// <returns></returns>
        public async Task Add(Tb_userPPt_Down t)
        {
            db.tb_userppt_down.Add(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 删除下载记录
        /// </summary>
        /// <param name="t">下载记录</param>
        /// <returns></returns>
        public async Task Delete(Tb_userPPt_Down t)
        {
            db.tb_userppt_down.Add(t);
            db.SaveChanges();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询所有下载记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tb_userPPt_Down> RetrieveAll()
        {
            return db.tb_userppt_down.Where(t => 1 == 1);
        }

        public Task<Tb_userPPt_Down> RetrieveById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过用户ID查询对应的下载记录
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public List<Tb_userPPt_Down> RetrieveByUser(int userID)
        {
            List<Tb_userPPt_Down> downList = db.tb_userppt_down.Where(t => t.UserId == userID).OrderByDescending(t => t.CreateTime).ToList();
            return downList;
        }

        /// <summary>
        /// 通过userID和pptID查询下载记录
        /// </summary>
        /// <param name="userID">user表ID</param>
        /// <param name="pptID">ppt表ID</param>
        /// <returns></returns>
        public Tb_userPPt_Down RetrieveByUserAndPpt(int userID, int pptID)
        {
            var Down = db.tb_userppt_down.FirstOrDefault(t => t.PPTId == pptID && t.UserId == userID);
            return Down;
        }

        public Task Update(Tb_userPPt_Down t)
        {
            throw new NotImplementedException();
        }
    }
}
