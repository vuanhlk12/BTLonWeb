using System;
using System.Collections.Generic;

namespace BTLon.Models
{
    public partial class SvDiaDiem
    {
        public Guid? UserId { get; set; }
        public Guid? DiaDiemId { get; set; }

        public virtual DiaDiem DiaDiem { get; set; }
        public virtual User User { get; set; }
    }
}
