using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt.basic.dataAccess.Repository.Tag;
using wt.basic.dataAccess.Repository.User;
using wt.basic.db.DBModels;

namespace wt.basic.service.Tag
{

    public class TagService
    {
        private ITagRepository repository = null;
        private IUsersRepository userRepository = null;
        public TagService(ITagRepository repository, IUsersRepository repository1)
        {
            this.repository = repository;
            this.userRepository = repository1;
        }

        public IQueryable<Tb_tags> QueryAll()
        {
            var tags = repository.RetrieveAll();
            return tags;
        }

        /// <summary>
        /// 新增tag
        /// </summary>
        /// <param name="name">tag名字</param>
        /// <param name="userName">创建tag的当前用户名字</param>
        public void AddTag(string name, string userName)
        {
            var tag = new Tb_tags();
            tag.Name = name;
            tag.CreatedOn = DateTime.Now;
            tag.CreatedBy = userRepository.RetrieveByAccountName(userName);
            repository.Add(tag);
        }

        /// <summary>
        ///删除tag
        /// </summary>
        /// <param name="id">tag的id</param>
        public async void DeleteTag(int id)
        {
            //var type = new Tb_type();
            //type = await repository.RetrieveById(id);
            repository.DeleteById(id);
        }
    }
}
