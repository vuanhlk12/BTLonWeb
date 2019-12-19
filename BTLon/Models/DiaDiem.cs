using System;
using System.Collections.Generic;

namespace BTLon.Models
{
    public partial class DiaDiem
    {
        public DiaDiem()
        {
            SvDiaDiem = new HashSet<SvDiaDiem>();
        }

        public Guid DiaDiemId { get; set; }
        public Guid? PhongThiId { get; set; }
        public Guid? CaMtId { get; set; }

        public virtual CaThiMonThi CaMt { get; set; }
        public virtual PhongThi PhongThi { get; set; }
        public virtual ICollection<SvDiaDiem> SvDiaDiem { get; set; }
    }
}
