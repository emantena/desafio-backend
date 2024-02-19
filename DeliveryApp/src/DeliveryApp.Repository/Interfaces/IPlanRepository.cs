using DeliveryApp.Domain.Dto;

namespace DeliveryApp.Repository.Interfaces
{
	public interface IPlanRepository
	{
		Task<IEnumerable<PlanDto>> ListPlansAsync();
		Task<IEnumerable<PlanDto>> ListActivePlansAsync();
	}
}
