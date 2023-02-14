using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.Ppt
{
    public interface IPptRepository : IRepository<Tb_ppt>
    {
        List<Tb_ppt> RetrieveByType(int id);
    }
}
