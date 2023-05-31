using CoppelAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "server=localhost;database=coppel;username=root;password=root";

builder.Services.AddControllers();

builder.Services.AddDbContext<CoppelContext>(x =>
x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)
));

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.Run();
