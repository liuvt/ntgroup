using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ntgroup.APIs.Contracts;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpreadsReportController : ControllerBase
{
    private readonly ISpreadsReportServer context;
    //Get API Server
    private readonly ILogger<SpreadsReportController> logger;
    public SpreadsReportController(ILogger<SpreadsReportController> _logger, ISpreadsReportServer _context)
    {
        this.logger = _logger;
        this.context = _context;
    }


    [HttpGet("Reports/Totals")]
    public async Task<ActionResult<StatisticalReportTotal>> GetsTotal()
    {
        try
        {
            return Ok(await this.context.GetsTotal());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("Reports/Month/{month}")]
    public async Task<ActionResult<StatisticalReportTotal>> GetsTotalbyMonth(string month)
    {
        try
        {
            return Ok(await this.context.GetsTotalbyMonth(month));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("Reports/All")]
    public async Task<ActionResult<List<StatisticalReport>>> GetsAll()
    {
        try
        {
            return Ok(await this.context.Gets());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("Reports/{id}")] 
    public async Task<ActionResult<StatisticalReport>> Get(string id)
    {
        try
        {
            return Ok(await this.context.Get(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("Reports/User/{userId}")] 
    public async Task<ActionResult<List<StatisticalReport>>> GetsByUserId(string userId)
    {
        try
        {
            return Ok(await this.context.GetsByUserId(userId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("Reports/Yesterday")] 
    public async Task<ActionResult<List<StatisticalReport>>> GetsYesterday()
    {
        try
        {
            return Ok(await this.context.GetsYesterday());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("Reports/")] 
    public async Task<ActionResult<StatisticalReportTotal>> GetsYesterdayTotal()
    {
        try
        {
            return Ok(await this.context.GetsYesterdayTotal());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("Reports/Date/{byDate}")] 
    public async Task<ActionResult<List<StatisticalReport>>> GetsByDay(string byDate)
    {
        try
        {
            return Ok(await this.context.GetsByDay(byDate));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

}