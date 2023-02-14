using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt.basic.db.DBContexts;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.Tag
{

    public class TagPptRepository : ITagPptRepository
    {
        private readonly BasicContext db;
        public TagPptRepository(BasicContext dbContext)
        {
            this.db = dbContext;
        }

        /// <summary>
        /// 新增PPT与标签绑定表的记录
        /// </summary>
        /// <param name="t">PPT与标签的绑定记录</param>
        /// <returns></returns>
        public async Task Add(Tb_tagPPt t)
        {
            db.tb_tagppt.Add(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 删除PPT与标签绑定表的记录
        /// </summary>
        /// <param name="t">PPT与标签的绑定记录</param>
        /// <returns></returns>
        public async Task Delete(Tb_tagPPt t)
        {
            db.tb_tagppt.Remove(t);
            db.SaveChanges();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询PPT与标签绑定表的所有记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tb_tagPPt> RetrieveAll()
        {
            return db.tb_tagppt.Where(t => 1 == 1);
        }

        public Task<Tb_tagPPt> RetrieveById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过ppt的ID查询对应的标签绑定记录
        /// </summary>
        /// <param name="pptID">ppt的ID</param>
        /// <returns></returns>
        public List<Tb_tagPPt> RetrieveByPpt(int pptID)
        {
            List<Tb_tagPPt> tagPPts = new List<Tb_tagPPt>();
            tagPPts = db.tb_tagppt.Where(t => t.PPTId == pptID).ToList();
            return tagPPts;
        }

        public Task Update(Tb_tagPPt t)
        {
            throw new NotImplementedException();
        }
    }
}