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
	public class UserController : Controller
	{
		private readonly ILogger<UserController> _logger;

		public UserController(ILogger<UserController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			if (HttpContext.Session.GetString("role") == "user")
			{
				Guid? KyThiID = Guid.Parse(HttpContext.Session.GetString("CurrentKiThi"));
				BTLonContext db = new BTLonContext();
				string KiThiName = db.KyThi.Where(u => u.KyThiId == KyThiID).FirstOrDefault().KyThiName;
				ViewBag.KiThiName = KiThiName;
				return View();
			}
			return Redirect("/");
		}

		public IActionResult Result()
		{
			if (HttpContext.Session.GetString("role") == "user")
			{
				return View();
			}
			return Redirect("/");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
