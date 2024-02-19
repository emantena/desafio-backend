using DeliveryApp.Domain.Entity;

namespace DeliveryApp.Repository.Interfaces
{
	public interface IDeliveryManRepository
	{
		public Task<DeliveryMan> CreateAsync(DeliveryMan deliveryman);
		public Task<bool> CnpjExistsAsync(string cnpj);
		public Task<bool> CnhExistsAsync(string cnh);
		Task<DeliveryMan> GetByIdAsync(int deliveryManId);
		Task<DeliveryMan> GetByUserIdAsync(int userId);
		void Update(DeliveryMan deliveryMan);
	}
}
