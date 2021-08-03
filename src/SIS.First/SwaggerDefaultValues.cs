namespace SIS.First
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;

    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    ///     Представляет фильтр операций Swagger/Swashbuckle, используемый для документирования неявного параметра версии API.
    /// </summary>
    /// <remarks>
    ///     Этот <see cref="IOperationFilter" /> необходим только из-за ошибок в <see cref="SwaggerGenerator" />.
    ///     Как только они будут исправлены и опубликованы, этот класс можно будет удалить.
    /// </remarks>
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        ///     Применяет фильтр к указанной операции, используя заданный контекст.
        /// </summary>
        /// <param name="operation">Операция, к которой нужно применить фильтр.</param>
        /// <param name="context">Текущий контекст фильтра операций.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            if (operation.Parameters == null)
            {
                return;
            }

            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
            foreach (OpenApiParameter parameter in operation.Parameters)
            {
                ApiParameterDescription description =
                    apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (parameter.Schema.Default == null && description.DefaultValue != null)
                {
                    parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
                }

                parameter.Required |= description.IsRequired;
            }
        }
    }
}