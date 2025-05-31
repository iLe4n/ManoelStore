
using Microsoft.EntityFrameworkCore;
using ManoelStore.Data;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//Configure DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(connectionString));


var app = builder.Build();

//Migrations at startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<StoreDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying migrations");
        //
    }
}

//Configure HTTP request pipeline (Basically)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => //
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ManoelStore API V1");
        c.RoutePrefix = string.Empty; //
    });
}

app.UseHttpsRedirection();

app.UseAuthorization(); //If i want to use Authentication

app.MapControllers(); //Maps the Controllerss.

app.Run(); /*case-sensitive (Why this comment? Because I wrote 'one' once and I didn't 
understand why return error XD junior issues)*/
