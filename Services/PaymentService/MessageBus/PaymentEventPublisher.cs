using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Shared.Events;

namespace PaymentService.MessageBus
{
    public class PaymentEventPublisher
    {
        public void PublishPaymentCompleted(PaymentCompletedEvent paymentEvent)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "payment-completed",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = JsonSerializer.Serialize(paymentEvent);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "payment-completed",
                                 basicProperties: null,
                                 body: body);
        }
    }
}