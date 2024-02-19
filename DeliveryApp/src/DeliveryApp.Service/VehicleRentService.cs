using DeliveryApp.Domain.Entity;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.ViewModels.Request;
using DeliveryApp.Service.ViewModels.Response;
using Flunt.Notifications;
using CnhType = DeliveryApp.Domain.Enums.CnhType;

namespace DeliveryApp.Service
{
	public class VehicleRentService : IVehicleRentService
	{
		private readonly IVehicleRentRepository _repository;
		private readonly IVehicleRepository _vehicleRepository;
		private readonly IPlanVersionRepository _planRepository;
		private readonly IDeliveryManRepository _deliveryManRepository;

		public VehicleRentService(IVehicleRentRepository repository,
			IVehicleRepository vehicleRepository,
			IPlanVersionRepository planRepository,
			IDeliveryManRepository deliveryManRepository)
		{
			_repository = repository;
			_vehicleRepository = vehicleRepository;
			_planRepository = planRepository;
			_deliveryManRepository = deliveryManRepository;
		}

		public async Task<BaseResponse> CalculatePrice(int planId, DateTime startDate, DateTime returnDate)
		{
			var baseResponse = new BaseResponse();

			var totalDaysLocation = (int)returnDate.Subtract(startDate).TotalDays;

			if (totalDaysLocation < 1)
			{
				baseResponse.AddNotification(new Notification("", "Data final de locação deve ser maior que a data de inicio"));
				return baseResponse;
			}

			var plan = await _planRepository.GetPlanVersionByIdAsync(planId);

			if (plan is null)
			{
				baseResponse.AddNotification(new Notification("", "Plano não encontrado"));
				return baseResponse;
			}

			var additionalDaysLocation = totalDaysLocation - plan.MinDayRent;

			var baseCost = totalDaysLocation * plan.Price;

			if (additionalDaysLocation == 0)
			{
				baseResponse.AddValue(new PlanCostResponse(plan.Price, totalDaysLocation, dailyLateFee: 0,
					additionalCharge: 0, totalCost: baseCost));

				baseResponse.AddValue(new { totalDaysLocation, totalCost = baseCost });
			}
			else if (additionalDaysLocation > 0)
			{
				var additionalCostDaily = plan.Price + plan.DailyLateFee;
				var additionalCost = additionalDaysLocation * additionalCostDaily;

				baseResponse.AddValue(new PlanCostResponse(plan.Price, totalDaysLocation, additionalCost,
					additionalCharge: 0, totalCost: baseCost + additionalCost));
			}
			else
			{
				baseResponse.AddValue(new PlanCostResponse(plan.Price, totalDaysLocation, dailyLateFee: 0,
					plan.AdditionalCharge, totalCost: baseCost + plan.AdditionalCharge));
			}

			return baseResponse;
		}

		public async Task<bool> ExistsRent(int vehicleId)
		{
			var rent = await _repository.GetByVehicleIdAsync(vehicleId);

			return rent != null;
		}

		public async Task<BaseResponse> RentVehicleAsync(CreateRentVehicleRequest request)
		{
			var response = new BaseResponse();
			var vehicleAvailable = await _vehicleRepository.GetVehicleAvaliableAsync();

			if (vehicleAvailable is null)
			{
				response.AddNotification(new Notification("", "Não existe veículos disponíves para locação!"));
				return response;
			}

			var deliveryManValidation = await ValidateDeliveryManToRentVehicle(request.DeliveryManId);

			if (!deliveryManValidation.IsValid)
			{
				response.AddNotifications(deliveryManValidation.Messages);
				return response;
			}
			var deliveryMan = (DeliveryMan)deliveryManValidation.Value;
			var plan = await _planRepository.GetPlanVersionByIdAsync(request.PlanVersionId);

			if (plan is null)
			{
				response.AddNotification(new Notification("", "Plano não encontrado"));
				return response;
			}

			var vehicleRent = new VehicleRent(vehicleAvailable.VehicleId, request.PlanVersionId, deliveryMan.DeliveryManId,
				plan.MinDayRent);

			_ = await _repository.CreateAsync(vehicleRent);

			vehicleAvailable.IsRent = true;
			vehicleAvailable.CreateAt = DateTime.SpecifyKind(vehicleAvailable.CreateAt, DateTimeKind.Utc);
			_ = _vehicleRepository.Update(vehicleAvailable);

			return response;
		}

		private async Task<BaseResponse> ValidateDeliveryManToRentVehicle(int deliveryManId)
		{
			var deliveryMan = await _deliveryManRepository.GetByUserIdAsync(deliveryManId);
			var response = new BaseResponse();

			if (deliveryMan is null)
			{
				response.AddNotification(new Notification("", "Entregador não encontrado"));
				return response;
			}

			if (deliveryMan.CnhTypeId == (int)CnhType.B)
			{
				response.AddNotification(new Notification("", "Entregador não esta apto para alugar uma mota"));
				return response;
			}

			response.AddValue(deliveryMan);

			return response;
		}
	}
}
