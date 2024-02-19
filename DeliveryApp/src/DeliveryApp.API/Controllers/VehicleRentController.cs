using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.ViewModels.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeliveryApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class VehicleRentController : BaseController
	{
		private readonly IVehicleRentService _rentService;

		public VehicleRentController(ILogger<VehicleRentController> logger, IVehicleRentService rentservice)
			: base(logger)
		{
			_rentService = rentservice;
		}

		[HttpPost]
		public async Task<IActionResult> VehicleRent(CreateRentVehicleRequest request)
		{
			var response = await _rentService.RentVehicleAsync(request);
			return Response(response, HttpStatusCode.Created);
		}

		[HttpGet, Route("price/{planId}")]
		public async Task<IActionResult> ConsultingPrice(int planId, [FromQuery] DateTime startDate, [FromQuery] DateTime returnDate)
		{
			var response = await _rentService.CalculatePrice(planId, startDate, returnDate);
			return Response(response, HttpStatusCode.Created);
		}
	}
}