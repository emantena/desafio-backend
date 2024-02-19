using DeliveryApp.Domain.Dto;
using DeliveryApp.Domain.Entity;

namespace DeliveryApp.Repository.Interfaces
{
	public interface IVehicleRepository : IDisposable
	{
		Task<Vehicle> CreateAsync(Vehicle vehicle);
		Task<bool> PlateExistsAsync(string plate);
		Task<IEnumerable<VehicleDto>> ListVehiclesAsync();
		Task<VehicleDto> GetVehicleAsync(string plate);
		Task<Vehicle> GetByIdAsync(int vehicleId);
		Task<Vehicle> GetVehicleAvaliableAsync();
		Vehicle Update(Vehicle vehicle);
		void Remove(Vehicle vehicle);
	}
}