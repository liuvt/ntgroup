using ntgroup.APIs.Contracts;
using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ntgroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpreadsConfigController : ControllerBase
{
    //Get API Server
    private readonly ISpreadsConfigServer context;
    private readonly ILogger<SpreadsConfigController> logger;
    public SpreadsConfigController(ISpreadsConfigServer _context, ILogger<SpreadsConfigController> _logger)
    {
        this.context = _context;
        this.logger = _logger;
    }
#region Bankings
    [HttpGet("Bankings")]
    public async Task<ActionResult<Banking>> GetsBankAll()
    {
        try
        {
            return Ok(await this.context.GetsBankAll());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("Bankings/{bank_Id}")]
    public async Task<ActionResult<Banking>> GetBankById(string bank_Id)
    {
        try
        {
            return Ok(await this.context.GetBankById(bank_Id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpPost("Bankings/")]
    public async Task<ActionResult<string>> CreateBank(BankingCreateDTO model)
    {
        try
        {
            return Ok(
                await this.context.CreateBank(model) == true ? 
                "Tạo thành công tài khoản ngân hàng" : 
                "Lỗi không thể tạo tài khoản ngân hàng"
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpPut("Bankings/")]
    public async Task<ActionResult<string>> UpdateBank(BankingCreateDTO model)
    {
        try
        {
            return Ok(
                await this.context.UpdateBank(model) == true ? 
                "Cập nhật thành công" : 
                "Lỗi không thể cập nhật"
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("Bankings/{bank_Id}")]
    public async Task<ActionResult<string>> DeleteBank(string bank_Id)
    {
        try
        {
            return Ok(
                await this.context.DeleteBank(bank_Id) == true ? 
                "Xóa thành công" : 
                "Lỗi không thể xóa"
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("Bankings/DeleteRow/{bank_Id}")]
    public async Task<ActionResult<string>> DeleteRowBank(string bank_Id)
    {
        try
        {
            return Ok(
                await this.context.DeleteRowBank(bank_Id) == true ? 
                "Xóa thành công" : 
                "Lỗi không thể xóa"
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
    #endregion  

#region Areas

    [HttpGet("Areas")]
    public async Task<ActionResult<Area>> GetsAreaAll()
    {
        try
        {
            return Ok(await this.context.GetsAreaAll());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("Areas/{area_Id}")]
    public async Task<ActionResult<Area>> GetAreaById(string area_Id)
    {
        try
        {
            return Ok(await this.context.GetAreaById(area_Id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpPost("Areas/")]
    public async Task<ActionResult<string>> CreateArea(AreaCreateDTO model)
    {
        try
        {
            return Ok(
                await this.context.CreateArea(model) == true ? 
                "Tạo mới thành công" : 
                "Lỗi không thể tạo mới"
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpPut("Areas/")]
    public async Task<ActionResult<string>> UpdateArea(AreaCreateDTO model)
    {
        try
        {
            return Ok(
                await this.context.UpdateArea(model) == true ? 
                "Cập nhật thành công" : 
                "Lỗi không thể cập nhật"
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("Areas/{area_Id}")]
    public async Task<ActionResult<string>> DeleteArea(string area_Id)
    {
        try
        {
            return Ok(
                await this.context.DeleteArea(area_Id) == true ? 
                "Xóa thành công" : 
                "Lỗi không thể xóa"
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("Areas/DeleteRow/{area_Id}")]
    public async Task<ActionResult<string>> DeleteRowArea(string area_Id)
    {
        try
        {
            return Ok(
                await this.context.DeleteRowArea(area_Id) == true ? 
                "Xóa thành công" : 
                "Lỗi không thể xóa"
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    #endregion   

}