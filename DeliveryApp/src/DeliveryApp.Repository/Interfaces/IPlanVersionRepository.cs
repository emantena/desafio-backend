using DeliveryApp.Domain.Entity;

namespace DeliveryApp.Repository.Interfaces
{
	public interface IPlanVersionRepository
	{
		Task<PlanVersion> GetPlanVersionByIdAsync(int planVersionId);
	}
}
