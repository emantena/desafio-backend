using Dapper;
using DeliveryApp.Domain.Dto;
using DeliveryApp.Domain.Entity;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Repository.Interfaces.Base;
using DeliveryApp.Repository.Repositories.Base;

namespace DeliveryApp.Repository.Repositories
{
	public class PlanRepository : IPlanRepository
	{
		private readonly IGenericRepository<DeliveryAppContext, Plans> _repository;

		public PlanRepository(IGenericRepository<DeliveryAppContext, Plans> repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<PlanDto>> ListActivePlansAsync()
		{
			using var connection = _repository.GetDbConnection();

			var query = @"SELECT p.""PlanId""
							, p.""Name""
							, p.""Description""
							, pv.""PlanVersionId""
							, pv.""Price""
							, pv.""Active""
						FROM
						public.""Plans"" p
							JOIN public.""PlanVersion"" pv ON p.""PlanId"" = pv.""PlanId""
								AND pv.""Active""";

			return await connection.QueryAsync<PlanDto>(query);
		}

		public async Task<IEnumerable<PlanDto>> ListPlansAsync()
		{
			using var connection = _repository.GetDbConnection();

			var query = @"SELECT p.""PlanId""
							, p.""Name""
							, p.""Description""
							, pv.""PlanVersionId""
							, pv.""Price""
							, pv.""Active""
						FROM
						public.""Plans"" p
							JOIN public.""PlanVersion"" pv ON p.""PlanId"" = pv.""PlanId""";

			return await connection.QueryAsync<PlanDto>(query);
		}
	}
}
