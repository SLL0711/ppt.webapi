using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using wt.basic.db.DBContexts;

namespace wt.basic.db.DBModels
{
    public class Tb_users
    {
        public int ID { get; set; }
        public string Headshot { get; set; }
        public string Name { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public int? Sort { get; set; }
        public int State { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        //[InverseProperty("CreatedBy")]
        public virtual ICollection<Tb_ppt> pptCreateds { get; set; } = new List<Tb_ppt>();

        //[InverseProperty("ModifiedBy")]
        public virtual ICollection<Tb_ppt> pptModifys { get; set; } = new List<Tb_ppt>();

        //[InverseProperty("userFavourites")]
        public virtual ICollection<Tb_ppt> fvrt_ppt { get; set; } = new List<Tb_ppt>();

        public virtual ICollection<Tb_userPPt_Favr> fvrt_ppt2 { get; set; } = new List<Tb_userPPt_Favr>();

        //[InverseProperty("usersDownloads")]
        public virtual ICollection<Tb_ppt> down_ppt { get; set; } = new List<Tb_ppt>();
        public virtual ICollection<Tb_userPPt_Down> down_ppt2 { get; set; } = new List<Tb_userPPt_Down>();

        //[InverseProperty("CreatedBy")]
        public virtual ICollection<Tb_advice> adviceCreateds { get; set; } = new List<Tb_advice>();

    }
}
