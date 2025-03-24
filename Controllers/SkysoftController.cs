using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ntgroup.APIs.Contracts;
using ntgroup.Data;
using ntgroup.Data.Entities.Skysofts;
using ntgroup.Data.Models.Skysofts;

namespace ntgroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkysoftController : ControllerBase
{
    private readonly ISkysoftServer context;
    //Get API Server
    public SkysoftController(ISkysoftServer _context)
    {
        this.context = _context;
    }

    //[Authorize(Roles = "Owner,Manager,Accountant,Checker")]
    [HttpGet("Vehicles")]
    public async Task<ActionResult<List<Vehicle>>> GetsVehicles()
    {
        try
        {
            var listVehicles = await context.GetsVehicles();
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
    [HttpPost("Trips")]
    public async Task<ActionResult<List<TripDTO>>> GetsTrips([FromBody]TripRequestDTO datereport)
    {
        try
        {
            Console.WriteLine("datereport1: "+ datereport.DateReport.ToString());

            var trips = await context.GetsTrips(datereport);
            var vehicles = await context.GetsVehicles();

            var _trips = trips.TripsDto(vehicles);
            return Ok( 
               _trips
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    //[Authorize(Roles = "Owner,Manager,Accountant,Checker")]
    [HttpPost("Trips/Date/")]
    public async Task<ActionResult<List<TripDTO>>> GetsTripsDate([FromBody]TripRequestDTO datereport)
    {
        try
        {
            var trips = await context.GetsTripsDate(datereport);
            var vehicles = await context.GetsVehicles();

            var _trips = trips.TripsDto(vehicles);
            return Ok( 
               _trips
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}