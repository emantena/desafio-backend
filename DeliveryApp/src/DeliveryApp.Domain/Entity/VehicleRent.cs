namespace DeliveryApp.Domain.Entity
{
	public class VehicleRent
	{
		public int VehicleRentId { get; set; }
		public int VehicleId { get; set; }
		public int PlanVersionId { get; set; }
		public int DeliverymanId { get; set; }
		public DateTime StartRent { get; set; }
		public DateTime PrevisionEndRent { get; set; }
		public DateTime? EndRent { get; set; }

		public Vehicle Vehicle { get; set; }
		public PlanVersion PlanVersion { get; set; }
		public DeliveryMan Deliveryman { get; set; }


		public VehicleRent()
		{

		}
		public VehicleRent(int vehicleId, int planVersionId, int deliverymanId, int totalDaysRent)
		{
			VehicleId = vehicleId;
			PlanVersionId = planVersionId;
			DeliverymanId = deliverymanId;
			StartRent = DateTime.SpecifyKind(DateTime.Now.AddDays(1), DateTimeKind.Utc);
			PrevisionEndRent = DateTime.SpecifyKind(StartRent.AddDays(totalDaysRent), DateTimeKind.Utc);
		}
	}
}
