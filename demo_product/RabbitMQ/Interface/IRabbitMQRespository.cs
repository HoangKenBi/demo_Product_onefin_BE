using demo_product.Entity;

namespace demo_product.RabbitMQ.Interface
{
    public interface IRabbitMQRespository
    {
        void SendOrderDetail(OrderDetail orderDetail);
        Task StartListeningAsync(CancellationToken cancellationToken);
      
    }
}

