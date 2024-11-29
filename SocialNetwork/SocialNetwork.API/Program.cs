using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Services;
using SocialNetwork.Storage.Data;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri(builder.Configuration["VaultUri"]);
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Local DB
//builder.Services.AddDbContext<SocialNetworkContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:Local"]));

// Hosted DB
builder.Services.AddDbContext<SocialNetworkContext>(options => options.UseSqlServer(builder.Configuration["Develop"]));


builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (!EF.IsDesignTime)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<SocialNetworkContext>().Database;
        logger.LogInformation($"Connecting to database: {db.GetDbConnection().ConnectionString}");
        logger.LogInformation($"Apply migrations:");
        foreach(var migration in db.GetPendingMigrations())
        {
            logger.LogInformation(migration);
        }
        db.Migrate();
        logger.LogInformation("Migrations are applied");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();