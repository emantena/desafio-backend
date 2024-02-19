using DeliveryApp.Domain.Entity;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Repository.Interfaces.Base;
using DeliveryApp.Repository.Repositories.Base;

namespace DeliveryApp.Repository.Repositories
{
	public class VehicleRentRepository : IVehicleRentRepository
	{
		private readonly IGenericRepository<DeliveryAppContext, VehicleRent> _repository;

		public VehicleRentRepository(IGenericRepository<DeliveryAppContext, VehicleRent> repository)
		{
			_repository = repository;
		}

		public Task<VehicleRent> GetByVehicleIdAsync(int vehicleId)
		{
			return _repository.FirstOrDefaultAsync(x => x.VehicleId == vehicleId);
		}

		public async Task<VehicleRent> CreateAsync(VehicleRent vehicleRent)
		{
			return await _repository.AddAsync(vehicleRent);
		}
	}
}
