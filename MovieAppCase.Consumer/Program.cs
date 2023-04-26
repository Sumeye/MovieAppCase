using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieAppCase.Core.DTOs;
using MovieAppCase.Service.Email;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class Program
{
    private static void Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddTransient<IEmailService, EmailService>();
              }).Build()
              ;

        var emailService = host.Services.GetRequiredService<IEmailService>();



        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();

        channel.QueueDeclare("suggestion", exclusive: false);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var suggestion = JsonConvert.DeserializeObject<SuggestionDto>(message);

            var email = new EmailDto()
            {
                To = suggestion.Email ?? "",
                Subject = "Film Önerisi",
                Body = "<b>Film Adı: " + suggestion.MovieTitle + "</b><p>Açıklama: " + suggestion.MovieOverview + "</p><br><b>Messaj: </b>" + suggestion.Message
            };


            emailService.SendEmail(email);

            Console.WriteLine($"suggestion finished");
        };

        channel.BasicConsume(queue: "suggestion", autoAck: true, consumer: consumer);

        Console.ReadKey();
    }
}