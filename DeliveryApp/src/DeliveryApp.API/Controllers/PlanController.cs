using DeliveryApp.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeliveryApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlanController : BaseController
	{
		private readonly IPlanService _planService;


		public PlanController(ILogger<PlanController> logger, IPlanService planService) : base(logger)
		{
			_planService = planService;
		}

		[HttpGet, Route("active")]
		public async Task<IActionResult> ActivePlan()
		{
			var result = await _planService.ListActivePlansAsync();
			return Response(result, HttpStatusCode.OK);
		}

		[HttpGet]
		public async Task<IActionResult> Plan()
		{
			var result = await _planService.ListPlansAsync();
			return Response(result, HttpStatusCode.OK);
		}
	}
}
