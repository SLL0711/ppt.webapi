using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wt.basic.db.DBModels;

namespace wt.basic.dataAccess.Repository.User
{
    public interface IUsersRepository : IRepository<Tb_users>
    {
        Tb_users RetrieveByAccountName(string userName);
    }
}
