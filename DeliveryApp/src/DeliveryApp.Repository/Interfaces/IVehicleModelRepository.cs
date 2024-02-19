using DeliveryApp.Domain.Entity;

namespace DeliveryApp.Repository.Interfaces
{
	public interface IVehicleModelRepository
	{
		Task<IEnumerable<VehicleModel>> ListActivesModelsByBrandId(int brandId);
		Task<bool> ModelExists(int modelId);
	}
}