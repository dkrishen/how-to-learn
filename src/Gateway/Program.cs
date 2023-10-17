using Gateway.Core.Middlwares;
using Gateway.Data;
using Gateway.Logic;
using Gateway.Logic.Profiles;
using Gateway.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HowToLearnDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["Data:Database:ConnectionString"]);
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<ISectionRepository, SectionRepository>();
builder.Services.AddTransient<ISectionLogic, SectionLogic>();

builder.Services.AddAutoMapper(typeof(SectionMapperProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

try
{
    var context = app.Services.GetRequiredService<HowToLearnDbContext>();
    DbInitializer.Initialize(context);
}
catch { }

app.Run();
