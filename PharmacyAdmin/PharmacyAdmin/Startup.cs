using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using PharmacyAdmin.Service;
using PharmacyAdmin.Helpers;
using Blazored.Toast;
using System;

namespace PharmacyAdmin
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
            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(options => Configuration.Bind("AzureAd", options));

            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddRazorPages();

            services.AddServerSideBlazor().AddHubOptions(hub => hub.MaximumReceiveMessageSize = 100 * 1024 * 1024); // 100 MB

            /* SignalR issue. https://github.com/dotnet/aspnetcore/issues/18840
            services.AddServerSideBlazor().AddHubOptions(options =>
                                                                  {
                                                                  options.MaximumReceiveMessageSize = 100 * 1024 * 1024; // 100 MB
                                                                  options.ClientTimeoutInterval = TimeSpan.FromSeconds(600);
                                                                  options.HandshakeTimeout = TimeSpan.FromSeconds(300);
                                                                  options.KeepAliveInterval = TimeSpan.FromSeconds(150);}); 
            */

            // Add Telerik Blazor server side services
            services.AddTelerikBlazor();
            services.AddBlazoredToast();
            services.AddHttpClient<HttpService>();

            services.AddSignalR(e => {
                e.MaximumReceiveMessageSize = 1000;
                e.EnableDetailedErrors = true;
            });

            /*
            services.AddSignalR(options => {
                                             options.MaximumReceiveMessageSize = 1000;
                                             options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
                                             options.HandshakeTimeout = TimeSpan.FromSeconds(30);
                                             options.KeepAliveInterval = TimeSpan.FromSeconds(15);
                                             options.EnableDetailedErrors = true;
            });
            */

            services.AddDbContext<Data.DatabaseContext>(options =>
                          options.UseSqlServer(
                              Configuration.GetConnectionString("DefaultConnection")));
            //Article service  
            services.AddScoped<IArticleService, ArticleService>();
			services.AddSingleton<WeatherForecastService>();
            services.AddScoped<IAmbrisentanREMService, AmbrisentanREMService>();
            services.AddScoped<ISqlService, SqlService>();
            services.AddScoped<IUserService, UserService>();

            // Services shared throught out the application
            services.AddScoped<IDapperService, DapperService>();
            services.AddScoped<StateHelper>();
            services.AddScoped<ScheduleService>();
            services.AddScoped<DocumentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            /*
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(@"C:\temp\FedEx", "CPRPlus")),
                RequestPath = "/documents"
            });
            */

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
