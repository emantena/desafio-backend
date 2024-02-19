using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.ViewModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeliveryApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = "Adminstrator")]
	public class VehicleController : BaseController
	{
		private readonly IVehicleService _vehicleService;
		private readonly IVehicleBrandService _vehicleBrandService;
		private readonly IVehicleModelService _vehicleModelService;

		public VehicleController(ILogger<VehicleController> logger, IVehicleService vehicleService, IVehicleBrandService vehicleBrandService, IVehicleModelService vehicleModelService)
			: base(logger)
		{
			_vehicleService = vehicleService;
			_vehicleBrandService = vehicleBrandService;
			_vehicleModelService = vehicleModelService;
		}

		[HttpGet, Route("brand")]
		public async Task<IActionResult> Brand()
		{
			var result = await _vehicleBrandService.ListBrandsAsync();
			return Response(result, HttpStatusCode.OK);
		}

		[HttpGet, Route("brand/{brandId}/model")]
		public async Task<IActionResult> Model(int brandId)
		{
			var result = await _vehicleModelService.ListModelsAsync(brandId);
			return Response(result, HttpStatusCode.OK);
		}

		[HttpPost]
		public async Task<IActionResult> Vehicle(CreateVehicleRequest request)
		{
			var result = await _vehicleService.CreateAsync(request);
			return Response(result, HttpStatusCode.OK);
		}

		[HttpGet]
		public async Task<IActionResult> Vehicles()
		{
			var result = await _vehicleService.ListVehicles();
			return Response(result, HttpStatusCode.OK);
		}

		[HttpGet, Route("{plate}")]
		public async Task<IActionResult> Vehicles(string plate)
		{
			var result = await _vehicleService.GetVehicle(plate);
			return Response(result, HttpStatusCode.OK);
		}

		[HttpPatch]
		public async Task<IActionResult> UpdateVehicle(UpdateVehicleRequest request)
		{
			var result = await _vehicleService.UpdateVehicle(request);
			return Response(result, HttpStatusCode.NoContent);
		}

		[HttpDelete, Route("{vehicleId}")]
		public async Task<IActionResult> Delete(int vehicleId)
		{
			var response = await _vehicleService.Remove(vehicleId);
			return Response(response, HttpStatusCode.NoContent);
		}
	}
}