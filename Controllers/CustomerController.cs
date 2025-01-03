using ntgroup.APIs.Contracts;
using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ntgroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
        //Get API Server
    private readonly ICustomerServer context;
    private readonly ILogger<CustomerController> logger;
    public CustomerController(ICustomerServer _context, ILogger<CustomerController> _logger)
    {
        this.context = _context;
        this.logger = _logger;
    }
}