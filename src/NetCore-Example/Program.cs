using Example.Common.Cache.Helper;
using Example.Common.Cache.Model;
using Example.Common.Cache.Service;

var builder = WebApplication.CreateBuilder(args);

// appsettings.json kullanımı için eklediğimiz kod
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Redis
// redis DI
builder.Services.AddSingleton(sp => new RedisServer(new RedisSetting
{
    RedisDB = Convert.ToInt32(configuration.GetSection("RedisSetting:RedisDB").Value),
    RedisEndPoint = configuration.GetSection("RedisSetting:RedisEndPoint").Value ?? "",
    RedisPassword = configuration.GetSection("RedisSetting:RedisPassword").Value ?? "",
    RedisPort = configuration.GetSection("RedisSetting:RedisPort").Value ?? "",
}));
builder.Services.AddSingleton<ICacheService, RedisCacheService>();
#endregion

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
