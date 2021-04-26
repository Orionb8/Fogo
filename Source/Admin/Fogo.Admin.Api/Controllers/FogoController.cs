using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Fogo.Api.Controllers {

    [ApiController]
    [Route("")]
    [Route("api")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FogoController : ControllerBase {

        [HttpGet]
        public IActionResult Get() => Ok(new StringBuilder()
            .Append($"Fogo administration\nVersion {HttpContext.GetRequestedApiVersion()}")
            .Append("\n© 2021 Fogo.\nAll rights reserved.")
            .ToString());
    }
}