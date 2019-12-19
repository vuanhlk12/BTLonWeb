using System;
using System.Collections.Generic;
using System.Linq;

namespace BTLon.Models
{
	public partial class DiaDiemExtend : DiaDiem
	{
		private BTLonContext db = new BTLonContext();
		public string PhongThiName { get; set; }
		public int? ComputerNumber { get; set; }
		public string CaThiIdFake { get; set; }
		public string CaThiName { get; set; }
		public DateTime? Date { get; set; }
		public DateTime? Start { get; set; }
		public int Enrolled { get { return this.db.SvDiaDiem.Where(u => u.DiaDiemId == this.DiaDiemId).Count(); } }
		public string Count => _ = this.Enrolled.ToString() + '/' + ComputerNumber.ToString();
	}
}
