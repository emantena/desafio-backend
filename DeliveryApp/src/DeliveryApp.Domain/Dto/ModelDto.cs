namespace DeliveryApp.Domain.Dto
{
	public class ModelDto
	{
		public int ModelId { get; set; }
		public string Model { get; set; }

		public BrandDto Brand { get; set; }
	}
}
