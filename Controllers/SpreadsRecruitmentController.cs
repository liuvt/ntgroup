using ntgroup.APIs.Contracts;
using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ntgroup.Controllers;

#region 1 InformationApplies
// API Controller: InformationApplies
[Route("api/[controller]")]
[ApiController]
public class InformationAppliesController : ControllerBase
{
    private readonly ISpreadsRecruitmentServer context;
    public InformationAppliesController(ISpreadsRecruitmentServer _context)
    {
        this.context = _context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InformationApply>>> GetsInformationApplies()
    {
        try
        {
            return Ok(await context.GetsInformationApplies());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InformationApply>> GetInformationApply(string id)
    {
        try
        {
            var result = await context.GetInformationApply(id);

            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreateInformationApply(InformationApply model)
    {

        try
        {
            var result = await context.CreateInformationApply(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpPut("Update")]
    public async Task<ActionResult<InformationApply>> UpdateInformationApply(InformationApply model)
    {
        try
        {
            var result = await context.UpdateInformationApply(model);

            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteInformationApply(string id)
    {
        try
        {
            return Ok(await context.DeleteInformationApply(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("Row/{id}")]
    public async Task<ActionResult> DeleteRowInformationApply(string id)
    {
        try
        {
            return Ok(await context.DeleteRowInformationApply(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}
#endregion

#region 2 Companies
// API Controller: Companies
[ApiController]
[Route("api/[controller]")]
public partial class CompaniesController : ControllerBase
{
    private readonly ISpreadsRecruitmentServer context;
    public CompaniesController(ISpreadsRecruitmentServer _context)
    {
        this.context = _context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Company>>> GetsCompanies()
    {
        try
        {
            return Ok(await context.GetsCompanies());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetCompany(string id)
    {
        try
        {
            var result = await context.GetCompany(id);

            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Company>> CreateCompany(Company model)
    {

        try
        {
            var result = await this.context.CreateCompany(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCompany(string id)
    {
        try
        {
            return Ok(await context.DeleteCompany(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("Row/{id}")]
    public async Task<ActionResult> DeleteRowCompany(string id)
    {
        try
        {
            return Ok(await context.DeleteRowCompany(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}
#endregion

#region 3 Jobs
// API Controller: Jobs
[ApiController]
[Route("api/[controller]")]
public partial class JobsController : ControllerBase
{
    private readonly ISpreadsRecruitmentServer context;
    public JobsController(ISpreadsRecruitmentServer _context)
    {
        this.context = _context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Job>>> GetsJobs()
    {
        try
        {
            return Ok(await context.GetsJobs());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Job>> GetJob(string id)
    {
        try
        {
            var result = await context.GetJob(id);

            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Job>> CreateJob(Job model)
    {

        try
        {
            var result = await context.CreateJob(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteJob(string id)
    {
        try
        {
            return Ok(await context.DeleteJob(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("Row/{id}")]
    public async Task<ActionResult> DeleteRowJob(string id)
    {
        try
        {
            return Ok(await context.DeleteRowJob(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}
#endregion

#region 4 Recruitments
// API Controller: Recruitments
[Route("api/[controller]")]
[ApiController]
public class RecruitmentsController : ControllerBase
{
    private readonly ISpreadsRecruitmentServer context;
    public RecruitmentsController(ISpreadsRecruitmentServer _context)
    {
        this.context = _context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recruitment>>> GetsRecruitments()
    {
        try
        {
            return Ok(await context.GetsRecruitments());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recruitment>> GetRecruitment(string id)
    {
        try
        {
            var result = await context.GetRecruitment(id);

            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Recruitment>> CreateRecruitment(Recruitment model)
    {

        try
        {
            var result = await context.CreateRecruitment(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRecruitment(string id)
    {
        try
        {
            return Ok(await context.DeleteRecruitment(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }

    [HttpDelete("Row/{id}")]
    public async Task<ActionResult> DeleteRowRecruitment(string id)
    {
        try
        {
            return Ok(await context.DeleteRowRecruitment(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                                                "Error: " + ex.Message);
        }
    }
}
#endregion