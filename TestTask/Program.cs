using Codebridge_TestTask.Data;
using Codebridge_TestTask.Interfaces;
using Codebridge_TestTask.MapperConfigs;
using Codebridge_TestTask.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IAppDbContext, AppDbContext>();
builder.Services.AddAutoMapper(typeof(DogMappingProfile));
builder.Services.AddScoped<IDogService, DogService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

public class ApplicationDbContext
{
}