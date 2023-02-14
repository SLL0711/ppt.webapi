using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt.basic.dataAccess.Repository.Type;
using wt.basic.db.DBModels;
using wt.basic.dataAccess.Repository.User;

namespace wt.basic.service.Type
{
    public class TypeService
    {
        private ITypeRepository repository = null;
        private IUsersRepository userRepository = null;
        public TypeService(ITypeRepository repository, IUsersRepository repository1)
        {
            this.repository = repository;
            this.userRepository = repository1;
        }

        /// <summary>
        /// 查询所有类型
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tb_type> QueryAll()
        {
            return repository.RetrieveAll();

        }

        /// <summary>
        /// 新增type
        /// </summary>
        /// <param name="name">type名字</param>
        /// <param name="userName">创建type的当前用户名字</param>
        public void AddType(string name, string userName)
        {
            var type = new Tb_type();
            type.Name = name;
            type.CreatedOn = DateTime.Now;
            type.CreatedBy = userRepository.RetrieveByAccountName(userName);
            repository.Add(type);
        }

        /// <summary>
        ///删除type
        /// </summary>
        /// <param name="id">type的id</param>
        public async void DeleteType(int id)
        {
            //var type = new Tb_type();
            //type = await repository.RetrieveById(id);
            repository.DeleteById(id);
        }
    }
}