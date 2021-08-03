namespace SIS.Second.Features.Organizations.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using FluentValidation;

    using MediatR;

    using Microsoft.EntityFrameworkCore;

    using SIS.Second.Context;
    using SIS.Second.Infrastructure.Exceptions;

    public class RefUserCommand : IRequest
    {
        /// <summary>
        /// Уникальный идентификатор организации.
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        public class RefUserCommandHandler : IRequestHandler<RefUserCommand>
        {
            private readonly SisContext _context;

            public RefUserCommandHandler(SisContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RefUserCommand request, CancellationToken cancellationToken)
            {
                var organization = await _context.Organizations.AsNoTracking()
                    .SingleOrDefaultAsync(it => it.Id == request.OrganizationId, cancellationToken);

                if (organization == null)
                    throw new BusinessException("Не удалось найти организацию");

                var user = await _context.Users.SingleOrDefaultAsync(it => it.Id == request.UserId, cancellationToken);

                if (user == null)
                    throw new BusinessException("Не удалось найти пользователя");

                user.OrganizationId = organization.Id;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

        public class RefUserCommandValidator : AbstractValidator<RefUserCommand>
        {
            public RefUserCommandValidator()
            {
                RuleFor(it => it.UserId).NotEmpty().WithMessage("Уникальный идентификатор пользователя обязателен.");
                RuleFor(it => it.OrganizationId).NotEmpty()
                    .WithMessage("Уникальный идентификатор организации обязателен.");
            }
        }
    }
}