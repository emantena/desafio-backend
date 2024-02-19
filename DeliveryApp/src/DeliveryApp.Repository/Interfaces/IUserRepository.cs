﻿using DeliveryApp.Domain.Entity;

namespace DeliveryApp.Repository.Interfaces
{
	public interface IUserRepository
	{
		Task<User> CreateUserAsync(User user);
		Task<User> GetUserByEmailAsync(string email);
		Task<User> GetUserByIdAsync(int userId);
	}
}
