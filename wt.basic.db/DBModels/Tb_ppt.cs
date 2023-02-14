using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using wt.basic.db.DBContexts;

namespace wt.basic.db.DBModels
{
    public class Tb_ppt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MinPicture { get; set; }
        public virtual ICollection<Tb_picture> Turn_picture { get; set; } = new List<Tb_picture>();
        public string Download_path { get; set; }
        public virtual Tb_type Type { get; set; }
        public int Page { get; set; }
        public string Size { get; set; }
        public int? Sort { get; set; }
        public int State { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual Tb_users CreatedBy { get; set; }
        public virtual Tb_users ModifiedBy { get; set; }
        public virtual ICollection<Tb_tags> Tag { get; set; } = new List<Tb_tags>();
        public virtual ICollection<Tb_tagPPt> Tag2 { get; set; } = new List<Tb_tagPPt>();
        public Boolean FavrState { get; set; }

        //[InverseProperty("pptFavourites")]
        public virtual ICollection<Tb_users> fvrt_user { get; set; } = new List<Tb_users>();

        public virtual ICollection<Tb_userPPt_Favr> fvrt_user2 { get; set; } = new List<Tb_userPPt_Favr>();

        //[InverseProperty("pptHistoryDowns")]
        public virtual ICollection<Tb_users> down_user { get; set; } = new List<Tb_users>();

        public virtual ICollection<Tb_userPPt_Down> down_user2 { get; set; } = new List<Tb_userPPt_Down>();
    }
}
