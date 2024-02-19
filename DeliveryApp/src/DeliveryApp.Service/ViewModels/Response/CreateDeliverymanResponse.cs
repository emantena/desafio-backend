namespace DeliveryApp.Service.ViewModels.Response
{
	public class CreateDeliverymanResponse
	{
		public int DeliverymanId { get; set; }
		public string Name { get; set; }
		public DateTime BirthDate { get; set; }
		public string CNH { get; set; }
		public string CNPJ { get; set; }
		public int CnhType { get; set; }

		public CreateDeliverymanResponse(int deliverymanId, string name, DateTime birthDate, string cnh, string cnpj, int cnhType)
		{
			DeliverymanId = deliverymanId;
			Name = name;
			BirthDate = birthDate;
			CNH = cnh;
			CNPJ = cnpj;
			CnhType = cnhType;
		}
	}
}
