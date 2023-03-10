using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;

namespace Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<MessageConsumer>(c =>
                        {
                            c.ConcurrentMessageLimit = 1;
                        });

                        x.UsingRabbitMq((context,cfg) =>
                        {
                            cfg.Host("rabbitmq", "/");
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                });
    }
}
