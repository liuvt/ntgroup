using ntgroup.APIs.Contracts;
using Microsoft.AspNetCore.Mvc;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftworkController : ControllerBase
{
    //Gọi Interface
    private readonly IShiftworkServer shiftworkServer;

    //Constructor call server API
    public ShiftworkController(IShiftworkServer _shiftworkServer)
    {
        this.shiftworkServer = _shiftworkServer;
    }

    //Tạo mới lên ca cho nhân viên taxi
    [HttpPost]
    public async Task<ActionResult<Shiftwork>> Post([FromBody] ShiftworkCreateDTO models)
    {
        try
        {
            var result = await this.shiftworkServer.Create(models);

            if (result == null) return NoContent();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    //Tạo lên ca cho danh sách nhân viên taxi
    [HttpPost("lists")]
    public async Task<ActionResult<string>> Posts([FromBody] List<ShiftworkCreateDTO> models)
    {
        try
        {
            var result = await this.shiftworkServer.CreateShiftworks(models);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    //HttpPatch
    [HttpPatch]
    public async Task<ActionResult<bool>> Update([FromBody] ShiftworkDTO models)
    {
        try
        {
            var result = await this.shiftworkServer.Update(models);

            if (result == false) return NoContent();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    //HttpDelete
    [HttpDelete]
    public async Task<ActionResult<bool>> Post(string sw_Id)
    {
        try
        {
            var result = await this.shiftworkServer.Deleted(sw_Id);

            if (result == false) return NoContent();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}