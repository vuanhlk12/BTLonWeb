using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BTLon.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace BTLon.Controllers
{
	public class KyThiController : Controller
	{
		private readonly ILogger<KyThiController> _logger;

		public KyThiController(ILogger<KyThiController> logger)
		{
			_logger = logger;
		}

		[HttpPost]

		public IActionResult Post(KyThi kythi)
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
