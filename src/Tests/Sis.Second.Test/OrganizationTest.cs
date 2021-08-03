namespace Sis.Second.Test
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Microsoft.EntityFrameworkCore;

    using SIS.Domain.Model.Entity;
    using SIS.Second.Context;
    using SIS.Second.Features.Organizations.Commands;

    using Xunit;

    public class OrganizationTest
    {
        [Fact]
        public async void RefUserCommandTest()
        {
            var options = new DbContextOptionsBuilder<SisContext>()
                .UseInMemoryDatabase("SisDatabase")
                .Options;

            await using var context = new SisContext(options);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@test.com",
                FirstName = "тест",
                LastName = "Тестов",
                Patronymic = "Тестович",
                Phone = "+79131133131"
            };

            await context.AddAsync(user);

            var organization = new Organization
            {
                Id = Guid.NewGuid(),
                Name = "Тестовая организация"
            };
            await context.AddAsync(organization);

            await context.SaveChangesAsync();

            var refUserCommand = new RefUserCommand
            {
                OrganizationId = organization.Id,
                UserId = user.Id
            };

            var refUserCommandHandler = new RefUserCommand.RefUserCommandHandler(context);
            var handle = await refUserCommandHandler.Handle(refUserCommand, Task.Factory.CancellationToken);

            context.Users.First(it => it.Id == user.Id).OrganizationId.Should().Be(organization.Id);
        }
    }
}