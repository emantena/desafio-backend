namespace DeliveryApp.Service.ViewModels.Response
{
	public class PlanCostResponse
	{
		public decimal PlanPrice { get; set; }
		public int TotalDaysLocation { get; set; }
		public decimal TotalCost { get; set; }
		public decimal DailyLateFee { get; set; }
		public decimal AdditionalCharge { get; set; }

		public PlanCostResponse(decimal planPrice, int totalDaysLocation, decimal dailyLateFee, decimal additionalCharge, decimal totalCost)
		{
			PlanPrice = planPrice;
			TotalDaysLocation = totalDaysLocation;
			TotalCost = totalCost;
			DailyLateFee = dailyLateFee;
			AdditionalCharge = additionalCharge;
		}
	}
}
