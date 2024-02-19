using DeliveryApp.Domain.Entity;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.ViewModels.Request;
using DeliveryApp.Service.ViewModels.Response;
using Flunt.Notifications;

namespace DeliveryApp.Service
{
	public class VehicleService : IVehicleService
	{
		private readonly IVehicleRepository _repository;
		private readonly IVehicleModelService _vehicleModelService;
		private readonly IVehicleRentService _vehicleRentService;

		public VehicleService(IVehicleRepository vehicleRepository,
			IVehicleModelService vehicleModelService,
			IVehicleRentService vehicleRentService
		)
		{
			_repository = vehicleRepository;
			_vehicleModelService = vehicleModelService;
			_vehicleRentService = vehicleRentService;
		}

		public async Task<BaseResponse> CreateAsync(CreateVehicleRequest request)
		{
			var response = new BaseResponse();
			var vehicle = new Vehicle(vehicleId: 0, request.ModelId, request.Plate, request.YearManufacture);

			if (!vehicle.IsValid)
			{
				response.AddNotifications(vehicle.Notifications);
				return response;
			}

			if (await _repository.PlateExistsAsync(vehicle.Plate))
			{
				response.AddNotification(new Notification("Placa", "Placa já esta cadastrada para outro veículo"));
				return response;
			}

			if (!await _vehicleModelService.ModelExists(request.ModelId))
			{
				response.AddNotification(new Notification("Modelo", "Modelo não encontrado"));
				return response;
			}
			vehicle = await _repository.CreateAsync(vehicle);

			response.AddValue(new CreateVehicleResponse(vehicle.Plate, vehicle.VehicleModelId, vehicle.YearManufacture, vehicle.CreateAt));
			return response;
		}


		public async Task<BaseResponse> ListVehicles()
		{
			var vehicles = await _repository.ListVehiclesAsync();

			var response = new BaseResponse();
			response.AddValue(vehicles);

			return response;
		}

		public async Task<BaseResponse> GetVehicle(string plate)
		{
			var vehicles = await _repository.GetVehicleAsync(plate);

			var response = new BaseResponse();
			response.AddValue(vehicles);

			return response;
		}

		public async Task<BaseResponse> UpdateVehicle(UpdateVehicleRequest request)
		{
			var response = new BaseResponse();
			var vehicle = await _repository.GetByIdAsync(request.VehicleId);

			if (vehicle is null)
			{
				response.AddNotification(new Notification("", "Veículo não localizado"));
				return response;
			}

			vehicle.Plate = request.Plate;
			vehicle.CreateAt = DateTime.SpecifyKind(vehicle.CreateAt, DateTimeKind.Utc);

			if (!vehicle.IsValid)
			{
				response.AddNotifications(vehicle.Notifications);
				return response;
			}

			if (await _repository.PlateExistsAsync(vehicle.Plate))
			{
				response.AddNotification(new Notification("", "Placa já cadastrada para outro veículo"));
				return response;
			}

			_ = _repository.Update(vehicle);

			return response;
		}

		public async Task<BaseResponse> Remove(int vehicleId)
		{
			var response = new BaseResponse();
			var vehicle = await _repository.GetByIdAsync(vehicleId);

			if (vehicle is null)
			{
				response.AddNotification(new Notification("", "Veículo não localizado"));
				return response;
			}

			if (await _vehicleRentService.ExistsRent(vehicleId))
			{
				response.AddNotification(new Notification("", "Veículo não pode ser excluído"));
				return response;
			}

			_repository.Remove(vehicle);

			return response;
		}

		public async Task<Vehicle> GetVehicleAvaliableToRent()
		{
			return await _repository.GetVehicleAvaliableAsync();
		}

		#region Disposable Members

		private bool _disposed;

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (_disposed || !disposing)
			{
				return;
			}

			_repository?.Dispose();

			_disposed = true;
		}
		#endregion
	}
}