using DeliveryApp.Domain.ValueObjects;
using DeliveryApp.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeliveryApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : BaseController
	{
		private readonly IUserService _userService;

		public UserController(ILogger<UserController> logger, IUserService userService) : base(logger)
		{
			_userService = userService;
		}

		[Authorize]
		[HttpGet, Route("profile")]
		public async Task<IActionResult> Profile()
		{
			var result = await _userService.GetUserProfileAsync(Context.UserId);

			return Response(result, HttpStatusCode.OK);
		}
	}
}
