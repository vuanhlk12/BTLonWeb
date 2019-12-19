using System;
using System.Collections.Generic;

namespace BTLon.Models
{
    public partial class User
    {
        public User()
        {
            SvDiaDiem = new HashSet<SvDiaDiem>();
            SvMonThiKiThi = new HashSet<SvMonThiKiThi>();
        }

        public Guid UserId { get; set; }
        public string UserIdfake { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime? Birth { get; set; }
        public Guid? CurrentKiThi { get; set; }

        public virtual ICollection<SvDiaDiem> SvDiaDiem { get; set; }
        public virtual ICollection<SvMonThiKiThi> SvMonThiKiThi { get; set; }
    }
}
