namespace DeliveryApp.Service.ViewModels.Response
{
	public class ModelResponse
	{
		public int ModelId { get; set; }
		public string Name { get; set; }

		public ModelResponse(int modelId, string name)
		{
			ModelId = modelId;
			Name = name;
		}
	}
}
