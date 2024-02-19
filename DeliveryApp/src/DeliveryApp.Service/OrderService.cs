using DeliveryApp.Domain.Entity;
using DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.ValueObjects;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.Interfaces.Queue;
using DeliveryApp.Service.ViewModels.Request;
using DeliveryApp.Service.ViewModels.Response;
using Flunt.Notifications;

namespace DeliveryApp.Service
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _repository;
		private readonly IRabbitMqService _mqService;
		private readonly IDeliveryManRepository _deliveryManService;

		public OrderService(IRabbitMqService mqService, IOrderRepository repository, IDeliveryManRepository deliveryManService)
		{
			_mqService = mqService;
			_repository = repository;
			_deliveryManService = deliveryManService;
		}

		public async Task<BaseResponse> AcceptOrderAsync(AcceptOrderRequest request)
		{
			var baseResponse = new BaseResponse();
			var order = await _repository.GetOrderByIdAsync(request.OrderId);

			if (order is null)
			{
				baseResponse.AddNotification(new Notification("", "Pedido não encotrado"));
				return baseResponse;
			}

			if (order.DeliveryManId > 0)
			{
				baseResponse.AddNotification(new Notification("", "Pedido não esta mais disponível"));
				return baseResponse;
			}

			var deliveryMan = await _deliveryManService.GetByUserIdAsync(Context.UserId);

			order.CreateAt = DateTime.SpecifyKind(order.CreateAt, DateTimeKind.Utc);
			order.DeliveryManId = deliveryMan.DeliveryManId;
			order.OrderStatusId = (int)OrderStatus.Accepted;
			_repository.Update(order);

			return baseResponse;
		}

		public async Task<BaseResponse> FinishOrderAsync(AcceptOrderRequest request)
		{
			var baseResponse = new BaseResponse();
			var order = await _repository.GetOrderByIdAsync(request.OrderId);

			if (order is null)
			{
				baseResponse.AddNotification(new Notification("", "Pedido não encotrado"));
				return baseResponse;
			}

			order.CreateAt = DateTime.SpecifyKind(order.CreateAt, DateTimeKind.Utc);
			order.OrderStatusId = (int)OrderStatus.Delivered;
			_repository.Update(order);

			return baseResponse;
		}

		public async Task<BaseResponse> CreateOrderAsync(OrderRequest request)
		{
			var response = new BaseResponse();

			var order = new Order(Context.UserId, request.RacePrice, OrderStatus.Available);

			if (!order.IsValid)
			{
				response.AddNotifications(order.Notifications);
				return response;
			}

			order = await _repository.CreateAsync(order);

			_mqService.SendMessage(order, "order_queue");

			return response;
		}

		public async Task<BaseResponse> LoadOrdersAsync()
		{
			var baseResponse = new BaseResponse();
			var orders = await _repository.GetOrderByUserIdAsync(Context.UserId);

			baseResponse.AddValue(orders);

			return baseResponse;
		}

		public async Task<BaseResponse> LoadAllOrdersAsync()
		{
			var baseResponse = new BaseResponse();
			var orders = await _repository.GetOrderAsync();

			baseResponse.AddValue(orders);

			return baseResponse;
		}
	}
}
