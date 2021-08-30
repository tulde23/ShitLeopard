using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using ShitLeopard.Api.HostedServices;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Providers;

namespace ShitLeopard
{
    public class Startup
    {
        private readonly string ShitleopardOrigins = "ShitleopardOrigins";

        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOpenTelemetryTracing((builder) => builder
                      .AddSource("shitleopard.io")
                      .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("shitleopard.api"))
                      .SetSampler(new TraceIdRatioBasedSampler(0.25))
                      .AddAspNetCoreInstrumentation()
                      .AddHttpClientInstrumentation()
                      .AddProcessor(new BatchActivityExportProcessor(new ShitLeopard.Api.ShitleopardExporter()))
                  );
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                options.AddPolicy(ShitleopardOrigins, policy => policy
                .WithOrigins(
                    "http://shitleopard.com",
                    "https://shitleopard.com",
                    "http://www.shitleopard.com",
                    "https://www.shitleopard.com",
                    "https://tullys.online",
                    "http://www.tullys.online",
                    "https://www.tullys.online",
                    "https://localhost:44398",
                    "https://localhost:5001",
                    "https://localhost:5002")
                .AllowAnyMethod());
            });
            services.AddOptions();
            services.AddHttpClient();
            services.AddControllersWithViews().AddNewtonsoftJson(a =>
            {
                a.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            //services.AddMetrics()
            //    .AddMetricsEndpoints()
            //    .AddMetricsTrackingMiddleware()
            //    .AddAppMetricsCollectors()
            //    .AddAppMetricsGcEventsMetricsCollector()
            //    .AddAppMetricsHealthPublishing()
            //    .AddAppMetricsSystemMetricsCollector()
            //    .AddMetricsReportingHostedService();
            services.AddSpaStaticFiles(options => options.RootPath = "client-app/dist");
            services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
            services.AddHostedService<MongoHostedService>();
            services.AddAutoMapper(typeof(Startup));
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shit Leopard", Version = "v1" });
            //});
            //services.AddOpenTelemetryTracing((builder) => builder
            //         .AddAspNetCoreInstrumentation()
            //         .AddHttpClientInstrumentation()
            //         .AddConsoleExporter());
            //// For options which can be bound from IConfiguration.
            //services.Configure<AspNetCoreInstrumentationOptions>(this.Configuration.GetSection("AspNetCoreInstrumentation"));

            //// For options which can be configured from code only.
            //services.Configure<AspNetCoreInstrumentationOptions>(options =>
            //{
            //    options.Filter = (req) =>
            //    {
            //        return req.Request.Host != null;
            //    };
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // If, for some reason, you need a reference to the built container, you
            // can use the convenience extension method GetAutofacRoot.
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            //app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shit Leopard");
            //    c.RoutePrefix = "docs";
            //});

            app.UseDeveloperExceptionPage();
          //  app.UseSerilogRequestLogging();
            //     app.UseDefaultFiles();
            //     app.UseStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            }); ;

            //      app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            //    app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";
                if (env.IsDevelopment())
                {
                    // Launch development server for Vue.js
                    spa.UseVueDevelopmentServer();
                }
            });
        }
    }
}