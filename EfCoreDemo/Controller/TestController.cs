using Microsoft.AspNetCore.Mvc;

namespace EfCoreDemo.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet]
    public string Get() => "OK";
}
