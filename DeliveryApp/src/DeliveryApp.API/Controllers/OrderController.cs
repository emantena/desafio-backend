using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.ViewModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeliveryApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : BaseController
	{
		private readonly IOrderService _orderService;
		public OrderController(ILogger<OrderController> logger, IOrderService orderService) : base(logger)
		{
			_orderService = orderService;
		}

		[HttpPost]
		[Authorize(Roles = "Adminstrator")]
		public async Task<IActionResult> CreateAsync(OrderRequest request)
		{
			var response = await _orderService.CreateOrderAsync(request);
			return Response(response, HttpStatusCode.Created);
		}

		[HttpGet]
		[Authorize(Roles = "Adminstrator")]
		public async Task<IActionResult> LoadOrders()
		{
			var response = await _orderService.LoadAllOrdersAsync();
			return Response(response, HttpStatusCode.OK);
		}

		[HttpPost, Route("accept")]
		[Authorize(Roles = "DeliveryMan")]
		public async Task<IActionResult> AcceptOrder(AcceptOrderRequest request)
		{
			var response = await _orderService.AcceptOrderAsync(request);
			return Response(response, HttpStatusCode.OK);
		}

		[HttpGet, Route("my-orders")]
		[Authorize(Roles = "DeliveryMan")]
		public async Task<IActionResult> LoadOrder()
		{
			var response = await _orderService.LoadOrdersAsync();
			return Response(response, HttpStatusCode.OK);
		}

		[HttpPatch, Route("finish")]
		[Authorize(Roles = "DeliveryMan")]
		public async Task<IActionResult> FinishOrder(AcceptOrderRequest request)
		{
			var response = await _orderService.FinishOrderAsync(request);
			return Response(response, HttpStatusCode.NoContent);
		}
	}
}
