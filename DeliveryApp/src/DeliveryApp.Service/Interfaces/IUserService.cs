using DeliveryApp.Domain.Entity;
using DeliveryApp.Service.ViewModels.Response;

namespace DeliveryApp.Service.Interfaces
{
	public interface IUserService
	{
		Task<BaseResponse> GetUserProfileAsync(int userId);
		Task<BaseResponse> CreateUserAsync(User user);
	}
}
