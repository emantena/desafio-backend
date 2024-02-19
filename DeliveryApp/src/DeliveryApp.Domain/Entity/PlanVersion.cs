namespace DeliveryApp.Domain.Entity
{
	public class PlanVersion
	{
		public int PlanVersionId { get; set; }
		public int PlanId { get; set; }
		public decimal Price { get; set; }
		public bool Active { get; set; }
		public int MinDayRent { get; set; }
		public decimal AdditionalCharge { get; set; }
		public decimal DailyLateFee { get; set; }

		public Plans Plans { get; set; }
	}
}
