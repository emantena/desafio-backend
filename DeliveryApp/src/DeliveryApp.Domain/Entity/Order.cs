using DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.Validation;
using Flunt.Notifications;

namespace DeliveryApp.Domain.Entity
{
	public class Order : Notifiable<Notification>
	{
		public int OrderId { get; set; }
		public int UserId { get; set; }
		public int DeliveryManId { get; set; }
		public int OrderStatusId { get; set; }
		public DateTime CreateAt { get; set; }
		public decimal RacePrice { get; set; }

		public User User { get; set; }

		public Order()
		{

		}

		public Order(int userId, decimal racePrice, OrderStatus orderStatus)
		{
			UserId = userId;
			RacePrice = racePrice;
			OrderStatusId = (int)orderStatus;
			CreateAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

			AddNotifications(new OrderValidator(this));
		}
	}
}
