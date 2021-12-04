using Autofac;
using Autofac.Extensions.DependencyInjection;
using Vulder.Timetable.Application;
using Vulder.Timetable.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultCorsPolicy();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuild =>
{
    containerBuild.RegisterModule(new InfrastructureModule());
    containerBuild.RegisterModule(new ApplicationModule());
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CORS");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}