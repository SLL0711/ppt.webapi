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
    public class TagRepository : ITagRepository
    {
        private readonly BasicContext db;
        public TagRepository(BasicContext dbContext)
        {
            this.db = dbContext;
        }

        /// <summary>
        /// 新增tags表记录
        /// </summary>
        /// <param name="t">tags记录</param>
        /// <returns></returns>
        public async Task Add(Tb_tags t)
        {
            db.tb_tags.Add(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 删除tags表记录
        /// </summary>
        /// <param name="t">tags表记录</param>
        /// <returns></returns>
        public async Task Delete(Tb_tags t)
        {
            t.State = 0;
            db.tb_tags.Update(t);
            db.SaveChanges();

        }

        /// <summary>
        /// 通过tagsID删除tags表记录
        /// </summary>
        /// <param name="id">tags表ID</param>
        /// <returns></returns>
        public async Task DeleteById(int id)
        {
            var t = db.tb_tags.FirstOrDefault(a => a.ID == id && a.State == 1);
            t.State = 0;
            db.tb_tags.Update(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 查询tags表所有记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tb_tags> RetrieveAll()
        {
            return db.tb_tags.Where(t => t.State == 1);
        }

        /// <summary>
        /// 通过tagsID查询tags表记录
        /// </summary>
        /// <param name="id">tags表ID</param>
        /// <returns></returns>
        public async Task<Tb_tags> RetrieveById(int id)
        {
            var tagObj = db.tb_tags.FirstOrDefault(t => t.ID == id && t.State == 1);
            return tagObj;
        }

        /// <summary>
        /// 更新tags表记录
        /// </summary>
        /// <param name="t">tags记录</param>
        /// <returns></returns>
        public async Task Update(Tb_tags t)
        {
            db.tb_tags.Update(t);
            db.SaveChanges();
        }
    }
}
