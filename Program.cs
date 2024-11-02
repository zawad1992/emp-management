using Microsoft.EntityFrameworkCore;
using HRMWeb.Data;
using HRMWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure database with retry logic
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
});

// Register services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Wait for database to be ready
        var retry = 0;
        const int maxRetries = 10;
        while (retry < maxRetries)
        {
            try
            {
                logger.LogInformation("Attempting to ensure database is created (Attempt {Retry}/{MaxRetries})", retry + 1, maxRetries);
                context.Database.EnsureCreated();
                logger.LogInformation("Database is ready");
                break;
            }
            catch (Exception ex)
            {
                retry++;
                if (retry == maxRetries)
                {
                    logger.LogError(ex, "Failed to ensure database is created after {MaxRetries} attempts", maxRetries);
                    throw;
                }
                logger.LogWarning(ex, "Failed to ensure database is created. Retrying in 5 seconds... (Attempt {Retry}/{MaxRetries})", retry, maxRetries);
                Thread.Sleep(5000); // Wait 5 seconds before retrying
            }
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while ensuring the database exists.");
    }
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

app.Run();