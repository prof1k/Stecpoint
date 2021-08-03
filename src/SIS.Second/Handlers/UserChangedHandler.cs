namespace SIS.Second.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using SIS.Second.Notifications;

    public class UserChangedHandler : INotificationHandler<UserChanged>
    {
        private readonly ILogger<UserChangedHandler> _logger;

        public UserChangedHandler(ILogger<UserChangedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserChanged notification, CancellationToken cancellationToken)
        {
            var user = notification.User;
            _logger.LogInformation($"Добавление пользователя: {user.Id}, {user.Email}, {user.Phone}");

            return Task.CompletedTask;
        }
    }
}