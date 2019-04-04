namespace ProductCatalog.API
{
    using System;
    using System.IO;
    using AutoMapper;
    using DataAccess;
    using Logger;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Console;
    using ProductCatalogGateway;
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        //private readonly ILogger _logger;
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true).AddEnvironmentVariables();
            
            configuration = builder.Build();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAll",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ));
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connection = $"{Configuration["ConnectionString:LocalDBConnectionString"]}";
            services.AddDbContext<ProductCatalogContext>(options =>
                options.UseSqlServer(connection, b => b.MigrationsAssembly("ProductCatalog.API")));

            services.AddAutoMapper();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info
            {
                Version = "v1",
                Title = "Product Catalog",
                Description = "Product Catalog Operations"
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors("AllowAll");

            loggerFactory.AddConsole();
            var logger = loggerFactory.CreateLogger<ConsoleLogger>();
            logger.LogInformation("Executing Configuration()");

            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Catalogs");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}