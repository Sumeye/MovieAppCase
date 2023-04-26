using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MovieAppCase.API.RabbitMQ
{
    public class RabbitMqProducer : IRabbitMqProducer
    {
        public void SendMessage<T>(T message)
        {

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();
            
            channel.QueueDeclare("suggestion", exclusive: false);
            
            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            
            channel.BasicPublish(exchange: "", routingKey: "suggestion", body: body);
        }
    }
}