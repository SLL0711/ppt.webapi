using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wt.basic.db.DBContexts;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.Favourites
{

    public class FavrRepository : IFavrRepository
    {
        private readonly BasicContext db;

        public FavrRepository(BasicContext basicContext)
        {
            this.db = basicContext;
        }

        /// <summary>
        /// 新增收藏记录
        /// </summary>
        /// <param name="t">收藏记录</param>
        /// <returns></returns>
        public async Task Add(Tb_userPPt_Favr t)
        {
            db.tb_userppt_fvrt.Add(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 删除收藏记录
        /// </summary>
        /// <param name="t">收藏记录</param>
        /// <returns></returns>
        public async Task Delete(Tb_userPPt_Favr t)
        {
            db.tb_userppt_fvrt.Remove(t);
            db.SaveChanges();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询所有收藏记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tb_userPPt_Favr> RetrieveAll()
        {
            return db.tb_userppt_fvrt.Where(t => 1 == 1);
        }

        public Task<Tb_userPPt_Favr> RetrieveById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过userID和pptID查询收藏纪录
        /// </summary>
        /// <param name="userID">user表ID</param>
        /// <param name="pptID">ppt表ID</param>
        /// <returns></returns>
        public Tb_userPPt_Favr RetrieveByUserAndPpt(int userID, int pptID)
        {
            var favr = db.tb_userppt_fvrt.FirstOrDefault(t => t.PPTId == pptID && t.UserId == userID);
            return favr;
        }

        public Task Update(Tb_userPPt_Favr t)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过用户ID查询对应的收藏记录
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Tb_userPPt_Favr> RetrieveByUser(int userID)
        {
            List<Tb_userPPt_Favr> favrList = db.tb_userppt_fvrt.Where(t => t.UserId == userID).OrderByDescending(t => t.CreateTime).ToList();
            return favrList;
        }

    }
}