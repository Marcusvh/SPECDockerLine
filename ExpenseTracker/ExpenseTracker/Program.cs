using ExpenseTracker.Context;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Manager;
using ExpenseTracker.Seeding;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ExpenseContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(9, 0, 4))
    )
);

builder.Services.AddScoped<ICategoryManager, CategoryManager>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<ITransactionManager, TransactionManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ExpenseContext>();
    
    DbSeeder.Seed(dbContext);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
