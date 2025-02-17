using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ntgroup.APIs.Contracts;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpreadsShiftworkController : ControllerBase
{
    private readonly ISpreadsShiftworkServer context;
    //Get API Server
    private readonly ILogger<SpreadsShiftworkController> logger;
    public SpreadsShiftworkController(ILogger<SpreadsShiftworkController> _logger, ISpreadsShiftworkServer _context)
    {
        this.logger = _logger;
        this.context = _context;
    }

    [HttpGet("Shiftwork/Drives")] //Sử dụng RoleName để xác thực
    public async Task<ActionResult<List<Drive>>> GetsDriveAll()
    {
        try
        {
            return Ok(await this.context.GetsDriveAll());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}