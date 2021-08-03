namespace SIS.Second.Infrastructure.Filters
{
    using System.Net;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using SIS.Second.Infrastructure.ActionResults;
    using SIS.Second.Infrastructure.Exceptions;

    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            switch (context.Exception)
            {
                case BusinessException businessException:
                    var problemDetails = new ValidationProblemDetails
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Произошла ошибка, изучите детали для информации."
                    };

                    problemDetails.Errors.Add("DomainValidations", new[] { businessException.Message });

                    context.Result = new BadRequestObjectResult(problemDetails);
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
                default:
                    var json = new JsonErrorResponse
                    {
                        Messages = new[]
                        {
                            "Произошла исключительная ошибка, повторите позже или обратитесь к системному администратору."
                        }
                    };

                    if (_env.IsDevelopment())
                        json.DeveloperMessage = context.Exception;

                    context.Result = new InternalServerErrorObjectResult(json);
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }

            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public object DeveloperMessage { get; set; } = default!;
            public string[] Messages { get; set; } = default!;
        }
    }
}