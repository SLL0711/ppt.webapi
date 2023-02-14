using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt.basic.dataAccess.Repository.Test;
using wt.basic.db.DBModels;
using wt.lib.Office;

namespace wt.basic.service.Test
{
    public class TestService
    {
        private ITestRepository repository = null;
        public TestService(ITestRepository repository)
        {
            this.repository = repository;
        }

        public void AddTags()
        {
            repository.Add(new db.DBModels.Tb_tags());
        }

        public List<Tb_tags> GetTags()
        {
            return repository.RetrieveAll().ToList();
        }

        public async Task DelTags(int id)
        {
            await repository.DeleteById(id);
        }

        public async Task UpdateTags(int id)
        {
            var tagObj = GetTags().FirstOrDefault(t => t.ID == id);
            tagObj.Name = tagObj.Name + "1";
            await repository.Update(tagObj);
        }

        public void TestGdipIssue(string pptPath)
        {
            PPtConvert.TransferPPT2ImgReturnList(pptPath);
        }
    }
}
