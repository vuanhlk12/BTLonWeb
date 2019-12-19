using System;
using System.Collections.Generic;

namespace BTLon.Models
{
    public partial class CaThi
    {
        public CaThi()
        {
            CaThiMonThi = new HashSet<CaThiMonThi>();
        }

        public Guid CaThiId { get; set; }
        public string CaThiIdFake { get; set; }
        public string CaThiName { get; set; }
        public Guid? KyThiId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Stop { get; set; }

        public virtual KyThi KyThi { get; set; }
        public virtual ICollection<CaThiMonThi> CaThiMonThi { get; set; }
    }
}
