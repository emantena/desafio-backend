using DeliveryApp.Domain.Entity;

namespace DeliveryApp.Repository.Interfaces
{
    public interface IVehicleBrandRepository
    {
        Task<IEnumerable<VehicleBrand>> ListActivesBrandsAsync();
    }
}