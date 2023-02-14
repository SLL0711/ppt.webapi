using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace wt.basic.db.DBModels
{
    public class Tb_picture
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? Sort { get; set; }
        public int State { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual Tb_users CreatedBy { get; set; }
        public virtual Tb_users ModifiedBy { get; set; }

        public virtual Tb_ppt Ppt { get; set; }
    }
}
