using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace NegocieOnline.Hackathon.Restfull
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting up");
#pragma warning disable CS0618 // O tipo ou membro é obsoleto
                CreateHostBuilder(args).Build().Run();
#pragma warning restore CS0618 // O tipo ou membro é obsoleto
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        [Obsolete("UseSerilog(IWebHostBuilder) is obsolete, prefer UseSerilog(IHostBuilder)")]
        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<DebtContext>(options =>
                        options.UseMySql(
                            hostContext.Configuration.GetConnectionString("DefaultConnection"),
                            new MySqlServerVersion(new Version(8, 0, 26))));

                    services.AddControllers();
                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Name", Version = "v1" });
                    });

                    // Adicionando política CORS
                    services.AddCors(options =>
                    {
                        options.AddPolicy("AllowAllOrigins",
                            builder => builder.AllowAnyOrigin()
                                              .AllowAnyMethod()
                                              .AllowAnyHeader());
                    });
                })
                .Configure(app =>
                {
                    var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

                    if (env.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();
                        app.UseSwagger();
                        app.UseSwaggerUI(c =>
                        {
                            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Name V1");
                        });
                    }
                    else
                    {
                        app.UseHsts();
                    }

                    // Habilitando a política CORS
                    app.UseCors("AllowAllOrigins");

                    app.UseHttpsRedirection();
                    app.UseRouting();
                    app.UseAuthorization();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                })
                .UseSerilog();
    }
}
