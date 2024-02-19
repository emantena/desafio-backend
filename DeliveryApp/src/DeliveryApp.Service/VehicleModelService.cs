using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.ViewModels.Response;

namespace DeliveryApp.Service
{
	public class VehicleModelService : IVehicleModelService
	{
		private readonly IVehicleModelRepository _repository;

		public VehicleModelService(IVehicleModelRepository repository)
		{
			_repository = repository;
		}

		public async Task<BaseResponse> ListModelsAsync(int brandId)
		{
			var vehicleModels = await _repository.ListActivesModelsByBrandId(brandId);
			var modelResponse = new List<ModelResponse>();

			foreach (var model in vehicleModels)
			{
				modelResponse.Add(new ModelResponse(model.VehicleModelId, model.Model));
			}

			var response = new BaseResponse();
			response.AddValue(modelResponse);

			return response;
		}

		public async Task<bool> ModelExists(int modelId)
		{
			if (modelId < 1)
			{
				return false;
			}

			return await _repository.ModelExists(modelId);
		}
	}
}