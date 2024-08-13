using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using ShopOnlineApi.Data;
using ShopOnlineApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContextPool<ShoppOnlineDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppOnlineConnection"))
);


builder.Services.AddScoped<IProductService , ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors(policy =>
policy.WithOrigins("http://localhost:7074", "https://localhost:7074")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType)
);
	


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
