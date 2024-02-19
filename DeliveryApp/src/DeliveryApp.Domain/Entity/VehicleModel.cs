namespace DeliveryApp.Domain.Entity
{
	public class VehicleModel
	{
		public int VehicleModelId { get; set; }
		public int VehicleBrandId { get; set; }
		public string Model { get; set; }
		public bool Active { get; set; }

		public VehicleBrand VehicleBrand { get; set; }
	}
}
