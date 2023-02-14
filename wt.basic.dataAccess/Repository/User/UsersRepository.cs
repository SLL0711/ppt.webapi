using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt.basic.db.DBContexts;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.User
{
    public class UsersRepository : IUsersRepository
    {
        private readonly BasicContext db;
        public UsersRepository(BasicContext dbContext)
        {
            this.db = dbContext;
        }

        /// <summary>
        /// 新增user表记录
        /// </summary>
        /// <param name="t">user记录</param>
        /// <returns></returns>
        public async Task Add(Tb_users t)
        {
            db.tb_user.Add(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 删除user表记录
        /// </summary>
        /// <param name="t">user记录</param>
        /// <returns></returns>
        public async Task Delete(Tb_users t)
        {
            t.State = 0;
            db.tb_user.Update(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 通过userID删除user表记录
        /// </summary>
        /// <param name="id">user表ID</param>
        /// <returns></returns>
        public async Task DeleteById(int id)
        {
            var t = db.tb_user.FirstOrDefault(a => a.ID == id && a.State == 1);
            t.State = 0;
            db.tb_user.Update(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 查询user表所有记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tb_users> RetrieveAll()
        {
            return db.tb_user.Where(t => t.State == 1);
        }

        /// <summary>
        /// 通过userName查询user对象
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns></returns>
        public Tb_users RetrieveByAccountName(string userName)
        {
            var user = db.tb_user.Where(t => t.AccountName == userName && t.State == 1).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// 通过userID查询user表记录
        /// </summary>
        /// <param name="id">user表ID</param>
        /// <returns></returns>
        public async Task<Tb_users> RetrieveById(int id)
        {

            Tb_users users = db.tb_user.Where(t => t.ID == id && t.State == 1).FirstOrDefault();
            return users;
        }

        /// <summary>
        /// 更新user表记录
        /// </summary>
        /// <param name="t">user记录</param>
        /// <returns></returns>
        public async Task Update(Tb_users t)
        {
            db.tb_user.Update(t);
            db.SaveChanges();
        }
    }
}