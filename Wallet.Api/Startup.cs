using Npgsql;
using Wallet.Api.Hubs;
using Wallet.Data.Repositories;

namespace Wallet.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddTransient<MessageRepository>();
            services.AddTransient((serviceProvider) =>
            {
                string cs = Configuration.GetConnectionString("DefaultConnection");
                var connection = new NpgsqlConnection(cs);
                connection.Open();
                return connection;
            });
            services.AddAutoMapper(x => x.AddProfile<AutoMapperProfile>());
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("",
                      policy =>
                      {
                          policy.WithOrigins("localhost");
                          policy.WithMethods("GET", "POST");
                          policy.AllowCredentials();
                      });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<MessageHub>("MessageHub");
            });

        }
    }
}
