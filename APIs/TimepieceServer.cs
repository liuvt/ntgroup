using ntgroup.APIs.Contracts;
using ntgroup.Data.Models;
using Microsoft.AspNetCore.Identity;
using ntgroup.Data.Entities;
using ntgroup.Data;

namespace ntgroup.APIs;

public class TimepieceServer : ITimepieceServer
{

    //Call dbContext to database
    private readonly ntgroupDbContext context;

    //Constructor
    public TimepieceServer(ntgroupDbContext _context)
    {
        this.context = _context;
    }

    public async Task<bool> Create(List<TimepieceDTO> models)
    {
        try
        {   
            await context.AddRangeAsync(models);
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Create method}: " + ex.Message);
        }
    }
}