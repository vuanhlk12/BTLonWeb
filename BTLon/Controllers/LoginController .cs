using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BTLon.Models;
using Microsoft.AspNetCore.Http;

namespace BTLon.Controllers
{
	public class LoginController : Controller
	{

		private readonly ILogger<LoginController> _logger;

		public LoginController(ILogger<LoginController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			if (HttpContext.Session.GetString("role") == null)
			{
				return View();
			}
			else if (HttpContext.Session.GetString("role") == "admin")
			{
				return Redirect("/Admin/");
			}
			else
			{
				return Redirect("/User/");
			}
		}
		[HttpPost]
		public ActionResult Login()
		{
			string user_name = Request.Form["username"];
			string password = Request.Form["password"];
			string role = Request.Form["role"];

			if (user_name.Trim() == "")
			{
				return Redirect("/");
			}

			if (role == "0")
			{
				using (var db = new BTLonContext())
				{
					Admin admin = db.Admin.Where(u => u.UserName.ToLower() == user_name.ToLower()).FirstOrDefault();
					if (admin == null)
					{
						return Redirect("/");
					}
					if (admin.Password == password)
					{
						HttpContext.Session.SetString("user", admin.UserName);
						HttpContext.Session.SetString("role", "admin");
						return Redirect("/Admin/");
					}
					else
					{
						return Redirect("/");
					}

				}
			}
			if (role == "1")
			{
				using (var db = new BTLonContext())
				{
					User user = db.User.FirstOrDefault(u => u.UserName.ToLower() == user_name.ToLower());
					if (user == null)
					{
						return Redirect("/");
					}
					if (user.Password == password)
					{
						HttpContext.Session.SetString("user", user.UserName);
						HttpContext.Session.SetString("userID", user.UserId.ToString());
						HttpContext.Session.SetString("CurrentKiThi", user.CurrentKiThi.ToString());
						HttpContext.Session.SetString("role", "user");
						return Redirect("/User/");
					}
					else
					{
						return Redirect("/");
					}

				}
			}
			return Redirect("/");
		}

		public ActionResult Logout()
		{
			HttpContext.Session.Clear();
			return Redirect("/");
		}
	}
}
