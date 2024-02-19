using DeliveryApp.Domain.Entity;
using DeliveryApp.Service.ViewModels.Request;
using DeliveryApp.Service.ViewModels.Response;

namespace DeliveryApp.Service.Interfaces
{
	public interface IVehicleService : IDisposable
	{
		Task<BaseResponse> CreateAsync(CreateVehicleRequest request);
		Task<BaseResponse> ListVehicles();
		Task<BaseResponse> GetVehicle(string plate);
		Task<BaseResponse> UpdateVehicle(UpdateVehicleRequest request);
		Task<BaseResponse> Remove(int vehicleId);
		Task<Vehicle> GetVehicleAvaliableToRent();
	}
}