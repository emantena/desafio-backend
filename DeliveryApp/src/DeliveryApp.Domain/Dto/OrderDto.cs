namespace DeliveryApp.Domain.Dto
{
	public class OrderDto
	{
		public int OrderId { get; set; }
		public string DeliveryManName { get; set; }
		public string Status { get; set; }
		public decimal RacePrice { get; set; }
	}
}
