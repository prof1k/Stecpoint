namespace SIS.Second.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using SIS.Second.Controllers.Base;
    using SIS.Second.Features.Organizations.Commands;

    /// <summary>
    /// Организации.
    /// </summary>
    public class OrganizationController : BaseController
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Связь организации и пользователя.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RefUser([FromBody] RefUserCommand client, CancellationToken token)
        {
            await _mediator.Send(client, token);
            return Ok("Привязка успешно");
        }
    }
}