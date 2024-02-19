namespace DeliveryApp.Domain.Dto
{
	public class VehicleDto
	{
		public int VehicleId { get; set; }
		public int VehicleModelId { get; set; }
		public string Plate { get; set; }
		public int YearManufacture { get; set; }
		public DateTime CreateAt { get; set; }
		public ModelDto Model { get; set; }
	}
}
