namespace SIS.First.Controllers.Base
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Базовый контроллер.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        
    }
}