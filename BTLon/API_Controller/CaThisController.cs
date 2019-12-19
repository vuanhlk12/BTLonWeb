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
	public class CaThisController : ControllerBase
	{
		private BTLonContext db = new BTLonContext();
		[HttpGet]
		public IQueryable<CaThi> GetAll()
		{
			IQueryable<CaThi> temp = db.CaThi.OrderBy(u => u.CaThiId);
			return temp;
		}

		[HttpGet("{KyThiID}")]
		public IQueryable<CaThi> GetByKyThiID(Guid KyThiID)
		{
			IQueryable<CaThi> temp = db.CaThi.Where(p=>p.KyThiId == KyThiID).OrderBy(u => u.CaThiIdFake);
			return temp;
		}

		[HttpPost]
		public void Post(CaThi cathi)
		{
			cathi.CaThiId = Guid.NewGuid();
			db.CaThi.Add(cathi);
			db.SaveChanges();
		}

		[HttpDelete("{CaThiID}")]
		public void Delete(Guid CaThiID)
		{
			var cathi = db.CaThi.Where(p => p.CaThiId == CaThiID).FirstOrDefault();
			db.CaThi.Remove(cathi);
			db.SaveChanges();
		}

	}
}

