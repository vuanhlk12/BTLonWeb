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
	public class SvController : ControllerBase
	{
		private BTLonContext db = new BTLonContext();
		private JavaScriptSerializer Serializer = new JavaScriptSerializer();
		[HttpGet]
		public IQueryable<User> GetAll()
		{
			IQueryable<User> temp = db.User.OrderBy(u => u.UserIdfake);
			return temp;
		}

		[HttpGet("GetByDiaDiem/{CaThiID}/{MonThiID}/{PhongThiID}")]
		public IQueryable<User> GetByDiaDiem(Guid CaThiID, Guid MonThiID, Guid PhongThiID)
		{
			CtMtsController ctmt = new CtMtsController();
			Guid? camtid = ctmt.GetCtMtID(CaThiID, MonThiID);
			DiaDiemsController diadiem = new DiaDiemsController();
			Guid? diadiemid = diadiem.GetDiaDiemID(PhongThiID, camtid);
			string query = "SELECT [User].* FROM [User] " +
				"INNER JOIN SV_DiaDiem ON SV_DiaDiem.UserID = [User].UserID " +
				$"WHERE SV_DiaDiem.DiaDiemID = '{diadiemid}'";
			IQueryable<User> user = db.User.FromSqlRaw(query);
			return user;
		}

		[HttpPost]
		public void Post([FromBody]Object _User)
		{
			User user = (User)Serializer.Deserialize<User>(_User.ToString());
			user.UserId = Guid.NewGuid();
			db.User.Add(user);
			db.SaveChanges();
		}

		[HttpPost("PostMulti")]
		public void PostMulti(List<User> _User)
		{
			foreach(User user in _User)
			{
				user.UserId = Guid.NewGuid();
				db.User.Add(user);
			}
			db.SaveChanges();
		}

		[HttpPost("PostSubjectForStudent")]
		public void PostSubjectForStudent(List<SvMonThiKiThi> SvMonThiKiThi)
		{
			foreach (SvMonThiKiThi a in SvMonThiKiThi)
			{
				db.SvMonThiKiThi.Add(a);
			}
			db.SaveChanges();
		}

		[HttpDelete("{UserID}")]
		public void Delete(Guid UserID)
		{
			var user = db.User.Where(p => p.UserId == UserID).FirstOrDefault();
			db.User.Remove(user);
			db.SaveChanges();
		}

	}
}

