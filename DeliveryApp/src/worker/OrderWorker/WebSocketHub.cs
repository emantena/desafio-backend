using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderWorker
{
	public class WebSocketHub : Hub
	{
		const string UserName = "agvtwipw";
		const string Password = "jqT8J8BVs12QV_5PPKfXjgr_onSn-IgS";
		const string HostName = "fly.rmq.cloudamqp.com";
		private IConnection _rabbitMQConnection;
		private IModel _rabbitMQChannel;

		public void StartRabbitMQ()
		{
			var factory = new ConnectionFactory()
			{
				UserName = UserName,
				Password = Password,
				HostName = HostName,
				VirtualHost = UserName
			};
			// Configurar conforme necessário
			_rabbitMQConnection = factory.CreateConnection();
			_rabbitMQChannel = _rabbitMQConnection.CreateModel();


			var consumer = new EventingBasicConsumer(_rabbitMQChannel);
			consumer.Received += (model, ea) =>
				{
					var body = ea.Body.ToArray();
					var message = Encoding.UTF8.GetString(body);

					Clients?.All.SendAsync("ReceiveMessage", message);

					Console.WriteLine($"Mensagem recebida da fila: {message}");
				};
			if (Clients != null)
			{
				_rabbitMQChannel.BasicConsume(queue: "order_queue",
											  autoAck: true,
											  consumer: consumer);
			}
		}

		public void StopRabbitMQ()
		{
			_rabbitMQChannel?.Dispose();
			_rabbitMQConnection?.Dispose();
		}

		public async Task SendMessage(string message)
		{
			try
			{
				await Clients.All.SendAsync("ReceiveMessage", message);

				Console.WriteLine($"Mensagem recebida do cliente: {message}");
			}
			catch (Exception ex)
			{

				throw ex;
			}

		}
	}
}