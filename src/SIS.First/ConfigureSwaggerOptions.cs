namespace SIS.First
{
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;

    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Конфигурация сваггер.
    /// </summary>
    /// <remarks>
    /// Это позволяет определять версию API, чтобы определить Swagger-документ для каждой версии API после
    /// <see cref="IApiVersionDescriptionProvider" /> услуга была определена из контейнера услуг.
    /// </remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // добавить swagger-документ для каждой обнаруженной версии API
            // примечание: вы можете пропустить или документировать устаревшие версии API по-разному
            foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "UserPublisher",
                Description = "Сервис взаимодействия с шиной модели User",
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
                info.Description += "Данная версия API устарела.";

            return info;
        }
    }
}