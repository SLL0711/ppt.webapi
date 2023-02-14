using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.Favourites
{
    public interface IFavrRepository : IRepository<Tb_userPPt_Favr>
    {
        Tb_userPPt_Favr RetrieveByUserAndPpt(int userID, int pptID);
        List<Tb_userPPt_Favr> RetrieveByUser(int userID);
    }
}
