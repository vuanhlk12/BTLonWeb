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
	public class KyThisController : ControllerBase
	{
		private BTLonContext db = new BTLonContext();
		private JavaScriptSerializer Serializer = new JavaScriptSerializer();
		[HttpGet]
		public IQueryable<KyThi> GetAll()
		{
			IQueryable<KyThi> temp = db.KyThi.OrderBy(u => u.KyThiIdFake);
			return temp;
		}

		[HttpPost]
		public void Post(KyThi kythi)
		{
			kythi.KyThiId = Guid.NewGuid();
			db.KyThi.Add(kythi);
			db.SaveChanges();
		}

		[HttpDelete("{KyThiId}")]
		public void Delete(Guid KyThiId)
		{
			var kythi = db.KyThi.Where(p => p.KyThiId == KyThiId).FirstOrDefault();
			db.KyThi.Remove(kythi);
			db.SaveChanges();
		}

		[HttpPut("SetKyThi/{KyThiID}")]
		public void SetKyThi(Guid? KyThiID)
		{
			var ListUser = db.User;
			foreach (User user in ListUser)
			{
				user.CurrentKiThi = KyThiID;
			}
			db.SaveChanges();
		}

	}
}

