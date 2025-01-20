using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetCoreGeneratedDocument;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

    static Driver _driver { get; set; } = new();

    [HttpPost("Auth/Register")]
    public async Task<IActionResult> Register(DriverDTO model)
    {
        try
        {
            var result = this.spreadsAuthenServer.Register(model);
            
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

             var result = this.spreadsAuthenServer.Login(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}