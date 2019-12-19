using System;
using System.Collections.Generic;

namespace BTLon.Models
{
    public partial class SvMonThiKiThi
    {
        public Guid? UserId { get; set; }
        public Guid? KyThiId { get; set; }
        public Guid? MonThiId { get; set; }
        public bool? IsValid { get; set; }

        public virtual KyThi KyThi { get; set; }
        public virtual MonThi MonThi { get; set; }
        public virtual User User { get; set; }
    }
}
