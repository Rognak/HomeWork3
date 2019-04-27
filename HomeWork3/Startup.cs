using HomeWork3.BusinessLogic;
using HomeWork3.ConsumerHandler;
using HomeWork3.Messages;
using HomeWork3.Services;
using HomeWork3.Services.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace HomeWork3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<GetUsersInfoRequestHandler>();
            services.AddScoped<IUserInfoService, UserInfoService>();

            services.AddScoped<UpdateUserInfoRequestHandler>();
            services.AddScoped<IUpdateUserInfo, UpdateInfoService>();
            services.AddScoped<ISendUser, SendUser>();

            // Register your consumers if the need dependencies
            services.AddScoped<UpdateDBConsumer>();

            services.AddHealthChecks();

            services.AddMassTransit(c =>
            {
                c.AddConsumer<UpdateDBConsumer>();
            });

            services.AddSingleton(provider => MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host("localhost", "/", h => { });

                cfg.ReceiveEndpoint(host, "submit", e =>
                {
                    e.PrefetchCount = 16;
                    e.LoadFrom(provider);
                });
            }));

            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IHostedService, BusService>();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
