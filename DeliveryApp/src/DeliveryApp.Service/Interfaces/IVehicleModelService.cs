using DeliveryApp.Service.ViewModels.Response;

namespace DeliveryApp.Service.Interfaces
{
	public interface IVehicleModelService
	{
		Task<BaseResponse> ListModelsAsync(int brandId);
		Task<bool> ModelExists(int modelId);
	}
}