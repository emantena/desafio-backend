using DeliveryApp.Domain.Entity;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Repository.Interfaces.Base;
using DeliveryApp.Repository.Repositories.Base;

namespace DeliveryApp.Repository.Repositories
{
    public class VehicleModelRepository : IVehicleModelRepository
	{

		private readonly IGenericRepository<DeliveryAppContext, VehicleModel> _repository;

		public VehicleModelRepository(IGenericRepository<DeliveryAppContext, VehicleModel> repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<VehicleModel>> ListActivesModelsByBrandId(int brandId)
		{
			return await _repository.SearchAsync(x => x.VehicleBrandId == brandId && x.Active);
		}

		public async Task<bool> ModelExists(int modelId)
		{
			return await _repository.AnyAsync(x => x.VehicleModelId == modelId);
		}
	}
}
