namespace DeliveryApp.Service.ViewModels.Response
{
	public class BrandResponse
	{
		public int BrandId { get; set; }
		public string Name { get; set; }

		public BrandResponse(int brandId, string name)
		{
			BrandId = brandId;
			Name = name;
		}
	}
}
