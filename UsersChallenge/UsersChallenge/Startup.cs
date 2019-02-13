﻿using System;
using Core.Api.Extensions;
using Core.Api.Filters;
using Core.Common.Data;
using Core.EF.Config;
using Identity.Domain.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UsersChallenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.ConfigureCors();

            // Exception Filter injection
            services.AddTransient<IExceptionFilter, ExceptionFilter>();
            services.AddMvcCore(options =>
                {
                    options.Filters.AddService<IExceptionFilter>();
                })
                .AddAuthorization()
                .AddJsonFormatters();

            // Injection of all business interfaces
            services.BindAll<IService>(AppDomain.CurrentDomain);
            services.BindAllFromGeneric(AppDomain.CurrentDomain, typeof(IRepository<>));

            // Injection of Random Users HttpClient
            services.AddSingleton<IRandomUsersHttpClient, RandomUsersHttpClient>(_ =>
                new RandomUsersHttpClient(Configuration.GetSection("RandomUsersEndpoint")?.Value));

            // Config and injection of EF
            services.ConfigureEntityFramework(Configuration.GetConnectionString("Sqlite"));
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
