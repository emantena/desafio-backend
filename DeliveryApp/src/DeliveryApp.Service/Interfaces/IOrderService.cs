using DeliveryApp.Service.ViewModels.Request;
using DeliveryApp.Service.ViewModels.Response;

namespace DeliveryApp.Service.Interfaces
{
	public interface IOrderService
	{
		Task<BaseResponse> AcceptOrderAsync(AcceptOrderRequest request);
		Task<BaseResponse> CreateOrderAsync(OrderRequest request);
		Task<BaseResponse> FinishOrderAsync(AcceptOrderRequest request);
		Task<BaseResponse> LoadAllOrdersAsync();
		Task<BaseResponse> LoadOrdersAsync();
	}
}
