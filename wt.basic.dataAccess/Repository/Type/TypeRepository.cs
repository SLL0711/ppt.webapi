using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wt.basic.db.DBContexts;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.Type
{
    public class TypeRepository : ITypeRepository
    {
        private readonly BasicContext db;
        public TypeRepository(BasicContext dbContext)
        {
            this.db = dbContext;
        }

        /// <summary>
        /// 新增type表记录
        /// </summary>
        /// <param name="t">type记录</param>
        /// <returns></returns>
        public async Task Add(Tb_type t)
        {
            db.tb_type.Add(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 删除type表记录
        /// </summary>
        /// <param name="t">type记录</param>
        /// <returns></returns>
        public async Task Delete(Tb_type t)
        {
            t.State = 0;
            db.tb_type.Update(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 通过typeID删除type表记录
        /// </summary>
        /// <param name="id">type表ID</param>
        /// <returns></returns>
        public async Task DeleteById(int id)
        {
            var t = db.tb_type.FirstOrDefault(a => a.ID == id && a.State == 1);
            t.State = 0;
            db.tb_type.Update(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 查询所有type表记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tb_type> RetrieveAll()
        {
            //string accountname = db.tb_type.Where(t => t.CreatedOn < DateTime.Now).FirstOrDefault().CreatedBy.AccountName;
            //List<Tb_type> allType = (List<Tb_type>)db.tb_type.Where(t => 1 == 1);
            //return (IQueryable<Tb_type>)allType;
            return db.tb_type.Where(t => t.State == 1);
        }

        /// <summary>
        /// 通过typeID查询type表记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tb_type> RetrieveById(int id)
        {
            Tb_type type = db.tb_type.Where(t => t.ID == id && t.State == 1).FirstOrDefault();
            return type;
        }

        /// <summary>
        /// 更新type表记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task Update(Tb_type t)
        {
            db.tb_type.Update(t);
            db.SaveChanges();
        }


    }
}