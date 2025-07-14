using Microsoft.EntityFrameworkCore;
using MyApp.Server.Data;
using MyApp.Server.Logging;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var serverVersion = ServerVersion.AutoDetect(connectionString); // Server versiyasini aniqlash
    options.UseMySql(connectionString, serverVersion);
});

builder.Services.AddControllers(option =>
{

}).AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILogging, LoggingV2>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowFrontendOrigin",
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:6064", "https://localhost:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontendOrigin");

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();