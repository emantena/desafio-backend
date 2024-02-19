namespace DeliveryApp.Service.ViewModels.Response
{
	public class CreateVehicleResponse
	{
		public string Plate { get; set; }
		public int ModelId { get; set; }
		public int YearManufacture { get; set; }
		public DateTime CreateAt { get; set; }

		public CreateVehicleResponse(string plate, int modelId, int yearManufacture, DateTime createAt)
		{
			Plate = plate;
			ModelId = modelId;
			YearManufacture = yearManufacture;
			CreateAt = createAt;
		}
	}
}
