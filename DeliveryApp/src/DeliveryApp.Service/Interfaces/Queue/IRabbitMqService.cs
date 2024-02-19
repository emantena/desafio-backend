using RabbitMQ.Client;

namespace DeliveryApp.Service.Interfaces.Queue
{
	public interface IRabbitMqService : IQueuService
	{
		ConnectionFactory ConnectionFactory { get; }
	}
}
