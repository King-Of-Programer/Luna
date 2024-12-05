using Microsoft.EntityFrameworkCore;
using Luna_Edge.Data; 
using Luna_Edge.Services;
using Microsoft.Extensions.DependencyInjection;
using Luna_Edge.Services.Task;
using Luna_Edge.Services.User;
using Luna_Edge.Services.UserService.UserService;
using Luna_Edge.Repositiries;
using Luna_Edge.Services.Token;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        builder.Services.AddControllers();

       
        string? connectionString = builder.Configuration.GetConnectionString("MySQL");
        if (connectionString == null)
        {
            throw new Exception("No connection string in appsettings.json");
        }
        try
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );
        }
        catch (Exception ex)
        {
            throw new Exception("Can't connect to MySQL Server: " + ex.Message, ex);
        }

        builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
        builder.Services.AddScoped<TokenService>();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}