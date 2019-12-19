using System;
using System.Collections.Generic;

namespace BTLon.Models
{
    public partial class CaThiMonThi
    {
        public CaThiMonThi()
        {
            DiaDiem = new HashSet<DiaDiem>();
        }

        public Guid CaMtId { get; set; }
        public Guid? CaThiId { get; set; }
        public Guid? MonThiId { get; set; }

        public virtual CaThi CaThi { get; set; }
        public virtual MonThi MonThi { get; set; }
        public virtual ICollection<DiaDiem> DiaDiem { get; set; }
    }
}
