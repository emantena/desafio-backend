namespace DeliveryApp.Service.ViewModels.Request
{
	public class CreateVehicleRequest
	{
		public string Plate { get; set; }
		public int ModelId { get; set; }
		public int YearManufacture { get; set; }
	}
}