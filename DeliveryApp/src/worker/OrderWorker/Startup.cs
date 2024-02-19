using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace OrderWorker
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
				{
					builder
						.AllowAnyMethod()
						.AllowAnyHeader()
						.SetIsOriginAllowed(origin => true) // Permitir qualquer origem
						.AllowCredentials(); // Permitir credenciais
				});
			});

			services.AddSignalR();
			services.AddSingleton<WebSocketHub>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseCors();
			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<WebSocketHub>("/websocket");
			});

			var webSocketHub = app.ApplicationServices.GetService<WebSocketHub>();
			webSocketHub?.StartRabbitMQ();
		}
	}
}