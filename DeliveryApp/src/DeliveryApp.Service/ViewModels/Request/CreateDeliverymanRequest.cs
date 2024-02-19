namespace DeliveryApp.Service.ViewModels.Request
{
	public class CreateDeliverymanRequest
	{
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public string Cnh { get; set; }
		public string Cnpj { get; set; }
		public int CnhType { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
