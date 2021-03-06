﻿using System;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shuttle.Access;
using Shuttle.Core.Castle;
using Shuttle.Core.Container;
using Shuttle.Core.Data;
using Shuttle.Core.Data.Http;
using Shuttle.Esb;
using Shuttle.Recall;
using Shuttle.Sentinel.DataAccess;
using Shuttle.Sentinel.Queues;

namespace Shuttle.Sentinel.WebApi
{
    public class Startup
    {
        private IServiceBus _bus;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void OnShutdown()
        {
            _bus?.Dispose();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();

            var container = new WindsorContainer();

            var componentContainer = new WindsorComponentContainer(container);

            componentContainer.RegisterInstance<IAccessConfiguration>(AccessSection.Configuration());

            componentContainer.RegisterSuffixed("Shuttle.Sentinel");
            componentContainer.RegisterSuffixed("Shuttle.Access.Sql");

            componentContainer.Register<IInspectionQueue, DefaultInspectionQueue>();
            componentContainer.Register<IHttpContextAccessor, HttpContextAccessor>();
            componentContainer.Register<IDatabaseContextCache, ContextDatabaseContextCache>();

            ServiceBus.Register(componentContainer);
            EventStore.Register(componentContainer);

            componentContainer.Resolve<IDataStoreDatabaseContextFactory>().ConfigureWith("Sentinel");
            componentContainer.Resolve<IDatabaseContextFactory>().ConfigureWith("Sentinel");
            var inspectionQueue = componentContainer.Resolve<IInspectionQueue>();

            _bus = ServiceBus.Create(componentContainer).Start();

            return WindsorRegistrationHelper.CreateServiceProvider(container, services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );

            app.UseMvc();
        }
    }
}