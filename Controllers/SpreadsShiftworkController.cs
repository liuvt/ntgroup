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

    [HttpGet("Shiftwork/Drives"), Authorize(Roles = "Owner")] //Sử dụng RoleName để xác thực Owner
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

    [HttpGet("Shiftwork/Drives/{drive_Id}"), Authorize] // Sử dụng xác thực mặc định
    public async Task<ActionResult<Drive>> GetDriveById(string drive_Id)
    {
        try
        {
            return Ok(await this.context.GetDriveById(drive_Id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpPost("Shiftwork/Drives/"), Authorize(Roles = "Owner")] //Sử dụng RoleName để xác thực Owner
    public async Task<ActionResult<string>> CreateDrive(Drive model)
    {
        try
        {
            return Ok(
                await this.context.CreateDrive(model) == true ? 
                "Tạo thành công" : 
                "Lỗi không thể tạo"
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}