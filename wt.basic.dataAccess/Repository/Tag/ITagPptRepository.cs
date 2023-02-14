using System;
using System.Collections.Generic;
using System.Text;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.Tag
{
    public interface ITagPptRepository : IRepository<Tb_tagPPt>
    {
        List<Tb_tagPPt> RetrieveByPpt(int pptID);
    }
}
