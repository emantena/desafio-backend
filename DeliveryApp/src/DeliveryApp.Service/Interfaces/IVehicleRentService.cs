using DeliveryApp.Service.ViewModels.Request;
using DeliveryApp.Service.ViewModels.Response;

namespace DeliveryApp.Service.Interfaces
{
	public interface IVehicleRentService
	{
		Task<bool> ExistsRent(int vehicleId);
		Task<BaseResponse> RentVehicleAsync(CreateRentVehicleRequest request);
		Task<BaseResponse> CalculatePrice(int planId, DateTime startDate, DateTime returnDate);
	}
}
