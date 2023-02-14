using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt.basic.db.DBContexts;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.Picture
{
    public class PictureRepository : IPictureRepository
    {
        private readonly BasicContext db;
        public PictureRepository(BasicContext dbContext)
        {
            this.db = dbContext;
        }

        /// <summary>
        /// 新增轮播图表记录
        /// </summary>
        /// <param name="t">轮播图记录</param>
        /// <returns></returns>
        public async Task Add(Tb_picture t)
        {
            db.tb_picture.Add(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 删除轮播图表记录
        /// </summary>
        /// <param name="t">轮播图记录</param>
        /// <returns></returns>
        public async Task Delete(Tb_picture t)
        {
            t.State = 0;
            db.tb_picture.Update(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 通过pictureID删除轮播图表记录
        /// </summary>
        /// <param name="id">pictureID</param>
        /// <returns></returns>
        public async Task DeleteById(int id)
        {
            var t = db.tb_picture.FirstOrDefault(a => a.ID == id && a.State == 1);
            t.State = 0;
            db.tb_picture.Update(t);
            db.SaveChanges();
        }

        /// <summary>
        /// 查询所有轮播图表记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tb_picture> RetrieveAll()
        {
            return db.tb_picture.Where(t => t.State == 1);
        }

        /// <summary>
        /// 通过id查询轮播图
        /// </summary>
        /// <param name="id">pictureID</param>
        /// <returns></returns>
        public async Task<Tb_picture> RetrieveById(int id)
        {
            Tb_picture picture = db.tb_picture.Where(t => t.ID == id && t.State == 1).FirstOrDefault();
            return picture;
        }

        /// <summary>
        /// 通过pptID查询对应的轮播图
        /// </summary>
        /// <param name="pptID">ppt的ID</param>
        /// <returns></returns>
        public List<Tb_picture> RetrieveByPpt(int pptID)
        {
            List<Tb_picture> turnPicture = new List<Tb_picture>();
            turnPicture = db.tb_picture.Where(t => t.Ppt.ID == pptID && t.State == 1).ToList();
            return turnPicture;
        }

        /// <summary>
        /// 更新轮播图表记录
        /// </summary>
        /// <param name="t">轮播图记录</param>
        /// <returns></returns>
        public async Task Update(Tb_picture t)
        {
            db.tb_picture.Update(t);
            db.SaveChanges();
        }
    }
}
