using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt.basic.db.DBContexts;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.Advice
{
    public class AdviceRepository : IAdviceRepository
    {
        private readonly BasicContext db;
        public AdviceRepository(BasicContext dbContext)
        {
            this.db = dbContext;
        }

        /// <summary>
        /// 新增advice记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task Add(Tb_advice t)
        {
            db.tb_advice.Add(t);
            db.SaveChanges();
        }

        public Task Delete(Tb_advice t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tb_advice> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public Task<Tb_advice> RetrieveById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Tb_advice t)
        {
            throw new NotImplementedException();
        }
    }
}
