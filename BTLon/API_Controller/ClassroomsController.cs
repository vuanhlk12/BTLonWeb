using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTLon.Models;
using Nancy.Json;

namespace BTLon.API_Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClassroomsController : ControllerBase
	{
		private BTLonContext db = new BTLonContext();
		[HttpGet]
		public IQueryable<PhongThi> GetAll()
		{
			IQueryable<PhongThi> temp = db.PhongThi.OrderBy(u => u.PhongThiName);
			return temp;
		}

		[HttpGet("{CaThiID}/{MonThiID}")]
		public IQueryable<PhongThi> GetByCtMt(Guid CaThiID, Guid MonThiID)
		{
			CtMtsController ctmt = new CtMtsController();
			Guid? ctmtID = ctmt.GetCtMtID(CaThiID, MonThiID);
			IQueryable<PhongThi> temp = db.PhongThi.FromSqlRaw("SELECT PhongThi.* FROM PhongThi " +
														"INNER JOIN DiaDiem ON DiaDiem.PhongThiID = PhongThi.PhongThiID " +
														$"WHERE DiaDiem.CaMtID  = '{ctmtID}'").OrderBy(u => u.PhongThiName);
			return temp;
		}

		[HttpPost("Multi")]
		public void PostMulti(List<PhongThi> PhongThi)
		{
			foreach (PhongThi a in PhongThi)
			{
				db.PhongThi.Add(a);
			}
			db.SaveChanges();
		}

		[HttpPost]
		public void Post(PhongThi PhongThi)
		{
			PhongThi.PhongThiId = Guid.NewGuid();
			db.PhongThi.Add(PhongThi);
			db.SaveChanges();
		}

		[HttpDelete("{PhongThiID}")]
		public void Delete(Guid PhongThiID)
		{
			var phongthi = db.PhongThi.Where(p => p.PhongThiId == PhongThiID).FirstOrDefault();
			db.PhongThi.Remove(phongthi);
			db.SaveChanges();
		}

	}
}

