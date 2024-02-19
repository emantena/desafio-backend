namespace DeliveryApp.Service.Interfaces.Queue
{
	public interface IQueuService
	{
		void SendMessage(object message, string queueName);
	}
}
