namespace SIS.Second.AppConfigure
{
    /// <summary>
    /// Настройки для RabbitMQ.
    /// </summary>
    public class RabbitMQConfiguration
    {
        public string Uri { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}