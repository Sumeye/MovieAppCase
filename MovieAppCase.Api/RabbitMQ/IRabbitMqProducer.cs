namespace MovieAppCase.API.RabbitMQ
{
    public interface IRabbitMqProducer
    {
        public void SendMessage<T>(T message);
    }
}
