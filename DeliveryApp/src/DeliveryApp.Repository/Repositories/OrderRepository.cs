using Dapper;
using DeliveryApp.Domain.Dto;
using DeliveryApp.Domain.Entity;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Repository.Interfaces.Base;
using DeliveryApp.Repository.Repositories.Base;

namespace DeliveryApp.Repository.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		public readonly IGenericRepository<DeliveryAppContext, Order> _repository;

		public OrderRepository(IGenericRepository<DeliveryAppContext, Order> repository)
		{
			_repository = repository;
		}

		public async Task<Order> CreateAsync(Order order)
		{
			return await _repository.AddAsync(order);
		}

		public async Task<Order> GetOrderByIdAsync(int orderId)
		{
			return await _repository.FirstOrDefaultAsync(x => x.OrderId == orderId);
		}

		public void Update(Order order)
		{
			_repository.Update(order);
		}

		public async Task<IEnumerable<OrderDto>> GetOrderByUserIdAsync(int userId)
		{
			using var connection = _repository.GetDbConnection();

			var query = @"select o.""OrderId""
						, o.""RacePrice""
						, os.""Name"" AS Status
					from public.""Order"" o
						join public.""OrderStatus"" os ON os.""OrderStatusId"" = o.""OrderStatusId""
						join public.""DeliveryMan"" d ON o.""DeliveryManId"" = d.""DeliveryManId""
						join public.""User"" u ON u.""UserId"" = o.""UserId""
					where d.""UserId"" = @USERID";

			return await connection.QueryAsync<OrderDto>(query, new { USERID = userId });
		}

		public async Task<IEnumerable<OrderDto>> GetOrderAsync()
		{
			using var connection = _repository.GetDbConnection();

			var query = @"select o.""OrderId""
						, o.""RacePrice""
						, os.""Name"" AS Status
						, d.""Name"" as ""DeliveryManName""
					from public.""Order"" o
						join public.""OrderStatus"" os ON os.""OrderStatusId"" = o.""OrderStatusId""
						left join public.""DeliveryMan"" d ON o.""DeliveryManId"" = d.""DeliveryManId""
						left join public.""User"" u ON u.""UserId"" = o.""UserId""";

			return await connection.QueryAsync<OrderDto>(query);
		}
	}
}
