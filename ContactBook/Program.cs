using ContactBook.Projectors;
using ContactBook.Repositories;
using ContactBook.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IUserWriteRepository, UserWriteRepository>();
builder.Services.AddTransient<IUserReadRepository, UserReadRepository>();
builder.Services.AddTransient<IUserWriteService, UserWriteService>();
builder.Services.AddTransient<IUserReadService, UserReadService>();
builder.Services.AddTransient<IUserProjector, UserProjector>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
