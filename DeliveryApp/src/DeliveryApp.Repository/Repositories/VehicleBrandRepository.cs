using DeliveryApp.Domain.Entity;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Repository.Interfaces.Base;
using DeliveryApp.Repository.Repositories.Base;

namespace DeliveryApp.Repository.Repositories
{
    public class VehicleBrandRepository : IVehicleBrandRepository
	{

		private readonly IGenericRepository<DeliveryAppContext, VehicleBrand> _repository;

		public VehicleBrandRepository(IGenericRepository<DeliveryAppContext, VehicleBrand> repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<VehicleBrand>> ListActivesBrandsAsync()
		{
			return await _repository.SearchAsync(x => x.Active);
		}
	}
}
