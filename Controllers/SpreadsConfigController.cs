using ntgroup.APIs.Contracts;
using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
}