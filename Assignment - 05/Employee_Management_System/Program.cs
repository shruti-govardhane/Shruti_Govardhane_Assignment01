using Employee_Management_System.CosmosDB;
using Employee_Management_System.Interface;
using Employee_Management_System.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeBasicDetails, EmployeeBasicDetailsService>();
builder.Services.AddScoped<IEmployeeAdditionalDetails, EmployeeAdditionalDetailsService>();
builder.Services.AddScoped<ICosmosDBService, CosmosDBService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
