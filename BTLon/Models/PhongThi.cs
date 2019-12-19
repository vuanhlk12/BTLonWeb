using System;
using System.Collections.Generic;

namespace BTLon.Models
{
    public partial class PhongThi
    {
        public PhongThi()
        {
            DiaDiem = new HashSet<DiaDiem>();
        }

        public Guid PhongThiId { get; set; }
        public string PhongThiName { get; set; }
        public int? ComputerNumber { get; set; }

        public virtual ICollection<DiaDiem> DiaDiem { get; set; }
    }
}
