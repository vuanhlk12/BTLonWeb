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
	public class CtMtsController : ControllerBase
	{
		private BTLonContext db = new BTLonContext();
		[HttpGet]
		public IQueryable<CaThiMonThi> GetAll()
		{
			IQueryable<CaThiMonThi> temp = db.CaThiMonThi;
			return temp;
		}

		[HttpGet("{CaThiID}/{MonThiID}")]
		public Guid? GetCtMtID(Guid CaThiID, Guid MonThiID)
		{
			CaThiMonThi temp = db.CaThiMonThi.Where(p => p.CaThiId == CaThiID && p.MonThiId == MonThiID).FirstOrDefault();
			return temp.CaMtId;
		}

		[HttpPost]
		public void Post(CaThiMonThi CaThiMonThi)
		{
			bool isValid = db.CaThiMonThi.Where(u => u.MonThiId == CaThiMonThi.MonThiId && u.CaThiId == CaThiMonThi.CaThiId).Count() == 0;
			if (isValid)
			{
				CaThiMonThi.CaMtId = Guid.NewGuid();
				db.CaThiMonThi.Add(CaThiMonThi);
				db.SaveChanges();
			}
		}

		[HttpDelete()]
		public void Delete(CaThiMonThi CaThiMonThi)
		{
			var ctmt = db.CaThiMonThi.Where(p => p.CaThiId == CaThiMonThi.CaThiId && p.MonThiId == CaThiMonThi.MonThiId).FirstOrDefault();
			db.CaThiMonThi.Remove(ctmt);
			db.SaveChanges();
		}

	}
}

