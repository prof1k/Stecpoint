namespace SIS.Second.Features.Users.Models
{
    using System;

    public class UserListDto
    {
        /// <summary>
        /// Адрес почтового ящика.
        /// </summary>
        public string Email { get; set; } = default!;

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; } = default!;

        public Guid Id { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; } = default!;

        /// <summary>
        /// Отчество.
        /// </summary>
        public string? Patronymic { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Phone { get; set; } = default!;
    }
}