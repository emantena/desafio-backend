using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.ViewModels.Response;

namespace DeliveryApp.Service
{
	public class VehicleBrandService : IVehicleBrandService
	{
		private readonly IVehicleBrandRepository _repository;

		public VehicleBrandService(IVehicleBrandRepository repository)
		{
			_repository = repository;
		}

		public async Task<BaseResponse> ListBrandsAsync()
		{
			var brands = await _repository.ListActivesBrandsAsync();
			var brandResponse = new List<BrandResponse>();

			foreach (var brand in brands)
			{
				brandResponse.Add(new BrandResponse(brand.VehicleBrandId, brand.Brand));
			}

			var response = new BaseResponse();
			response.AddValue(brandResponse);

			return response;
		}
	}
}