using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ntgroup.APIs.Contracts;

public interface ISpreadsShiftworkServer
{
    Task<List<Drive>> GetsDriveAll ();
    Task<Drive> GetDriveById (string drive_Id);
    Task<bool> CreateDrive(Drive model);
    Task<bool> UpdateDrive(Drive model);
    Task<bool> DeleteDrive(string drive_Id);
    Task<bool> DeleteRowDrive(string drive_Id);
} 
