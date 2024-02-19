using DeliveryApp.Service.ViewModels.Response;

namespace DeliveryApp.Service.Interfaces
{
	public interface IPlanService
	{
		Task<BaseResponse> ListActivePlansAsync();
		Task<BaseResponse> ListPlansAsync();
	}
}
