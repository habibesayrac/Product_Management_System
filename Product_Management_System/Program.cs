using Microsoft.EntityFrameworkCore;
using PMS.BusinessLayer.Abstract;
using PMS.BusinessLayer.Concrete;
using PMS.Consumer;
using PMS.DataAccessLayer.Abstract;
using PMS.DataAccessLayer.Concrete;
using PMS.DataAccessLayer.Repository;
using PMS.RabbitMq.RabbitMq;
using PMS.Redis.Repository;
using PMS.ResponseModel;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Serilog 

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:6379"); //Bu satýr Redis için eklendi

//RabbitMQ için eklenen satýrlar

builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();
builder.Services.AddSingleton<RabbitMqConnection>(); 

// Database MySQL

builder.Services.AddDbContext<Context>(options => options.UseMySQL("server=127.0.0.1;port=3306;database=PMSdb;user=root;password=root;Max Pool Size=200;Min Pool Size=10;Pooling=true"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductManager>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderManager>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserManager>();

builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddScoped<IOrderProductService, OrderProductManager>();

builder.Services.AddScoped<IUserProductRepository, UserProductRepository>();
builder.Services.AddScoped<IUserProductService, UserProductManager>();

builder.Services.AddScoped<IBaseResponseModel, BaseResponseModel>();

builder.Services.AddScoped<IRedisRepository, RedisRepository>(); //Bu satýr Redis için eklendi

builder.Services.AddTransient<ICategoryConsumer, CategoryConsumer>();
builder.Services.AddHostedService<CategoryBackgroundService>();

builder.Services.AddTransient<IOrderConsumer, OrderConsumer>();
builder.Services.AddHostedService<OrderBackgroundService>();

builder.Services.AddTransient<IProductConsumer, ProductConsumer>();
builder.Services.AddHostedService<ProductBackgroundService>();

builder.Services.AddTransient<IUserConsumer, UserConsumer>();
builder.Services.AddHostedService<UserBackgroundService>();

builder.Services.AddTransient<IUserProductConsumer, UserProductConsumer>();
builder.Services.AddHostedService<UserProductBackgroundService>();

builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(); //Serilog için eklendi

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();