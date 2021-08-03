namespace SIS.First.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserDto
    {
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

        /// <summary>
        /// Адрес почтового ящика.
        /// </summary>
        [StringLength(255)]
        public string Email { get; set; } = default!;

        public Guid Id { get; set; }
    }
}