using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Events;
using RabbitMQ.Client;
using EnrollmentService.Data;
using Entities.Concrete.EnrollmentService;
using System.Text;
using System.Text.Json;

namespace EnrollmentService.Services.Consumers
{
    public class PaymentCompletedConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentCompletedConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "payment-completed",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var eventData = JsonSerializer.Deserialize<PaymentCompletedEvent>(message);

                if (eventData != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<Context>();

                    var enrollment = new Enrollment
                    {
                        UserId = eventData.UserId,
                        CourseId = eventData.CourseId,
                        CompletionDate = eventData.PaidAt,
                        Status = "Active"
                    };

                    db.Enrollments.Add(enrollment);
                    await db.SaveChangesAsync();
                }
            };

            channel.BasicConsume(queue: "payment-completed",
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
