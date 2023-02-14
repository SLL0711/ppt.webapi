using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt.basic.dataAccess.Repository.Test;
using wt.basic.db.DBContexts;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.Ppt
{
    public class PptRepository : IPptRepository
    {
        private readonly BasicContext db;
        public PptRepository(BasicContext dbContext)
        {
            this.db = dbContext;
        }

        /// <summary>
        /// 新增PPT记录
        /// </summary>
        /// <param name="t">PPT记录</param>
        /// <returns></returns>
        public async Task Add(Tb_ppt t)
        {
            db.tb_ppt.Add(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 删除PPT记录
        /// </summary>
        /// <param name="t">PPT记录</param>
        /// <returns></returns>
        public async Task Delete(Tb_ppt t)
        {
            t.State = 0;
            db.tb_ppt.Update(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 通过pptID删除PPT
        /// </summary>
        /// <param name="id">PPT表ID</param>
        /// <returns></returns>
        public async Task DeleteById(int id)
        {
            var t = db.tb_ppt.FirstOrDefault(t => t.ID == id && t.State == 1);
            t.State = 0;
            db.tb_ppt.Update(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 查询所有PPT信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tb_ppt> RetrieveAll()
        {
            return db.tb_ppt.Where(t => t.State == 1).Include(ppt => ppt.CreatedBy).Include(ppt => ppt.ModifiedBy);
        }

        /// <summary>
        /// 通过pptID查询PPT信息
        /// </summary>
        /// <param name="id">PPT表ID</param>
        /// <returns></returns>
        public async Task<Tb_ppt> RetrieveById(int id)
        {
            var pptObj = db.tb_ppt.Where(t => t.State == 1)
                .Include(ppt => ppt.CreatedBy).Include(ppt => ppt.ModifiedBy)
                .FirstOrDefault(t => t.ID == id);
            return pptObj;
        }

        /// <summary>
        /// 通过typeID查询PPT信息
        /// </summary>
        /// <param name="id">type表ID</param>
        /// <returns></returns>
        public List<Tb_ppt> RetrieveByType(int id)
        {
            List<Tb_ppt> pptList = db.tb_ppt.Where(t => t.Type.ID == id && t.State == 1)
                .Include(ppt => ppt.CreatedBy).Include(ppt => ppt.ModifiedBy)
                .ToList();
            return pptList;
        }

        /// <summary>
        /// 更新PPT表记录
        /// </summary>
        /// <param name="t">PPT记录</param>
        /// <returns></returns>
        public async Task Update(Tb_ppt t)
        {
            db.tb_ppt.Update(t);
            db.SaveChanges();
        }
    }
}
