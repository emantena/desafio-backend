using DeliveryApp.Domain.Entity;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Repository.Interfaces.Base;
using DeliveryApp.Repository.Repositories.Base;

namespace DeliveryApp.Repository.Repositories
{
	public class DeliveryManRepository : IDeliveryManRepository
	{
		private readonly IGenericRepository<DeliveryAppContext, DeliveryMan> _repository;

		public DeliveryManRepository(IGenericRepository<DeliveryAppContext, DeliveryMan> repository)
		{
			_repository = repository;
		}

		public async Task<bool> CnhExistsAsync(string cnh)
		{
			return await _repository.AnyAsync(x => x.CNH == cnh);
		}

		public async Task<bool> CnpjExistsAsync(string cnpj)
		{
			return await _repository.AnyAsync(x => x.CNPJ == cnpj);
		}

		public async Task<DeliveryMan> CreateAsync(DeliveryMan deliveryman)
		{
			return await _repository.AddAsync(deliveryman);
		}

		public async Task<DeliveryMan> GetByIdAsync(int deliveryManId)
		{
			return await _repository.FirstOrDefaultAsync(x => x.DeliveryManId == deliveryManId);
		}

		public async Task<DeliveryMan> GetByUserIdAsync(int userId)
		{
			return await _repository.FirstOrDefaultAsync(x => x.UserId == userId);
		}

		public void Update(DeliveryMan deliveryMan)
		{
			_repository.Update(deliveryMan);
		}
	}
}
