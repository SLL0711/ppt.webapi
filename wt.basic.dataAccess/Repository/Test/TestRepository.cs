using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt.basic.db.DBContexts;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.Test
{
    public class TestRepository : ITestRepository
    {
        private readonly BasicContext db;
        public TestRepository(BasicContext dbContext)
        {
            this.db = dbContext;
        }


        public async Task Add(Tb_tags t)
        {
            //ICollection<Tb_ppt> ppts = db.tb_user.FirstOrDefault(u => u.ID == 1).fvrt_ppt;
            //ppts.Remove(ppts.FirstOrDefault(p => p.ID == 1));
            //db.SaveChanges();

            try
            {
                t = new Tb_tags();
                t.Name = "类别1";
                db.tb_tags.Add(t);
                //await db.SaveChanges();
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        public async Task AddUserFavr()
        {
            //db.tb_userppt_fvrt.Add(new Tb_userPPt_Favr
            //{
            //    PPTId = 1,
            //    UserId = 1,
            //    CreateTime = DateTime.Now
            //});


            //db.tb_userppt_down.Add(new Tb_userPPt_Down
            //{
            //    PPTId = 1,
            //    UserId = 1,
            //    CreateTime = DateTime.Now
            //});

            //db.SaveChanges();


            //1、创建用户 2、创建ppt 3、创建收藏表记录
            Tb_users user = new Tb_users();
            user.AccountName = "zhangsan";
            user.State = 0;
            user.CreatedOn = DateTime.Now;
            user.ModifiedOn = DateTime.Now;

            Tb_ppt ppt = new Tb_ppt();
            ppt.Name = "ppt";
            ppt.State = 0;
            ppt.CreatedOn = DateTime.Now;
            ppt.ModifiedOn = DateTime.Now;
            ppt.MinPicture = "/";
            ppt.Download_path = "/";
            ppt.Page = 10;
            ppt.Size = "111223";

            user.fvrt_ppt.Add(ppt);

            db.tb_user.Add(user);
            db.tb_ppt.Add(ppt);

            db.SaveChanges();
        }

        public async Task DelUserFavr(int userId, int favrId)
        {
            ICollection<Tb_ppt> ppts = db.tb_user.FirstOrDefault(u => u.ID == userId).fvrt_ppt;
            var pptObj = db.tb_ppt.FirstOrDefault(p => p.ID == favrId);
            ppts.Remove(pptObj);
            db.SaveChanges();
        }

        public IQueryable<Tb_tags> RetrieveAll()
        {
            //throw new NotImplementedException();
            //db.
            string accoutname = db.tb_tags.Where(t => 1 == 1).FirstOrDefault().CreatedBy.AccountName;
            return db.tb_tags.Where(t => 1 == 1);
        }

        public Task Delete(Tb_tags t)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteById(int id)
        {
            db.tb_tags.Remove(db.tb_tags.FirstOrDefault(a => a.ID == id));
            db.SaveChanges();
        }

        public Task<Tb_tags> RetrieveById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Tb_tags t)
        {
            db.tb_tags.Update(t);
            db.SaveChanges();
        }
    }
}
