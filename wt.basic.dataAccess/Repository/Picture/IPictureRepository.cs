using System;
using System.Collections.Generic;
using System.Text;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.Picture
{
    public interface IPictureRepository:IRepository<Tb_picture>
    {
        List<Tb_picture> RetrieveByPpt(int pptID);
    }
}
