var builder = WebApplication.CreateBuilder(args);
using Gateway.Data;
using Microsoft.EntityFrameworkCore;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HowToLearnDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["Data:Database:ConnectionString"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

try
{
    var context = app.Services.GetRequiredService<HowToLearnDbContext>();
    DbInitializer.Initialize(context);
}
catch { }

app.Run();
