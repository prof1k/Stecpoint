namespace SIS.Second.Features.Users.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using FluentValidation;

    using MediatR;

    using SIS.Domain.Model.Entity;
    using SIS.Second.Context;
    using SIS.Second.Context.Models;
    using SIS.Second.Extensions.Context;
    using SIS.Second.Features.Users.Models;

    public class GetUserCommand : IRequest<Pagination<UserListDto>>
    {
        /// <summary>
        /// Количество запрашиваемых данных.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Количество пропускаемых данных.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Уникальный идентификатор организации.
        /// </summary>
        public Guid? OrganizationId { get; set; }

        public class GetUserCommandHandler : IRequestHandler<GetUserCommand, Pagination<UserListDto>>
        {
            private readonly SisContext _context;
            private readonly IMapper _mapper;

            public GetUserCommandHandler(SisContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Pagination<UserListDto>> Handle(GetUserCommand request,
                CancellationToken cancellationToken)
            {
                var paginationUsers = await _context.Users.Where(it => it.OrganizationId == request.OrganizationId)
                    .ToPaginationAsync<User, UserListDto>(request.Count, request.Offset, _mapper.ConfigurationProvider);

                return paginationUsers;
            }
        }

        public class GetUserCommandValidator : AbstractValidator<GetUserCommand>
        {
            public GetUserCommandValidator()
            {
                RuleFor(it => it.Count).GreaterThan(0)
                    .WithMessage("Количество требуемых данных должно быть больше нуля.");
                RuleFor(it => it.Offset).GreaterThanOrEqualTo(0)
                    .WithMessage("Количество пропускаемых данных должно быть больше или равно нулю.");
            }
        }
    }
}