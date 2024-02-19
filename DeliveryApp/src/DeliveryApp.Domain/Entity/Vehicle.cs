using DeliveryApp.Domain.Validation;
using Flunt.Notifications;

namespace DeliveryApp.Domain.Entity
{
	public class Vehicle : Notifiable<Notification>
	{
		public int VehicleId { get; set; }
		public int VehicleModelId { get; set; }
		public string Plate { get; set; }
		public int YearManufacture { get; set; }
		public DateTime CreateAt { get; set; }

		public VehicleModel VehicleModel { get; set; }
		public bool IsRent { get; set; }

		public Vehicle()
		{

		}

		public Vehicle(int vehicleId, int vehicleModelId, string plate, int yearManufacture)
		{
			VehicleId = vehicleId;
			VehicleModelId = vehicleModelId;
			Plate = plate.ToUpper();
			YearManufacture = yearManufacture;
			CreateAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

			AddNotifications(new VehicleValidator(this));
		}
	}
}
