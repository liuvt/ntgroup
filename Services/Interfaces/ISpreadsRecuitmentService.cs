using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ISpreadsRecuitmentService
{
    Task<List<Job>> GetsJobs();
} 
