using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ntgroup.APIs.Contracts;
using ntgroup.Data.Entities;
using ntgroup.Data.Models.Skysofts;

namespace ntgroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly ISkysoftVehicleServer context;
    //Get API Server
    public VehicleController(ISkysoftVehicleServer _context)
    {
        this.context = _context;
    }

    //[Authorize(Roles = "Owner,Manager,Accountant,Checker")]
    [HttpGet]
    public async Task<ActionResult<List<Vehicle>>> GetsVehicles()
    {
        try
        {
            var listVehicles = await context.PostHTTPToSkysoftVehicles();
            Console.WriteLine(listVehicles);
            return Ok( 
               listVehicles
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    //[Authorize(Roles = "Owner,Manager,Accountant,Checker")]
    [HttpGet("{datereport}")]
    public async Task<ActionResult<List<Trip>>> GetsReports(string datereport)
    {
        try
        {
            var dt = await context.PostHTTPToSkysoftTrips(datereport);
            Console.WriteLine(dt);
            return Ok( 
               dt
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}