namespace SIS.Second.Notifications
{
    using MediatR;

    using SIS.Domain.Model.Entity;

    public class UserChanged : INotification
    {
        public User User { get; set; }
    }
}