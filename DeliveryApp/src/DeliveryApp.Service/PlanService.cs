using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.ViewModels.Response;

namespace DeliveryApp.Service
{
	public class PlanService : IPlanService
	{
		private readonly IPlanRepository _repository;

		public PlanService(IPlanRepository repository)
		{
			_repository = repository;
		}

		public async Task<BaseResponse> ListActivePlansAsync()
		{
			var response = new BaseResponse();
			response.AddValue(await _repository.ListActivePlansAsync());

			return response;
		}

		public async Task<BaseResponse> ListPlansAsync()
		{
			var response = new BaseResponse();
			response.AddValue(await _repository.ListPlansAsync());

			return response;
		}
	}
}
