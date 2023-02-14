using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace wt.basic.db.DBModels
{
    public class Tb_userPPt_Favr
    {
        public int UserId { get; set; }
        public int PPTId { get; set; }

        public virtual Tb_users User { get; set; }
        public virtual Tb_ppt PPt { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
