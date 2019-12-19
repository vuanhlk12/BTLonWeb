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
	public class AdminController : Controller
	{
		private readonly ILogger<AdminController> _logger;

		public AdminController(ILogger<AdminController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			if (HttpContext.Session.GetString("role") == "admin")
			{
				return View();
			}
			return Redirect("/");
		}

		public IActionResult Student()
		{
			if (HttpContext.Session.GetString("role") == "admin")
			{
				BTLonContext db = new BTLonContext();
				ViewBag.ListKyThi = db.KyThi.ToList();
				return View();
			}
			return Redirect("/");
		}

		public IActionResult Subject()
		{
			if (HttpContext.Session.GetString("role") == "admin")
			{
				return View();
			}
			return Redirect("/");
		}

		public IActionResult Classroom()
		{
			if (HttpContext.Session.GetString("role") == "admin")
			{
				return View();
			}
			return Redirect("/");
		}

		public IActionResult Privacy()
		{
			if (HttpContext.Session.GetString("role") == "admin")
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
