using Dapper;
using DeliveryApp.Domain.Dto;
using DeliveryApp.Domain.Entity;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Repository.Interfaces.Base;
using DeliveryApp.Repository.Repositories.Base;

namespace DeliveryApp.Repository.Repositories
{
	public class VehicleRepository : IVehicleRepository
	{

		private readonly IGenericRepository<DeliveryAppContext, Vehicle> _repository;

		public VehicleRepository(IGenericRepository<DeliveryAppContext, Vehicle> repository)
		{
			_repository = repository;
		}

		public async Task<Vehicle> CreateAsync(Vehicle vehicle)
		{
			return await _repository.AddAsync(vehicle);
		}

		public async Task<bool> PlateExistsAsync(string plate)
		{
			return await _repository.AnyAsync(x => x.Plate == plate);
		}

		public async Task<IEnumerable<VehicleDto>> ListVehiclesAsync()
		{
			using var connection = _repository.GetDbConnection();

			var query = @"SELECT v.""VehicleId"" AS VehicleId
							, v.""Plate"" AS Plate
							, v.""YearManufacture"" AS YearManufacture
							, v.""Active"" AS Active
							, v.""CreateAt"" AS CreateAt
							, vm.""VehicleModelId"" AS ModelId
							, vm.""Model"" AS Model
							, vb.""VehicleBrandId"" AS BrandId
							, vb.""Brand"" AS Brand
						FROM ""Vehicle"" v
							JOIN ""VehicleModel"" vm ON v.""VehicleModelId"" = vm.""VehicleModelId""
							JOIN ""VehicleBrand"" vb ON vm.""VehicleBrandId"" = vb.""VehicleBrandId"";";

			return await connection.QueryAsync<VehicleDto, ModelDto, BrandDto, VehicleDto>(query, (vehicle, model, brand) =>
			{
				vehicle.Model = model;
				vehicle.Model.Brand = brand;

				return vehicle;
			}, splitOn: "ModelId,BrandId");
		}

		public async Task<VehicleDto> GetVehicleAsync(string plate)
		{
			using var connection = _repository.GetDbConnection();

			var query = @"SELECT v.""VehicleId"" AS VehicleId
							, v.""Plate"" AS Plate
							, v.""YearManufacture"" AS YearManufacture
							, v.""Active"" AS Active
							, v.""CreateAt"" AS CreateAt
							, vm.""VehicleModelId"" AS ModelId
							, vm.""Model"" AS Model
							, vb.""VehicleBrandId"" AS BrandId
							, vb.""Brand"" AS Brand
						FROM ""Vehicle"" v
							JOIN ""VehicleModel"" vm ON v.""VehicleModelId"" = vm.""VehicleModelId""
							JOIN ""VehicleBrand"" vb ON vm.""VehicleBrandId"" = vb.""VehicleBrandId""
						WHERE v.""Plate"" = @PLATE;";

			var result = await connection.QueryAsync<VehicleDto, ModelDto, BrandDto, VehicleDto>(
				query,
				(vehicle, model, brand) =>
				{
					vehicle.Model = model;
					vehicle.Model.Brand = brand;

					return vehicle;
				},
				new { PLATE = plate },
				splitOn: "ModelId,BrandId"
			);

			return result.FirstOrDefault();
		}

		public async Task<Vehicle> GetByIdAsync(int vehicleId)
		{
			return await _repository.FirstOrDefaultAsync(x => x.VehicleId == vehicleId);
		}

		public void Remove(Vehicle vehicleId)
		{
			_repository.RemoveByEntity(vehicleId);
		}

		public Vehicle Update(Vehicle vehicle)
		{
			_repository.Update(vehicle);
			return vehicle;
		}

		public Task<Vehicle> GetVehicleAvaliableAsync()
		{
			return _repository.FirstOrDefaultAsync(x => !x.IsRent);
		}

		#region Disposable Members

		private bool _disposed;

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (_disposed || !disposing)
			{
				return;
			}

			_repository?.Dispose();

			_disposed = true;
		}
		#endregion
	}
}
