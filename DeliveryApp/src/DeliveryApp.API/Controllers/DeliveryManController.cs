using DeliveryApp.Domain.ValueObjects;
using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.ViewModels.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeliveryApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DeliveryManController : BaseController
	{
		private readonly IDeliveryManService _deliveryManService;

		public DeliveryManController(ILogger<DeliveryManController> logger, IDeliveryManService deliveryManService)
			: base(logger)
		{
			_deliveryManService = deliveryManService;
		}

		[HttpPost]
		public async Task<IActionResult> DeliveryMan(CreateDeliverymanRequest request)
		{
			var response = await _deliveryManService.CreateAsync(request);
			return Response(response, HttpStatusCode.Created);
		}

		[HttpPost, Route("{deliveryManId}/upload/document/cnh")]
		public async Task<IActionResult> UploadFile(IFormFile file, int deliveryManId)
		{
			if (deliveryManId != Context.UserId)
			{
				return BadRequest("Arquivo não enviado.");
			}

			if (file == null || file.Length == 0)
			{
				return BadRequest("Arquivo não enviado.");
			}

			byte[] fileBytes;

			using (var memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream);
				fileBytes = memoryStream.ToArray();
			}

			var response = await _deliveryManService.SendDocumentImageAsync(fileBytes, Context.UserId);

			return Response(response, HttpStatusCode.NoContent);
		}
	}
}
