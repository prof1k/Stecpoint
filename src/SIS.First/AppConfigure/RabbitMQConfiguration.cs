namespace SIS.First.AppConfigure
{
    /// <summary>
    /// Настройки для RabbitMQ.
    /// </summary>
    public class RabbitMQConfiguration
    {
        public string Uri { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}