using DeliveryApp.Service.ViewModels.Request;
using DeliveryApp.Service.ViewModels.Response;

namespace DeliveryApp.Service.Interfaces
{
	public interface IDeliveryManService
	{
		Task<BaseResponse> CreateAsync(CreateDeliverymanRequest request);
		Task<BaseResponse> SendDocumentImageAsync(byte[] fileBytes, int deliveryManId);
	}
}
