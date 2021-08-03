namespace SIS.Second.Infrastructure.Behaviours
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using FluentValidation;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using SIS.Second.Extensions;
    using SIS.Second.Infrastructure.Exceptions;

    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
                return await next();

            string typeName = request.GetGenericTypeName();

            _logger.LogInformation("----- Валидация команды {CommandType}", typeName);

            var context = new ValidationContext<TRequest>(request);
            var validationResults =
                await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(result => result.Errors)
                .Where(error => error != null).ToList();
            if (failures.Any())
            {
                _logger.LogWarning(
                    "Ошибка валидации - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}",
                    typeName, request, failures);

                throw new BusinessException(
                    $"Ошибки проверки команд для типа: {typeof(TRequest).Name}",
                    new ValidationException("Ошибка валидации", failures));
            }

            return await next();
        }
    }
}