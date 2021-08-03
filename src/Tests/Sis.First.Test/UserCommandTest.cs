namespace Sis.First.Test
{
    using System.Threading;
    using System.Threading.Tasks;

    using MassTransit;

    using Moq;

    using SIS.Domain.Contract.Entities;
    using SIS.First.Features.Users.Commands;

    using Xunit;

    public class UserCommandTest
    {
        [Fact]
        public async void PublishEndpointTest()
        {
            var publishEndpoint = new Mock<IPublishEndpoint>();

            var command = new AddUserCommand();
            var handler = new AddUserCommand.AddUserCommandHandler(publishEndpoint.Object);
            var unit = await handler.Handle(command, Task.Factory.CancellationToken);

            publishEndpoint.Verify(x => x.Publish(It.IsAny<IUserChangedMessage>(), Task.Factory.CancellationToken));
        }
    }
}