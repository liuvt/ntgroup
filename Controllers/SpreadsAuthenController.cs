using Microsoft.AspNetCore.Mvc;
using ntgroup.APIs.Contracts;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpreadsAuthenController : ControllerBase
{
    private readonly ISpreadsAuthenServer spreadsAuthenServer;
    //Get API Server
    private readonly ILogger<SpreadsAuthenController> logger;
    public SpreadsAuthenController(ILogger<SpreadsAuthenController> _logger, ISpreadsAuthenServer _spreadsAuthenServer)
    {
        this.logger = _logger;
        this.spreadsAuthenServer = _spreadsAuthenServer;
    }

    [HttpPost("Auth/Register")]
    public async Task<ActionResult<bool>> Register(DriverDTO model)
    {
        try
        {
            var result = await this.spreadsAuthenServer.Register(model);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("Auth/Login")]
    public async Task<ActionResult> Login(DriverDTO model)
    {
        try
        {

            var result = await this.spreadsAuthenServer.Login(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("Auth/Gets")]
    public async Task<ActionResult<List<Driver>>> Gets()
    {
        try
        {
            return Ok(await this.spreadsAuthenServer.Gets());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}