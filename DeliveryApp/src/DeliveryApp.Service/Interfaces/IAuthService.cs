using DeliveryApp.Domain.ValueObjects;
using DeliveryApp.Service.ViewModels.Request;

namespace DeliveryApp.Service.Interfaces
{
	public interface IAuthService
	{
		Task<Jwt> AuthenticateAsync(AuthRequest request);
	}
}
