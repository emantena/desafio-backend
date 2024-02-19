using DeliveryApp.Domain.Entity;
using DeliveryApp.Domain.Enums;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.Interfaces.Storage;
using DeliveryApp.Service.ViewModels.Request;
using DeliveryApp.Service.ViewModels.Response;
using Flunt.Notifications;

namespace DeliveryApp.Service
{
	public class DeliveryManService : IDeliveryManService
	{
		private readonly IDeliveryManRepository _repository;
		private readonly IFireBaseStorage _storage;
		private readonly IUserService _userService;

		public DeliveryManService(IDeliveryManRepository repository, IFireBaseStorage storage, IUserService userService)
		{
			_repository = repository;
			_storage = storage;
			_userService = userService;
		}

		public async Task<BaseResponse> CreateAsync(CreateDeliverymanRequest request)
		{
			var response = new BaseResponse();

			var userResponse = await _userService.CreateUserAsync(new User((int)Role.DeliveryMan, request.Name, request.Email, request.Password));

			if (!userResponse.IsValid)
			{
				response.AddNotifications(userResponse.Messages);
				return response;
			}

			var user = (User)userResponse.Value;
			var deliveryMan = new DeliveryMan(request.Name, request.BirthDate, request.Cnh, request.Cnpj, request.CnhType, user.UserId);

			if (!deliveryMan.IsValid)
			{
				response.AddNotifications(deliveryMan.Notifications);
				return response;
			}

			if (await _repository.CnpjExistsAsync(deliveryMan.CNPJ))
			{
				response.AddNotification(new Notification("CNPJ", "Cnpj já cadastrado"));
				return response;
			}

			if (await _repository.CnhExistsAsync(deliveryMan.CNH))
			{
				response.AddNotification(new Notification("Cnh", "Cnh já informada"));
				return response;
			}

			deliveryMan = await _repository.CreateAsync(deliveryMan);

			response.AddValue(new CreateDeliverymanResponse(deliveryMan.DeliveryManId, deliveryMan.Name, deliveryMan.BirthDate, deliveryMan.CNH, deliveryMan.CNPJ, deliveryMan.CnhTypeId));
			return response;
		}

		public async Task<BaseResponse> SendDocumentImageAsync(byte[] fileBytes, int deliveryManId)
		{
			var response = new BaseResponse();

			var deliveryMan = await _repository.GetByIdAsync(deliveryManId);

			if (deliveryMan is null)
			{
				response.AddNotification(new Notification("", "Entregador não encontrado"));
				return response;
			}

			var url = await _storage
				.AddBucket("document_image")
				.UploadFileAsync(fileBytes, Guid.NewGuid().ToString());

			deliveryMan.CnhImage = url;
			deliveryMan.BirthDate = DateTime.SpecifyKind(deliveryMan.BirthDate, DateTimeKind.Utc);
			_repository.Update(deliveryMan);

			return response;
		}
	}
}