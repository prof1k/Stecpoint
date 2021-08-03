namespace SIS.First.Features.Users.Commands
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading;
    using System.Threading.Tasks;

    using FluentValidation;

    using MassTransit;

    using MediatR;

    using SIS.Domain.Contract.Entities;
    using SIS.Domain.Model.Messages;
    using SIS.First.FluentValidators;

    public class AddUserCommand : IRequest
    {
        /// <summary>
        /// Адрес почтового ящика.
        /// </summary>
        [StringLength(255)]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Имя.
        /// </summary>
        [StringLength(255)]
        public string FirstName { get; set; } = default!;

        /// <summary>
        /// Фамилия.
        /// </summary>
        [StringLength(255)]
        public string LastName { get; set; } = default!;

        /// <summary>
        /// Отчество.
        /// </summary>
        [StringLength(255)]
        public string? Patronymic { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        [StringLength(13, MinimumLength = 8)]
        public string Phone { get; set; } = default!;

        public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
        {
            private readonly IPublishEndpoint _endpoint;

            public AddUserCommandHandler(IPublishEndpoint endpoint)
            {
                _endpoint = endpoint;
            }

            public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
            {
                await _endpoint.Publish<IUserChangedMessage>(new UserChangedMessage
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Patronymic = request.Patronymic,
                    Phone = request.Phone
                }, cancellationToken);

                return Unit.Value;
            }
        }

        public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
        {
            public AddUserCommandValidator()
            {
                RuleFor(c => c.FirstName).NotEmpty().WithMessage("Неверно задано имя");
                RuleFor(c => c.LastName).NotEmpty().WithMessage("Неверно задана фамилия");
                RuleFor(c => c.Phone).PhoneMustMatchRegion("RU");
                RuleFor(c => c.Email).EmailAddress().WithMessage("Неверно указан email");
            }
        }
    }
}