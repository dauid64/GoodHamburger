using Microsoft.EntityFrameworkCore;
using GoodHamburger.Data;
using GoodHamburger.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<OrderService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<Context>(opt =>
    opt.UseInMemoryDatabase("GoodHmaburgerDb")
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    # region Seed de dados
    using (var scope = app.Services.CreateScope())
    {
        // Para banco de dados na memória é necessário fazer na mão utilizando o EnsureCreted
        // Fonte: https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
        var context = scope.ServiceProvider.GetRequiredService<Context>();
        context.Database.EnsureCreated();
    }
    # endregion
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();