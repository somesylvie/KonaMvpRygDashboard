using KonaMvpRygDashboard;

var LocalDevOrigins = "_localDevOrigins";
var builder = WebApplication.CreateBuilder(args);

// Let the locally-running react app hit the backend
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: LocalDevOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRainbowService, RainbowService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(LocalDevOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
