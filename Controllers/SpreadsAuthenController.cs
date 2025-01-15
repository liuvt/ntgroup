using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ntgroup.APIs.Contracts;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpreadsAuthenController : ControllerBase
{

    //Get API Server
    private readonly ISpreadsAuthenServer context;
    private readonly ILogger<SpreadsAuthenController> logger;
    public SpreadsAuthenController(ISpreadsAuthenServer _context, ILogger<SpreadsAuthenController> _logger)
    {
        this.context = _context;
        this.logger = _logger;
    }
    static Driver _driver { get; set; } = new();

    [HttpPost("Auth/Register")]
    public async Task<IActionResult> Register(DriverDTO driverDTO)
    {
        try
        {
            var passwordHash = new PasswordHasher<DriverDTO>().HashPassword(driverDTO, driverDTO.Password);

            _driver = new Driver
            {
                Id = Guid.NewGuid().ToString(),
                Username = driverDTO.Username,
                PasswordHash = passwordHash
            };

            return Ok(_driver);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("Auth/Login")]
    public async Task<ActionResult> Login(DriverDTO driverDTO)
    {
        try
        {
            if (driverDTO.Username != _driver.Username) throw new Exception("Sai tài khoản");

            var verify = new PasswordHasher<DriverDTO>().VerifyHashedPassword(driverDTO, _driver.PasswordHash, driverDTO.Password);

            if (verify == PasswordVerificationResult.Failed) throw new Exception("Sai mật khẩu");

            var token = await this.CreateToken();

            return Ok(token);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    private async Task<string> CreateToken()
    {
        try
        {
            //Thông tin User đưa vào Token
            var listClaims = new List<Claim>
                        {
                            new Claim("id", _driver.Id),
                            new Claim("username", _driver.Username),
                            new Claim(ClaimTypes.Role, "Employee"),
                            new Claim(JwtRegisteredClaimNames.Jti, _driver.Id)
                        };

            //Khóa bí mật
            var autKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my Secret, my infomation: vanlt490811@gmail.com , the key size must be greater than: 512 bits"));

            //Tạo chữ ký với khóa bí mật
            var signCredentials = new SigningCredentials(autKey, SecurityAlgorithms.HmacSha512Signature);

            var autToken = new JwtSecurityToken(
                claims: listClaims, //Thông tin User
                issuer: "https://localhost:5110",
                audience: "Taxi Nam Thang Manager API",
                expires: DateTime.Now.AddDays(30), //Thời gian tồn tại Token
                signingCredentials: signCredentials //Chữ ký
            );

            return new JwtSecurityTokenHandler().WriteToken(autToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}