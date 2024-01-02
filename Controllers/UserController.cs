using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleJWT.Models;

namespace SimpleJWT.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly ILogger<UserController> _logger;

		public UserManager _UserManager;
		public UserController(ILogger<UserController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		[Route("Get")]

		public IActionResult Get()
		{
			this._UserManager = new UserManager();
			return Ok(this._UserManager.GetUsers());
		}

		[HttpPost]
		[Route("Add")]

		public IActionResult AddUser(User user)
		{
			this._UserManager = new UserManager();
			return Ok(this._UserManager.AddUser(user));
		}

	}
}