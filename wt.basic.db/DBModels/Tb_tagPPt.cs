using System;
using System.Collections.Generic;
using System.Text;

namespace wt.basic.db.DBModels
{
    public class Tb_tagPPt
    {
        public int TagId { get; set; }
        public int PPTId { get; set; }
        public virtual Tb_ppt PPt{ get; set; }
        public virtual Tb_tags Tag { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
