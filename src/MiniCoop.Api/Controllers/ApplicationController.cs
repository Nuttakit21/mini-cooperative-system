using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCoop.Application.Interfaces;

[ApiController]
[Route("api/applications")]
[Authorize]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationRepository _service;
    public ApplicationController(IApplicationRepository service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetActiveAsync());
}