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
	public class DiaDiemsController : ControllerBase
	{
		private BTLonContext db = new BTLonContext();
		private JavaScriptSerializer Serializer = new JavaScriptSerializer();
		[HttpGet]
		public IQueryable<DiaDiem> GetAll()
		{
			IQueryable<DiaDiem> temp = db.DiaDiem.OrderBy(u => u.DiaDiemId);
			return temp;
		}

		[HttpGet("GetByMonThi/{MonThiID}")]
		public List<DiaDiemExtend> GetByMonThi(Guid? MonThiID)
		{
			Guid? KyThiID = Guid.Parse(HttpContext.Session.GetString("CurrentKiThi"));
			Guid? UserID = Guid.Parse(HttpContext.Session.GetString("userID"));
			bool? isValid = db.SvMonThiKiThi.Where(u => u.MonThiId == MonThiID && u.KyThiId == KyThiID && u.UserId == UserID).FirstOrDefault().IsValid;
			if ((bool)isValid)
			{
				var DiaDiem = (from cathi in db.CaThi
							   join cathimonthi in db.CaThiMonThi
							   on cathi.CaThiId equals cathimonthi.CaThiId
							   join monthi in db.MonThi
							   on cathimonthi.MonThiId equals monthi.MonThiId
							   join diadiem in db.DiaDiem
							   on cathimonthi.CaMtId equals diadiem.CaMtId
							   join phongthi in db.PhongThi
							   on diadiem.PhongThiId equals phongthi.PhongThiId
							   where monthi.MonThiId == MonThiID && cathi.KyThiId == KyThiID
							   select new
							   {
								   DiaDiemId = diadiem.DiaDiemId,
								   CaThiIdFake = cathi.CaThiIdFake,
								   CaThiName = cathi.CaThiName,
								   Date = cathi.Date,
								   Start = cathi.Start,
								   PhongThiName = phongthi.PhongThiName,
								   ComputerNumber = phongthi.ComputerNumber
							   }).OrderBy(u => u.CaThiIdFake).ToList();
				return (List<DiaDiemExtend>)Serializer.Deserialize<List<DiaDiemExtend>>(Serializer.Serialize(DiaDiem));
			}
			else return null;

		}

		[HttpGet("GetDetail")]
		public List<Detail> GetDetail(Guid? MonThiID)
		{
			Guid? UserID = Guid.Parse(HttpContext.Session.GetString("userID"));
			var Detail = (from cathi in db.CaThi
						  join cathimonthi in db.CaThiMonThi
						  on cathi.CaThiId equals cathimonthi.CaThiId
						  join monthi in db.MonThi
						  on cathimonthi.MonThiId equals monthi.MonThiId
						  join diadiem in db.DiaDiem
						  on cathimonthi.CaMtId equals diadiem.CaMtId
						  join phongthi in db.PhongThi
						  on diadiem.PhongThiId equals phongthi.PhongThiId
						  join SvDd in db.SvDiaDiem
						  on diadiem.DiaDiemId equals SvDd.DiaDiemId
						  where SvDd.UserId == UserID
						  select new
						  {
							  MonThiIdFake = monthi.MonThiIdFake,
							  monThiName = monthi.MonThiName,
							  giaoVien = monthi.GiaoVien,
							  DiaDiemId = diadiem.DiaDiemId,
							  CaThiIdFake = cathi.CaThiIdFake,
							  CaThiName = cathi.CaThiName,
							  Date = cathi.Date,
							  Start = cathi.Start,
							  PhongThiName = phongthi.PhongThiName,
							  ComputerNumber = phongthi.ComputerNumber
						  }).OrderBy(u => u.CaThiIdFake).ToList();
			return (List<Detail>)Serializer.Deserialize<List<Detail>>(Serializer.Serialize(Detail));
		}


		[HttpPost("AddUser/{DiaDiemID}")]
		public void AddUser(Guid DiaDiemID)
		{
			string querry = "SELECT phongthi.* FROM phongthi " +
				"INNER JOIN DiaDiem ON PhongThi.PhongThiID = DiaDiem.PhongThiID " +
				$"WHERE DiaDiem.DiaDiemID = '{DiaDiemID}'";
			var phongthi = db.PhongThi.FromSqlRaw(querry).FirstOrDefault();
			int? MaxNumber = phongthi.ComputerNumber;
			var list = db.SvDiaDiem.Where(u => u.DiaDiemId == DiaDiemID);
			int? currentNumber = list.Count();
			if (currentNumber < MaxNumber)
			{
				Guid? UserID = Guid.Parse(HttpContext.Session.GetString("userID"));
				bool isValid = list.Where(u => u.UserId == UserID).Count() == 0;
				if (isValid)
				{
					SvDiaDiem SvDiaDiem = new SvDiaDiem();
					SvDiaDiem.UserId = UserID;
					SvDiaDiem.DiaDiemId = DiaDiemID;
					db.SvDiaDiem.Add(SvDiaDiem);
					db.SaveChanges();
				}
			}
		}

		[HttpDelete("{CaThiID}/{MonThiID}/{PhongThiID}")]
		public void Delete(Guid CaThiID, Guid MonThiID, Guid PhongThiID)
		{
			CtMtsController ctmt = new CtMtsController();
			Guid? ctmtID = ctmt.GetCtMtID(CaThiID, MonThiID);
			DiaDiem diadiem = db.DiaDiem.Where(u => u.CaMtId == ctmtID && u.PhongThiId == PhongThiID).FirstOrDefault();
			db.DiaDiem.Remove(diadiem);
			db.SaveChanges();
		}

		[HttpPost]
		public void Post([FromBody]Object _DiaDiem)
		{
			DiaDiem DiaDiem = (DiaDiem)Serializer.Deserialize<DiaDiem>(_DiaDiem.ToString());
			bool isValid = db.DiaDiem.Where(u => u.CaMtId == DiaDiem.CaMtId && u.PhongThiId == DiaDiem.PhongThiId).Count() == 0;
			if (isValid)
			{
				DiaDiem.DiaDiemId = Guid.NewGuid();
				db.DiaDiem.Add(DiaDiem);
				db.SaveChanges();
			}
		}

		[HttpPost("{CaThiID}/{MonThiID}/{PhongThiID}")]
		public void Add(Guid CaThiID, Guid MonThiID, Guid PhongThiID)
		{
			CtMtsController ctmt = new CtMtsController();
			Guid? ctmtID = ctmt.GetCtMtID(CaThiID, MonThiID);
			bool isValid = db.DiaDiem.Where(u => u.CaMtId == ctmtID && u.PhongThiId == PhongThiID).Count() == 0;
			if (isValid)
			{
				DiaDiem diadiem = new DiaDiem();
				diadiem.DiaDiemId = Guid.NewGuid();
				diadiem.CaMtId = ctmtID;
				diadiem.PhongThiId = PhongThiID;
				db.DiaDiem.Add(diadiem);
				db.SaveChanges();
			}
		}

		public Guid? GetDiaDiemID(Guid? PhongThiID, Guid? CaMtID)
		{
			return db.DiaDiem.Where(u => u.PhongThiId == PhongThiID && u.CaMtId == CaMtID).FirstOrDefault().DiaDiemId;
		}

	}
}

