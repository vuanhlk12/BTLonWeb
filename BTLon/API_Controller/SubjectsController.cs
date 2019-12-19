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
	public class SubjectsController : ControllerBase
	{
		private BTLonContext db = new BTLonContext();
		private JavaScriptSerializer Serializer = new JavaScriptSerializer();
		[HttpGet]
		public IQueryable<MonThi> GetAll()
		{
			IQueryable<MonThi> temp = db.MonThi.OrderBy(u => u.MonThiIdFake);
			return temp;
		}

		[HttpGet("UserMonThi")]
		public List<MonThiExtend> GetByUser()
		{
			Guid? UserID = Guid.Parse(HttpContext.Session.GetString("userID"));
			Guid? KyThiID = Guid.Parse(HttpContext.Session.GetString("CurrentKiThi"));
			var items = (from monthi in db.MonThi
						 join SvMtKt in db.SvMonThiKiThi
						 on monthi.MonThiId equals SvMtKt.MonThiId
						 where SvMtKt.UserId == UserID && SvMtKt.KyThiId == KyThiID
						 select new
						 {
							 MonThiId = monthi.MonThiId,
							 MonThiIdFake = monthi.MonThiIdFake,
							 MonThiName = monthi.MonThiName,
							 GiaoVien = monthi.GiaoVien,
							 IsValid = SvMtKt.IsValid
						 }).OrderBy(u => u.MonThiIdFake).ToList();
			return (List<MonThiExtend>)Serializer.Deserialize<List<MonThiExtend>>(Serializer.Serialize(items));
		}

		[HttpGet("GetMonThiByKyThi/{KyThiID}/{UserID}")]
		public List<MonThiExtend> GetMonThiByKyThi(Guid? KyThiID, Guid? UserID)
		{
			var items = (from monthi in db.MonThi
						 join SvMtKt in db.SvMonThiKiThi
						 on monthi.MonThiId equals SvMtKt.MonThiId
						 where SvMtKt.UserId == UserID && SvMtKt.KyThiId == KyThiID
						 select new
						 {
							 MonThiId = monthi.MonThiId,
							 MonThiIdFake = monthi.MonThiIdFake,
							 MonThiName = monthi.MonThiName,
							 GiaoVien = monthi.GiaoVien,
							 IsValid = SvMtKt.IsValid
						 }).OrderBy(u => u.MonThiIdFake).ToList();
			return (List<MonThiExtend>)Serializer.Deserialize<List<MonThiExtend>>(Serializer.Serialize(items));
		}

		[HttpGet("{CaThiID}")]
		public IQueryable<MonThi> GetByCaThiID(Guid CaThiID)
		{
			string query = "SELECT MonThi.* FROM MonThi " +
														"INNER JOIN CaThi_MonThi ON MonThi.MonThiID = CaThi_MonThi.MonThiID " +
														"INNER JOIN CaThi ON CaThi_MonThi.CaThiID = CaThi.CaThiID " +
														$"WHERE CaThi.CaThiID ='{CaThiID}'";
			IQueryable<MonThi> temp = db.MonThi.FromSqlRaw(query).OrderBy(u => u.MonThiIdFake);
			return temp;
		}

		[HttpPost]
		public void Post([FromBody]Object _MonThi)
		{
			MonThi MonThi = (MonThi)Serializer.Deserialize<MonThi>(_MonThi.ToString());
			MonThi.MonThiId = Guid.NewGuid();
			db.MonThi.Add(MonThi);
			db.SaveChanges();
		}

		[HttpDelete("{MonThiID}")]
		public void Delete(Guid MonThiID)
		{
			var monthi = db.MonThi.Where(p => p.MonThiId == MonThiID).FirstOrDefault();
			db.MonThi.Remove(monthi);
			db.SaveChanges();
		}

	}
}

