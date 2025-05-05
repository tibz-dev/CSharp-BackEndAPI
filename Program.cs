var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Use CORS
app.UseCors("AllowAll");

// 🔁 Optional: Disable HTTPS redirection for local testing
// Comment this out if your API isn't running with HTTPS
// app.UseHttpsRedirection();

// Only show Swagger/OpenAPI in development (optional)
if (app.Environment.IsDevelopment())
{
    // This is only relevant if you added Swagger with AddEndpointsApiExplorer + AddSwaggerGen
    // Not needed for your current use case
}

// ❗ Map your custom endpoint
app.MapGet("/api/message", (IHttpContextAccessor httpContextAccessor) =>
{
    string results = "Hello from C# API! The current time is " + DateTime.Now.ToString("T") + " Computer Name: " + Environment.MachineName;
    
    string ipAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
    // Check if the IP address is null or empty
    // Check if the IP address is IPv4 or IPv6 and format accordingly
    if (httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
    {
        ipAddress = "[" + ipAddress + "]"; // IPv6 format
    }

    else if (httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
    {
        ipAddress = ipAddress; // IPv4 format
    }
    else
    {
        ipAddress = "Unknown IP"; // Fallback for unknown IP address
    }

    return results + " IP Address: " + ipAddress;
});

// Optional default example (you can keep or remove)
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

// WeatherForecast record (for the example above)
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
