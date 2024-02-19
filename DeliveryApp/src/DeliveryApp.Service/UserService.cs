using DeliveryApp.Domain.Entity;
using DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.Extensions;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.ViewModels.Response;
using Flunt.Notifications;

namespace DeliveryApp.Service
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<BaseResponse> CreateUserAsync(User user)
		{
			var response = new BaseResponse();

			if (!user.IsValid)
			{
				response.AddNotifications(user.Notifications);
				return response;
			}

			if (await _userRepository.GetUserByEmailAsync(user.Email) != null)
			{
				response.AddNotification(
				new Notification()
				{
					Key = "email",
					Message = "email já cadastrado"
				});
				return response;
			}

			user = await _userRepository.CreateUserAsync(user);

			response.AddValue(user);
			return response;
		}

		public async Task<BaseResponse> GetUserProfileAsync(int userId)
		{
			var response = new BaseResponse();
			var user = await _userRepository.GetUserByIdAsync(userId);

			if (user == null)
			{
				response.AddNotification(new Notification
				{
					Key = "",
					Message = "usuário não localizado"
				});

				return response;
			}
			var role = user.RoleId.IntToEnum<Role>();
			response.AddValue(new UserProfileResponse(user.UserId, user.Name, user.Email, role.GetEnumDescription()));
			return response;
		}
	}
}
