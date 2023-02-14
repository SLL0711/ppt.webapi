using System;
using System.Collections.Generic;
using System.Text;

namespace wt.basic.db.DBModels
{
    public class Tb_advice
    {
        public int ID { get; set; }
        public string Advice { get; set; }
        public int type { get; set; }
        public virtual Tb_users CreatedBy { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
