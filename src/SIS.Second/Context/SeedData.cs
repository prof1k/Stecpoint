namespace SIS.Second.Context
{
    using System;
    using System.Linq;

    using SIS.Domain.Model.Entity;

    /// <summary>
    /// Заполнение первоночальных данных.
    /// </summary>
    internal static class SeedData
    {
        /// <summary>
        /// Добавление пользователя при необходимости.
        /// </summary>
        /// <param name="context"></param>
        internal static void AddUserIfNeed(this SisContext context)
        {
            var user1 = new User
            {
                Email = "test@test.com",
                FirstName = "Тест",
                LastName = "Тестовый",
                Patronymic = "Тестович",
                Phone = "+79138784664"
            };
            AddUserIfNeed(context, "C0B4FAB8-0A46-4411-B021-46C7568C08AD", user1);

            var user2 = new User
            {
                Email = "test2@test.com",
                FirstName = "Тест2",
                LastName = "Тестовый2",
                Patronymic = "Тестович2",
                Phone = "+79138784666"
            };
            AddUserIfNeed(context, "7FD92144-A0B7-4F8E-897D-9E200A8A8274", user2);

            var user3 = new User
            {
                Email = "test3@test.com",
                FirstName = "Тест3",
                LastName = "Тестовый3",
                Patronymic = "Тестович3",
                Phone = "+79138784667"
            };
            AddUserIfNeed(context, "259CD58A-20BE-4511-BDF3-AAC543D7A6C8", user3);

            var user4 = new User
            {
                Email = "test4@test.com",
                FirstName = "Тест4",
                LastName = "Тестовый4",
                Patronymic = "Тестович4",
                Phone = "+79138784668"
            };
            AddUserIfNeed(context, "B0E28779-7FDD-4FAC-86CB-C61DF95F6EFF", user4);

            var user5 = new User
            {
                Email = "test5@test.com",
                FirstName = "Тест5",
                LastName = "Тестовый5",
                Patronymic = "Тестович5",
                Phone = "+79138784669"
            };
            AddUserIfNeed(context, "3F8570E5-860C-41BE-AA34-48ADAE85C16D", user5);
        }

        private static void AddUserIfNeed(SisContext context, string guid, User userAdding)
        {
            var userId = Guid.Parse(guid);

            var user = context.Users.SingleOrDefault(it => it.Id == userId);

            if (user != null)
                return;

            userAdding.Id = userId;

            context.Add(userAdding);
        }

        /// <summary>
        /// Добавление организации при необходимости.
        /// </summary>
        /// <param name="context"></param>
        internal static void AddOrganizationIfNeed(this SisContext context)
        {
            AddOrganizationIfNeed(context, "68D2D6BA-C738-4C7D-A605-CF722BB4ACD8", "Тестовая организация");
            AddOrganizationIfNeed(context, "4AA77E75-C909-4AF9-8E6B-1AF5427CF800", "Тестовая организация2");
            AddOrganizationIfNeed(context, "E417CCEA-AFE1-4A32-9876-30CC2B5208C5", "Тестовая организация3");
        }

        private static void AddOrganizationIfNeed(SisContext context, string guid, string organizationName)
        {
            var organizationId = Guid.Parse(guid);

            var organization = context.Organizations.SingleOrDefault(it => it.Id == organizationId);

            if (organization != null)
                return;

            organization = new Organization
            {
                Id = organizationId,
                Name = organizationName
            };

            context.Add(organization);
        }
    }
}