using MassTransit;
using SimpleChatroom.Worker.Consumer;

namespace SimpleChatroom.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();

                    var connectionString = hostContext.Configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Connection string 'RabbitMQ' not found.");
                    services.AddMassTransit(c =>
                    {
                        c.AddConsumer<CommandConsumer>();

                        c.UsingRabbitMq((context, config) =>
                        {
                            config.ReceiveEndpoint("simplechatroom-command", queue =>
                            {
                                queue.ConfigureConsumer<CommandConsumer>(context);
                            });

                            config.Host(connectionString);
                        });
                    });
                })
                .Build();

            host.Run();
        }
    }
}