namespace SIS.Second.Extensions.DI
{
    using System.IO;
    using System.Reflection;

    using FluentValidation.AspNetCore;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.PlatformAbstractions;

    using Newtonsoft.Json;

    using SIS.Second.Infrastructure.Filters;

    using Swashbuckle.AspNetCore.SwaggerGen;

    public static class ServiceCollectionExtensions
    {
        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddControllers(options => { options.Filters.Add(typeof(HttpGlobalExceptionFilter)); })
                .AddFluentValidation(c =>
                {
                    c.RegisterValidatorsFromAssemblyContaining<Startup>();
                    // Опционально установите фабрику валидаторов, если у вас есть проблемы с разрешением области видимости внутри валидаторов.
                    //c.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                //options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddVersionedApiExplorer(
                options =>
                {
                    // добавить проводник версий api, который также добавляет сервис IApiVersionDescriptionProvider
                    // примечание: указанный код формата отформатирует версию как "'v'major[.minor][-status]".
                    options.GroupNameFormat = "'v'VVV";

                    // примечание: эта опция необходима только при версионировании по сегменту url. SubstitutionFormat
                    // можно также использовать для управления форматом версии API в шаблонах маршрутов
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options =>
            {
                // добавить пользовательский фильтр операций, который устанавливает значения по умолчанию
                options.OperationFilter<SwaggerDefaultValues>();

                // Установить путь к комментариям для Swagger JSON и пользовательского интерфейса.
                options.IncludeXmlComments(XmlCommentsFilePath);
            });

            return services;
        }
    }
}