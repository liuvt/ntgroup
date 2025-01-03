using ntgroup.APIs.Contracts;
using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DocumentFormat.OpenXml.Office2016.Excel;

namespace ntgroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
        //Get API Server
    private readonly IDriverServer context;
    private readonly ILogger<DriverController> logger;
    public DriverController(IDriverServer _context, ILogger<DriverController> _logger)
    {
        this.context = _context;
        this.logger = _logger;
    }

    [HttpPost("CREATECARS")]
    public async Task<ActionResult<string>> CreateCars([FromBody] List<DriverCreateDTO> models)
    {
        try
        {
            var result = await this.context.CreateDrivers(models);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DriverCreateDTO model)
    {
        try
        {
            var result = await this.context.Create(model);
            if(result == null) return BadRequest();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<DriverDTO>>> Gets()
    {
        try
        {
            var result = await this.context.Gets();

            if(result == null) return NoContent();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("{driver_Id}")]
    public async Task<ActionResult<DriverDTO>> Get(string driver_Id)
    {
        try
        {
            // var result = await this.context.Get(driver_Id);

            // if(result == null) return NoContent();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    
    [HttpPatch]
    public async Task<ActionResult<CarDTO>> Update(string car_Id, [FromBody] CarUpdateDTO model)
    {
        try
        {
            // var result = await this.context.Update(car_Id, model);

            // if (result == null) return NoContent();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> Delete(string driver_Id)
    {
        try
        {
            var result = await this.context.Delete(driver_Id);

            if (!result) return BadRequest();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("DELETES")]
    public async Task<ActionResult<string>> DeleteDrivers(List<string> driverIds)
    {
        try
        {
            var result = await this.context.DeleteDrivers(driverIds);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}