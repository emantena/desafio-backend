using DeliveryApp.Domain.Extensions;
using DeliveryApp.Domain.Validation;
using Flunt.Notifications;

namespace DeliveryApp.Domain.Entity
{
	public class DeliveryMan : Notifiable<Notification>
	{
		public int DeliveryManId { get; set; }
		public int UserId { get; set; }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public string CNH { get; set; }
		public string CNPJ { get; set; }
		public int CnhTypeId { get; set; }
		public string CnhImage { get; set; }

		public DeliveryMan()
		{

		}

		public DeliveryMan(string name, DateTime birthDate, string cnh, string cnpj, int cnhType, int userId)
		{
			Name = name;
			BirthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);
			CNH = cnh.OnlyNumber();
			CNPJ = cnpj.OnlyNumber();
			CnhTypeId = cnhType;
			UserId = userId;

			AddNotifications(new DeliveryManValidator(this));
		}
	}
}
