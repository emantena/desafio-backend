namespace DeliveryApp.Domain.Dto
{
	public class PlanDto
	{
		public int PlanId { get; set; }
		public int PlanVersionId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public bool Active { get; set; }
	}
}
