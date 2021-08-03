namespace SIS.First
{
    using System;
    using System.Linq;
    using System.Reflection;

    using FluentValidation;

    using GreenPipes;

    using MassTransit;
    using MassTransit.RabbitMqTransport.Configuration;

    using MediatR;

    using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using RabbitMQ.Client;

    using SIS.First.AppConfigure;
    using SIS.First.Extensions;
    using SIS.First.Infrastructure.Behaviours;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    // создать конечную точку swagger для каждой обнаруженной версии API
                    foreach (ApiVersionDescription description in provider.ApiVersionDescriptions
                        .OrderByDescending(x => x.ApiVersion.MajorVersion)
                        .ThenByDescending(x => x.ApiVersion.MinorVersion))
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            services.AddCustomMVC(Configuration)
                .AddSwagger(Configuration)
                .AddFluentValidationRulesToSwagger();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, configurator) =>
                {
                    var rabbitMqConfiguration = new RabbitMQConfiguration();
                    Configuration.Bind("RabbitMQ", rabbitMqConfiguration);
                    configurator.Host(new Uri(rabbitMqConfiguration.Uri), hostConfigurator =>
                    {
                        hostConfigurator.Username(rabbitMqConfiguration.UserName);
                        hostConfigurator.Password(rabbitMqConfiguration.Password);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}