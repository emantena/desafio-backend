using DeliveryApp.Domain.Entity;

namespace DeliveryApp.Repository.Interfaces
{
	public interface IVehicleRentRepository
	{
		Task<VehicleRent> GetByVehicleIdAsync(int vehicleId);
		Task<VehicleRent> CreateAsync(VehicleRent vehicleRent);
	}
}
