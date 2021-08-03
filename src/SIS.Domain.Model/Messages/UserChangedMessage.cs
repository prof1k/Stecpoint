namespace SIS.Domain.Model.Messages
{
    using SIS.Domain.Contract.Entities;

    public class UserChangedMessage : IUserChangedMessage
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? Patronymic { get; set; }
        public string Phone { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}