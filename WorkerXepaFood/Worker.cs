using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace WorkerXepaFood
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConnection _connection;
        private IModel _channel;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declara a fila QUEUE.PRODUTO.SQL caso ainda não exista
            _channel.QueueDeclare(
                queue: "QUEUE.PRODUTO.SQL",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                try
                {
                    byte[] body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _logger.LogInformation($" [x] Process Queue {ea.RoutingKey} body {message}");

                    int dots = message.Split('.').Length - 1;
                    await Task.Delay(dots * 1000, stoppingToken);

                    _logger.LogInformation(" [x] Done");

                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error Worker {ex.Message}");
                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                }
            };

            _channel.BasicConsume(queue: "QUEUE.PRODUTO.SQL", autoAck: false, consumer: consumer);

            stoppingToken.Register(() =>
            {
                _logger.LogInformation("Worker is stopping.");
                _channel?.Close();
                _connection?.Close();
            });

            return Task.CompletedTask;
        }
    }
}