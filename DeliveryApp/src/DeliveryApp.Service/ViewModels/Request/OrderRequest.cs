namespace DeliveryApp.Service.ViewModels.Request
{
	public class OrderRequest
	{
		public int UserId { get; set; }
		public int OrderStatusId { get; set; }
		public DateTime CreateAt { get; set; }
		public decimal RacePrice { get; set; }
	}
}
