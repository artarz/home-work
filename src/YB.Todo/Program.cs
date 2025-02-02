using Microsoft.EntityFrameworkCore;
using YB.Data.Repository.Implementation;
using YB.Data.Repository.Interface;
using YB.Data.ToDo;
using YB.Service.ToDoService;

const string localHostOrigins = "_localHostOrigins";

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;
var connString = builder.Configuration.GetConnectionString("TodoConnString");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(opts => opts.AddPolicy(name: localHostOrigins, policy =>
{
    policy.WithOrigins("localhost:3000", "http://localhost:3000");
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ToDoDBContext>(x => x.UseSqlServer(connString));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IToDoService, ToDoService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
            //.AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(localHostOrigins);
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();