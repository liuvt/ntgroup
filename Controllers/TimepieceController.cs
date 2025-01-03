using ntgroup.APIs.Contracts;
using Microsoft.AspNetCore.Mvc;
using ntgroup.Data.Entities;

namespace ntgroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimepieceController : ControllerBase
{
    //Gọi Interface
    private readonly ITimepieceServer timepieceServer;

    //Constructor call server API
    public TimepieceController(ITimepieceServer _timepieceServer)
    {
        this.timepieceServer = _timepieceServer;
    }

    //Post list
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] List<TimepieceDTO> models)
    {
        try
        {
            var result = await this.timepieceServer.Create(models);

            if (result == false) return NoContent();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}