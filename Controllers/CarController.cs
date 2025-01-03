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
public class CarController : ControllerBase
{
        //Get API Server
    private readonly ICarServer context;
    private readonly ILogger<CarController> logger;
    public CarController(ICarServer _context, ILogger<CarController> _logger)
    {
        this.context = _context;
        this.logger = _logger;
    }

    [HttpPost("CREATECARS")]
    public async Task<ActionResult<string>> CreateCars([FromBody] List<CarCreateDTO> models)
    {
        try
        {
            var result = await this.context.CreateCars(models);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CarCreateDTO model)
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
    public async Task<ActionResult<List<CarDTO>>> Gets()
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

    [HttpGet("{car_Id}")]
    public async Task<ActionResult<CarDTO>> Get(string car_Id)
    {
        try
        {
            var result = await this.context.Get(car_Id);

            if(result == null) return NoContent();
            return Ok(result);
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
            var result = await this.context.Update(car_Id, model);

            if (result == null) return NoContent();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> Delete(string car_Id)
    {
        try
        {
            var result = await this.context.Delete(car_Id);

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
    public async Task<ActionResult<string>> DeleteCars(List<string> carIds)
    {
        try
        {
            var result = await this.context.DeleteCars(carIds);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}