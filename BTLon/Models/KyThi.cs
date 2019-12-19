using System;
using System.Collections.Generic;

namespace BTLon.Models
{
    public partial class KyThi
    {
        public KyThi()
        {
            CaThi = new HashSet<CaThi>();
            SvMonThiKiThi = new HashSet<SvMonThiKiThi>();
        }

        public Guid KyThiId { get; set; }
        public string KyThiIdFake { get; set; }
        public string KyThiName { get; set; }

        public virtual ICollection<CaThi> CaThi { get; set; }
        public virtual ICollection<SvMonThiKiThi> SvMonThiKiThi { get; set; }
    }
}
