using DeliveryApp.Service.ViewModels.Response;

namespace DeliveryApp.Service.Interfaces
{
    public interface IVehicleBrandService
    {
        Task<BaseResponse> ListBrandsAsync();
    }
}