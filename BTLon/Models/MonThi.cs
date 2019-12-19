using System;
using System.Collections.Generic;

namespace BTLon.Models
{
    public partial class MonThi
    {
        public MonThi()
        {
            CaThiMonThi = new HashSet<CaThiMonThi>();
            SvMonThiKiThi = new HashSet<SvMonThiKiThi>();
        }

        public Guid MonThiId { get; set; }
        public string MonThiIdFake { get; set; }
        public string MonThiName { get; set; }
        public string GiaoVien { get; set; }

        public virtual ICollection<CaThiMonThi> CaThiMonThi { get; set; }
        public virtual ICollection<SvMonThiKiThi> SvMonThiKiThi { get; set; }
    }
}
