using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wt.basic.dataAccess.Repository.HistoryDownload;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.HistoryDownload
{
    public interface IHisDownRepository : IRepository<Tb_userPPt_Down>
    {
        Tb_userPPt_Down RetrieveByUserAndPpt(int userID, int pptID);

        List<Tb_userPPt_Down> RetrieveByUser(int userID);
    }
}
