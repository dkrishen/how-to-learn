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
builder.Services.AddTransient<ISectionTopicRepository, SectionTopicRepository>();
builder.Services.AddTransient<ISectionRepository, SectionRepository>();
builder.Services.AddTransient<ITopicRepository, TopicRepository>();
builder.Services.AddTransient<ISectionLogic, SectionLogic>();
builder.Services.AddTransient<ITopicLogic, TopicLogic>();
builder.Services.AddTransient<IElasticRepository, ElasticRepository>();

builder.Services.AddAutoMapper(typeof(SectionMapperProfile));
builder.Services.AddAutoMapper(typeof(TopicMapperProfile));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<HowToLearnDbContext>();
    DbInitializer.Initialize(context);
}

app.Run();