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

    [Authorize(Roles = "Owner,Manager,Accountant,Checker")]
    [HttpGet("Reports/Month/{month}/User/{userId}")]
    public async Task<ActionResult<StatisticalReport>> GetsStatisticalReportByUserId(string month, string userId)
    {
        try
        {
            return Ok(await this.context.GetsStatisticalReportByUserID(month, userId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [Authorize(Roles = "Owner,Manager,Accountant,Checker")]
    [HttpGet("Reports/Month/{month}")]
    public async Task<ActionResult<StatisticalReport>> GetsStatisticalReportByMonth(string month)
    {
        try
        {
            return Ok(await this.context.GetsStatisticalReportByMonth(month));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [Authorize(Roles = "Owner,Manager,Accountant,Checker")]
    [HttpGet("Reports/")]
    public async Task<ActionResult<List<StatisticalReport>>> GetsStatisticalReportDetail()
    {
        try
        {
            return Ok(await this.context.GetsStatisticalReportDetail());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}