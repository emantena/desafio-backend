using DeliveryApp.Domain.Dto;
using DeliveryApp.Domain.Entity;

namespace DeliveryApp.Repository.Interfaces
{
	public interface IOrderRepository
	{
		Task<Order> CreateAsync(Order order);
		Task<Order> GetOrderByIdAsync(int orderId);
		Task<IEnumerable<OrderDto>> GetOrderByUserIdAsync(int userId);
		Task<IEnumerable<OrderDto>> GetOrderAsync();
		void Update(Order order);
	}
}
