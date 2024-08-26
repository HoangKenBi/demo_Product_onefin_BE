using AutoMapper;
using demo_product.Data;
using demo_product.Helpers;
using demo_product.Interface;
using demo_product.RabbitMQ;
using demo_product.RabbitMQ.Interface;
using demo_product.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

internal class Program
{
    private static void Main(string[] args)
    {       
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Cấu hình DbContext
        var connectionString = builder.Configuration.GetConnectionString("MyDB");
        builder.Services.AddDbContext<MyDbContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });

        // Cấu hình AutoMapper
        builder.Services.AddAutoMapper(typeof(ApplicationMapper));


        var rabbitMQConfig = builder.Configuration.GetSection("RabbitMQ");

        // Đăng ký IConnectionFactory
        builder.Services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
        {
            HostName = rabbitMQConfig["HostName"],
            UserName = rabbitMQConfig["UserName"],
            Password = rabbitMQConfig["Password"]
        });

        // Đăng ký IConnection
        builder.Services.AddSingleton<IConnection>(sp =>
        {
            var factory = sp.GetRequiredService<IConnectionFactory>();
            return factory.CreateConnection();
        });

        // Đăng ký IModel
        builder.Services.AddScoped<IModel>(sp =>
        {
            var connection = sp.GetRequiredService<IConnection>();
            return connection.CreateModel();
        });

        // Cấu hình addscope interface và service
        builder.Services.AddScoped<IProductRespository, ProductService>();
        builder.Services.AddScoped<IAccountRespository, AccountService>();
        builder.Services.AddScoped<IOrderRespository, OrderService>();
        builder.Services.AddScoped<IOrderDetailRespository, OrderDetailService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IRabbitMQRespository, RabbitMQService>();
        builder.Services.AddScoped<RabbitMQService>();


        builder.Services.AddCors(p => p.AddPolicy("Cors", build =>
        {
            build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        }));

        var app = builder.Build();

        // Khởi động dịch vụ lắng nghe RabbitMQ
        var cancellationToken = app.Lifetime.ApplicationStopping; // Lấy cancellation token từ vòng đời ứng dụng

        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

        _ = Task.Run(() =>
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var rabbitMQService = scope.ServiceProvider.GetRequiredService<RabbitMQService>();
                rabbitMQService.StartListeningAsync(cancellationToken).GetAwaiter().GetResult();
            }
        });
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("Cors");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

   
}