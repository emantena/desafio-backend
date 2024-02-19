using DeliveryApp.Domain.Entity;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Repository.Interfaces.Base;
using DeliveryApp.Repository.Repositories.Base;

namespace DeliveryApp.Repository.Repositories
{
	public class PlanVersionRepository : IPlanVersionRepository
	{
		private readonly IGenericRepository<DeliveryAppContext, PlanVersion> _repository;

		public PlanVersionRepository(IGenericRepository<DeliveryAppContext, PlanVersion> repository)
		{
			_repository = repository;
		}

		public async Task<PlanVersion> GetPlanVersionByIdAsync(int planVersionId)
		{
			return await _repository.FirstOrDefaultAsync(x =>
				x.PlanVersionId == planVersionId &&
				x.Active
			);
		}
	}
}
