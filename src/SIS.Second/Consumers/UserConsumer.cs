namespace SIS.Second.Consumers
{
    using System;
    using System.Threading.Tasks;

    using MassTransit;

    using MediatR;

    using SIS.Domain.Contract.Entities;
    using SIS.Domain.Model.Entity;
    using SIS.Second.Context;
    using SIS.Second.Notifications;

    public class UserConsumer : IConsumer<IUserChangedMessage>
    {
        private readonly SisContext _context;
        private readonly IMediator _mediator;

        public UserConsumer(SisContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<IUserChangedMessage> context)
        {
            var userChangedMessage = context.Message;

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = userChangedMessage.Email,
                FirstName = userChangedMessage.FirstName,
                LastName = userChangedMessage.LastName,
                Patronymic = userChangedMessage.Patronymic,
                Phone = userChangedMessage.Phone
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            _mediator.Publish(new UserChanged { User = user });
        }
    }
}