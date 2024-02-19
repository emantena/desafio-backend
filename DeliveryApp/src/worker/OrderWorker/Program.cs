using Microsoft.AspNetCore.Hosting;

namespace OrderWorker
{
	internal class Program
	{
		static void Main(string[] args)
		{
			using var host = Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				})
				.Build();

			host.Run();
			var webSocketHub = host.Services.GetService<WebSocketHub>();

			webSocketHub?.StopRabbitMQ();
		}
	}
}