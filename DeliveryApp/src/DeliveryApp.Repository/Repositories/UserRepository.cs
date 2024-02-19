using DeliveryApp.Domain.Entity;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Repository.Interfaces.Base;
using DeliveryApp.Repository.Repositories.Base;

namespace DeliveryApp.Repository.Repositories
{
	public class UserRepository : IUserRepository
	{

		private readonly IGenericRepository<DeliveryAppContext, User> _repository;

		public UserRepository(IGenericRepository<DeliveryAppContext, User> repository)
		{
			_repository = repository;
		}

		public async Task<User> CreateUserAsync(User user)
		{
			return await _repository.AddAsync(user);
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			return await _repository.FirstOrDefaultAsync(x => x.Email == email);
		}

		public async Task<User> GetUserByIdAsync(int userId)
		{
			return await _repository.FirstOrDefaultAsync(x => x.UserId == userId && x.Active);
		}
	}
}
