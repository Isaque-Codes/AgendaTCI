using AgendaTCI.Applications.Services;
using AgendaTCI.Contexts;
using AgendaTCI.Interfaces;
using AgendaTCI.Repositories;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

Env.Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("A variável CONNECTION_STRING não foi definida no .env.");

builder.Services.AddDbContext<TCIContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IAgendaRepository, AgendaRepository>();
builder.Services.AddScoped<AgendaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

WebApplication app = builder.Build();

app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();