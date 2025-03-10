using ntgroup.Data.Models;
using ntgroup.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ntgroup.APIs.Contracts;

public interface ISpreadsRecruitmentServer
{
    #region 1. InformationApplies Server
    Task<List<InformationApply>> GetsInformationApplies(); //Done   
    Task<InformationApply> GetInformationApply(string id); //Done  
    Task<bool> CreateInformationApply(InformationApply model);
    Task<InformationApply> UpdateInformationApply(InformationApply model);
    Task<bool> DeleteInformationApply(string id);
    Task<bool> DeleteRowInformationApply(string id);
    #endregion

    #region 2. Companies Server
    Task<List<Company>> GetsCompanies(); //Done  
    Task<Company> GetCompany(string id); //Done  
    Task<bool> CreateCompany(Company model);
    Task<Company> UpdateCompany(Company model);
    Task<bool> DeleteCompany(string id);
    Task<bool> DeleteRowCompany(string id);
    #endregion

    #region 3. Jobs Server
    Task<List<Job>> GetsJobs(); //Done
    Task<Job> GetJob(string id); //Done
    Task<bool> CreateJob(Job model);
    Task<Job> UpdateJob(Job model);
    Task<bool> DeleteJob(string id);
    Task<bool> DeleteRowJob(string id);
    #endregion

    #region 4. Recruitments Server
    Task<List<Recruitment>> GetsRecruitments(); //Done
    Task<Recruitment> GetRecruitment(string id); 
    Task<bool> CreateRecruitment(Recruitment model);
    Task<Recruitment> UpdateRecruitment(Recruitment model);
    Task<bool> DeleteRecruitment(string id);
    Task<bool> DeleteRowRecruitment(string id);
    #endregion
}