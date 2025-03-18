using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ntgroup.APIs.Contracts;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeductController : ControllerBase
{
    private readonly ISpreadsReportServer context;
    //Get API Server
    public DeductController(ISpreadsReportServer _context)
    {
        this.context = _context;
    }

    //[Authorize(Roles = "Owner,Manager,Accountant,Checker")]
    [HttpGet("Reports/Details")]
    public async Task<ActionResult<List<DeductDetail>>> GetsCashbasisDetails()
    {
        try
        {
            return Ok(await this.context.GetsDeductDetails());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    //[Authorize(Roles = "Owner,Manager,Accountant,Checker")]
    [HttpGet("Reports/")]
    public async Task<ActionResult<Deduct>> GetsDeduct()
    {
        try
        {
            return Ok(await this.context.GetsDeduct());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    //[Authorize(Roles = "Owner,Manager,Accountant,Checker")]
    [HttpGet("Reports/{msnv}")]
    public async Task<ActionResult<Deduct>> GetsDeduct(string msnv)
    {
        try
        {
            return Ok(await this.context.GetDeductDetail(msnv));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    //[Authorize(Roles = "Owner,Manager,Accountant,Checker")]
    [HttpGet("Reports/Month/{month}")]
    public async Task<ActionResult<Deduct>> GetsDeductByMonth(string month)
    {
        try
        {
            return Ok(await this.context.GetsDeductByMonth(month));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}