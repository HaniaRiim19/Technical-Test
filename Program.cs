using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Register services for controllers and MVC (including TempData)
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

var app = builder.Build();

// Map the controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ClassReport}/{action=Index}/{id?}");

// Define the web service route
app.MapGet("/", async () =>
{
    var connectionString = "Server=localhost,1433;Database=DevelopmentTest;User Id=sa;Password=Borninfire&3;TrustServerCertificate=True;";

    try
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("ClassRegistrationReport", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //Executing the stored procedure
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        var result = "";
                        while (await reader.ReadAsync())
                        {
                            result += $"{reader["Class"]}, {reader["Teacher Name"]}, {reader["Registrations"]}, {reader["Number Paid"]}\n";
                        }
                        return result;
                    }
                    else
                    {
                        return "No data returned from stored procedure.";
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        return $"Failed to connect or execute stored procedure: {ex.Message}";
    }
});

app.Run();
