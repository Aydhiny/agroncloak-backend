using Esp32ImageApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5055");

// Register DbContext with a connection string (you can modify this with your actual connection string)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enable CORS to allow frontend to call API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // Allow frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Register other services
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();  // Add Swagger for API documentation

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// Use CORS middleware
app.UseCors();
app.UseRouting();
app.UseAuthorization();
app.MapControllers(); // Map controller endpoints

app.Run();
