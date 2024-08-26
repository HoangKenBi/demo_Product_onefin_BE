using demo_product.Data;
using demo_product.Entity;
using demo_product.RabbitMQ.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using IModel = RabbitMQ.Client.IModel;

namespace demo_product.RabbitMQ
{
    public class RabbitMQService : BackgroundService, IRabbitMQRespository
    {

        private readonly IModel _channel;
        private readonly string _queueName = "orderDetailQueue"; // Tên hàng đợi RabbitMQ
        private ILogger<RabbitMQService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQService(ILogger<RabbitMQService> logger, IServiceProvider serviceProvider, IModel channel)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _channel = channel;
            
        }
        public void SendOrderDetail(OrderDetail orderDetail)
        {
            //khai báo hàng đợi trong rbMQ
            _channel.QueueDeclare(queue: _queueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            var message = JsonSerializer.Serialize(orderDetail);  // chuyển đổi orderDetail thành chuối json
            var body = Encoding.UTF8.GetBytes(message); // Mã hóa json thành byte vì rbMQ gửi-nhận bằng định dạng byte
            // Gửi thông điệp
            _channel.BasicPublish(exchange: "",
                                  routingKey: _queueName,
                                  basicProperties: null,
                                  body: body);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)  // Được ghi đè từ lớp BackgroundService
        {
            return Task.Run(() => StartListeningAsync(stoppingToken), stoppingToken); // đảm bảo hàm StartListeningAsync không làm ảnh hưởng đến các luồng chính khác
        }

        public async Task StartListeningAsync(CancellationToken cancellationToken)
        {
            try
            {
                // Khai báo hàm đợi trước khi nhận Thông điệp
                _channel.QueueDeclare(queue: _queueName,
                                      durable: false,
                                      exclusive: false,
                                      autoDelete: false,
                                      arguments: null);
                // consumer : để lắng nghe các thông điệp từ hàng đợi.
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"Received message: {message}"); // Debug log message

                    var orderDetail = JsonSerializer.Deserialize<OrderDetail>(message);

                    if (orderDetail != null)
                    {
                        await SaveOrderDetailAsync(orderDetail); // Lưu vào sql nếu Deserialize thành công
                    }
                    else
                    {
                        Console.WriteLine("Deserialization returned null."); // Log deserialization issues
                    }
                };

                _channel.BasicConsume(queue: _queueName,
                                       autoAck: true,  // tự động xác nhận thông điệp ngay khi nó được gửi đến consumer,
                                       consumer: consumer);

                //Dịch vụ nền tiếp tục chạy cho đến khi cancellationToken yêu cầu dừng.
                await Task.Delay(-1, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}"); // Log errors
            }
        }
        // Nhận orderDetail lưu vào sql
        private async Task SaveOrderDetailAsync(OrderDetail orderDetail)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
                dbContext.OrderDetails.Add(orderDetail);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
