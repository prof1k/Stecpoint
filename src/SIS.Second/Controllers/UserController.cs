namespace SIS.Second.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using SIS.Second.Context.Models;
    using SIS.Second.Controllers.Base;
    using SIS.Second.Features.Users.Commands;
    using SIS.Second.Features.Users.Models;

    /// <summary>
    /// Пользователи.
    /// </summary>
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Пагинированный список пользователей.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        /// <returns>Список пользователей.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Pagination<UserListDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Users([FromBody] GetUserCommand client, CancellationToken token)
        {
            var users = await _mediator.Send(client, token);
            return Ok(users);
        }
    }
}