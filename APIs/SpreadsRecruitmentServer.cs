using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ntgroup.APIs.Contracts;
using ntgroup.Data.Entities;
using ntgroup.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using System.Globalization;
using ntgroup.Extensions;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;

namespace ntgroup.APIs;

public class SpreadsRecruitmentServer : ISpreadsRecruitmentServer
{

    protected readonly IConfiguration configuration;
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string sheetJobs = "Jobs";
    private readonly string sheetCompanies = "Companies";
    private readonly string sheetRecruitments = "Recruitments";
    private readonly string sheetInformationApplies = "InformationApplies";
    private readonly string spreadSheetId = "12oJPxnZn6IZsxPckZuVBUyirPm5NquDxMLQs27H-SK8"; //SpreadID of dbRecruitment
    private SheetsService sheetsService;

    //Constructor
    public SpreadsRecruitmentServer(IConfiguration _configuration)
    {
        this.configuration = _configuration;

        //File xác thực google tài khoản
        GoogleCredential credential;
        using (var stream = new FileStream(configuration["GoogleSheetConfig:ServiceAccount"]!, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(Scopes);
        }

        // Đăng ký service
        sheetsService = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = configuration["GoogleSheetConfig:ApplicationName"],
        });

    }

    #region 1. InformationApplies Server
    public async Task<List<InformationApply>> GetsInformationApplies()
    {
        try
        {
            var data = new List<InformationApply>();
            var range = $"{sheetInformationApplies}!A2:E";
            var values = await GGSExtensions.APIGetValues(sheetsService, spreadSheetId, range);
            if (values == null || values.Count == 0)
            {
                throw new Exception("Không có dữ liệu sheet.");
            }

            foreach (var item in values)
            {
                data.Add(new InformationApply
                {
                    information_id = item[0].ToString() ?? string.Empty,
                    information_FullName = item[1].ToString() ?? string.Empty,
                    information_Email = item[2].ToString() ?? string.Empty,
                    information_Phone = item[3].ToString() ?? string.Empty,
                    createdAt = item[4].ToString() ?? string.Empty
                });
            }

            return data;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    public async Task<InformationApply> GetInformationApply(string id)
    {
        try
        {
            var datas = await GetsInformationApplies();

            var result = datas.FirstOrDefault(a => a.information_id == id);

            return result ?? throw new Exception($"ID ({id}) không tồn tại!");
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi khi tìm kiếm: {ex.Message}");
        }
    }
    
    public async Task<bool> CreateInformationApply(InformationApply model)
    {
        try
        {

            // Lấy danh sách hiện có
            var datas = await GetsInformationApplies();

            // Kiểm tra tồn tại trùng số tài khoản và mã ngân hàng
            if (datas.Any(a => a.information_id == model.information_id))
            {
                throw new Exception($"ID này đã tồn tại.");
            }
            
            //Create extensions
            await GGSExtensions.APICreateValues(sheetsService, spreadSheetId, $"{sheetInformationApplies}!A2:E", new ValueRange 
            {
                Values = new List<IList<object>> 
                { 
                    new List<object> { 
                        Guid.NewGuid(), 
                        model.information_FullName, 
                        model.information_Email, 
                        model.information_Phone, 
                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    } 
                }
            });

            Console.WriteLine("Create success!");
            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Không thể tạo mới. {ex.Message}");
        }
    }
    
    public async Task<InformationApply> UpdateInformationApply(InformationApply model)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var datas = await GetsInformationApplies();
            
            // Tìm vị trí đối tượng cập nhật
            var byId = datas.FindIndex(a => a.information_id == model.information_id);
            if (byId == -1) // Nếu không tìm thấy information_id trả về -1
            {
                throw new Exception($"Không tồn tại không thể cập nhật");
            }

            await GGSExtensions.APIUpdateValues(sheetsService, spreadSheetId, $"{sheetInformationApplies}!B{byId+2}:D", new ValueRange
            {
                Values = new List<IList<object>> 
                { 
                    new List<object> { 
                        model.information_FullName, 
                        model.information_Email, 
                        model.information_Phone
                    } 
                }
            });

            Console.WriteLine("Update successfully.");
            return model;
        }
        catch (Exception ex)
        {
            throw new Exception($"Không thể cập nhật. {ex.Message}");
        }
    }
    
    public async Task<bool> DeleteInformationApply(string id)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var datas = await GetsInformationApplies();
            
            // Tìm vị trí đối tượng xóa
            var byId = datas.FindIndex(a => a.information_id == id);
            if (byId == -1)
            {
                throw new Exception($"ID ({id}) này không tồn tại không thể xóa");
            }
            await GGSExtensions.APIRemoveValues(sheetsService, spreadSheetId, $"{sheetInformationApplies}!A{byId+2}:E{byId+2}");

            Console.WriteLine("Clear successfully.");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Không thể xóa. {ex.Message}");
        }
    }
    
    public async Task<bool> DeleteRowInformationApply(string id)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var datas = await GetsInformationApplies();
            
            // Tìm vị trí đối tượng xóa
            var byId = datas.FindIndex(a => a.information_id == id);
            if (byId == -1)
            {
                throw new Exception($"ID ({id}) này không tồn tại không thể xóa");
            }
            
            // GOOGLE SHEET ID
            var gid = 1065425795;
            await GGSExtensions.APIRemoveRows(sheetsService, spreadSheetId, gid, byId);

            Console.WriteLine("Row deleted successfully.");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Không thể xóa row. {ex.Message}");
        }
    }
    #endregion

    #region 2. Companies Server
    public async Task<List<Company>> GetsCompanies()
    {
        try
        {
            var data = new List<Company>();
            var range = $"{sheetCompanies}!A2:F";
            var values = await GGSExtensions.APIGetValues(sheetsService, spreadSheetId, range);

            if (values == null || values.Count == 0)
            {
                throw new Exception("Không có dữ liệu sheet.");
            }

            foreach (var item in values)
            {
                data.Add(new Company
                {
                    company_id = item[0].ToString() ?? string.Empty,
                    company_name = item[1].ToString() ?? string.Empty,
                    website = item[2].ToString() ?? string.Empty,
                    description = item[3].ToString() ?? string.Empty,
                    location = item[4].ToString() ?? string.Empty,
                    created_at = item[5].ToString() ?? string.Empty
                });
            }

            return data;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    public async Task<Company> GetCompany(string id)
    {
        try
        {
            var datas = await GetsCompanies();
            var result = datas.FirstOrDefault(a => a.company_id == id);

            return result ?? throw new Exception($"Công ty với ID ({id}) không tồn tại!");
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi khi tìm kiếm: {ex.Message}");
        }
    }
    
    public async Task<bool> CreateCompany(Company model)
    {
        try
        {

            // Lấy danh sách hiện có
            var datas = await GetsCompanies();

            // Kiểm tra tồn tại trùng số tài khoản và mã ngân hàng
            if (datas.Any(a => a.company_id == model.company_id))
            {
                throw new Exception($"ID này đã tồn tại.");
            }
            
            //Create extensions
            await GGSExtensions.APICreateValues(sheetsService, spreadSheetId, $"{sheetCompanies}!A2:F", new ValueRange 
            {
                Values = new List<IList<object>> 
                { 
                    new List<object> { 
                        Guid.NewGuid(), 
                        model.company_name, 
                        model.website, 
                        model.description,
                        model.location, 
                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    } 
                }
            });

            Console.WriteLine("Create success!");
            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Không thể tạo mới. {ex.Message}");
        }
    }
    
    public async Task<Company> UpdateCompany(Company model)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var datas = await GetsCompanies();
            
            // Tìm vị trí đối tượng cập nhật
            var byId = datas.FindIndex(a => a.company_id == model.company_id);
            if (byId == -1) // Nếu không tìm thấy information_id trả về -1
            {
                throw new Exception($"Không tồn tại không thể cập nhật");
            }

            await GGSExtensions.APIUpdateValues(sheetsService, spreadSheetId, $"{sheetCompanies}!B{byId+2}:E", new ValueRange
            {
                Values = new List<IList<object>> 
                { 
                    new List<object> { 
                        model.company_name, 
                        model.website, 
                        model.description,
                        model.location
                    }
                }
            });

            Console.WriteLine("Update successfully.");
            return model;
        }
        catch (Exception ex)
        {
            throw new Exception($"Không thể cập nhật. {ex.Message}");
        }
    }
    
    public async Task<bool> DeleteCompany(string id)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var datas = await GetsCompanies();
            
            // Tìm vị trí đối tượng xóa
            var byId = datas.FindIndex(a => a.company_id == id);
            if (byId == -1)
            {
                throw new Exception($"ID ({id}) này không tồn tại không thể xóa");
            }
            await GGSExtensions.APIRemoveValues(sheetsService, spreadSheetId, $"{sheetCompanies}!A{byId+2}:F{byId+2}");

            Console.WriteLine("Clear successfully.");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Không thể xóa. {ex.Message}");
        }
    }
    
    public async Task<bool> DeleteRowCompany(string id)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var datas = await GetsCompanies();
            
            // Tìm vị trí đối tượng xóa
            var byId = datas.FindIndex(a => a.company_id == id);
            if (byId == -1)
            {
                throw new Exception($"ID ({id}) này không tồn tại không thể xóa");
            }
            
            // GOOGLE SHEET ID
            var gid = 1833927514;
            await GGSExtensions.APIRemoveRows(sheetsService, spreadSheetId, gid, byId);

            Console.WriteLine("Row deleted successfully.");
            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Không thể xóa row. {ex.Message}");
        }
    }
    #endregion

    #region 3. Jobs Server
    public async Task<List<Job>> GetsJobs()
    {
        try
        {
            var data = new List<Job>();
            var range = $"{sheetJobs}!A2:J";
            var values = await GGSExtensions.APIGetValues(sheetsService, spreadSheetId, range);
            if (values == null || values.Count == 0)
            {
                throw new Exception("Không có dữ liệu sheet.");
            }

            foreach (var item in values)
            {
                var companyId = item[1].ToString() ?? string.Empty;

                var datasCompany = await GetsCompanies();
                var getCompany = datasCompany.FirstOrDefault(a => a.company_id == companyId);

                data.Add(new Job
                {
                    job_id = item[0].ToString() ?? string.Empty,
                    company_id = companyId,
                    title = item[2].ToString() ?? string.Empty,
                    description = item[3].ToString() ?? string.Empty,
                    location = item[4].ToString() ?? string.Empty,
                    salary_range = item[5].ToString() ?? string.Empty,
                    job_type = item[6].ToString() ?? string.Empty,
                    status = item[7].ToString() ?? string.Empty,
                    img = item[8].ToString() ?? string.Empty,
                    createdAt = item[9].ToString() ?? string.Empty,
                    Company = getCompany
                });
            }
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }

    public async Task<Job> GetJob(string id)
    {
        try
        {
            var datas = await GetsJobs();

            var result = datas.FirstOrDefault(a => a.job_id == id);

            return result ?? throw new Exception($"ID ({id}) không tồn tại!");
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi khi tìm kiếm: {ex.Message}");
        }
    }
    
    public async Task<bool> CreateJob(Job model)
    {
        try
        {
            // Lấy danh sách hiện có
            var datas = await GetsJobs();

            // Kiểm tra tồn tại trùng số tài khoản và mã ngân hàng
            if (datas.Any(a => a.job_id == model.job_id))
            {
                throw new Exception($"ID này đã tồn tại.");
            }
            
            var companyId = await GetCompany(model.company_id);
            if (companyId == null)
            {
                throw new Exception($"Company ID ({model.company_id}) không tồn tại");
            }

            //Create extensions
            await GGSExtensions.APICreateValues(sheetsService, spreadSheetId, $"{sheetJobs}!A2:J", new ValueRange 
            {
                Values = new List<IList<object>> 
                { 
                    new List<object> { 
                        Guid.NewGuid(), 
                        companyId.company_id,
                        model.title,
                        model.description,
                        model.location,
                        model.salary_range,
                        model.job_type,
                        model.status,
                        model.img, 
                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    } 
                }
            });

            Console.WriteLine("Create success!");
            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Không thể tạo mới. {ex.Message}");
        }
    }
    
    public async Task<Job> UpdateJob(Job model)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var datas = await GetsJobs();
            
            // Tìm vị trí đối tượng cập nhật
            var byId = datas.FindIndex(a => a.job_id == model.job_id);
            if (byId == -1) // Nếu không tìm thấy information_id trả về -1
            {
                throw new Exception($"Không tồn tại không thể cập nhật");
            }

            await GGSExtensions.APIUpdateValues(sheetsService, spreadSheetId, $"{sheetJobs}!C{byId+2}:I", new ValueRange
            {
                Values = new List<IList<object>> 
                { 
                    new List<object> { 
                        model.title, 
                        model.description, 
                        model.location,
                        model.salary_range,
                        model.job_type,
                        model.status,
                        model.img
                    }
                }
            });

            Console.WriteLine("Update successfully.");
            return model;
        }
        catch (Exception ex)
        {
            throw new Exception($"Không thể cập nhật. {ex.Message}");
        }
    }
    
    public async Task<bool> DeleteJob(string id)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var datas = await GetsJobs();
            
            // Tìm vị trí đối tượng xóa
            var byId = datas.FindIndex(a => a.job_id == id);
            if (byId == -1)
            {
                throw new Exception($"ID ({id}) này không tồn tại không thể xóa");
            }
            await GGSExtensions.APIRemoveValues(sheetsService, spreadSheetId, $"{sheetJobs}!A{byId+2}:J{byId+2}");

            Console.WriteLine("Clear successfully.");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Không thể xóa. {ex.Message}");
        }
    }
    
    public async Task<bool> DeleteRowJob(string id)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var datas = await GetsJobs();
            
            // Tìm vị trí đối tượng xóa
            var byId = datas.FindIndex(a => a.job_id == id);
            if (byId == -1)
            {
                throw new Exception($"ID ({id}) này không tồn tại không thể xóa");
            }
            
            // GOOGLE SHEET ID
            var gid = 1234531942;
            await GGSExtensions.APIRemoveRows(sheetsService, spreadSheetId, gid, byId);

            Console.WriteLine("Row deleted successfully.");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Không thể xóa row. {ex.Message}");
        }
    }
    #endregion

    #region 4. Recruitments Server
    public async Task<List<Recruitment>> GetsRecruitments()
    {
        try
        {
            var data = new List<Recruitment>();
            var range = $"{sheetRecruitments}!A2:G";
            var values = await GGSExtensions.APIGetValues(sheetsService, spreadSheetId, range);
            if (values == null || values.Count == 0)
            {
                throw new Exception("Không có dữ liệu sheet.");
            }

            foreach (var item in values)
            {
                var jobId = item[1].ToString() ?? string.Empty;
                var dts = await GetsJobs();
                var getJob = dts.FirstOrDefault(a => a.job_id == jobId);

                var information_id = item[2].ToString() ?? string.Empty;
                var dtss = await GetsInformationApplies();
                var getInformationApply = dtss.FirstOrDefault(a => a.information_id == information_id);

                data.Add(new Recruitment
                {
                    recruitment_id = item[0].ToString() ?? string.Empty,
                    job_id = jobId,
                    information_id = information_id,
                    cover_letter = item[3].ToString() ?? string.Empty,
                    resume_link = item[4].ToString() ?? string.Empty,
                    status = item[5].ToString() ?? string.Empty,
                    applied_at = item[6].ToString() ?? string.Empty,
                    Job = getJob,
                    InformationApply = getInformationApply
                });
            }

            return data;
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi dữ liệu. {ex.Message}");
        }
    }
    
    public async Task<Recruitment> GetRecruitment(string id)
    {
        try
        {
            var datas = await GetsRecruitments();

            var result = datas.FirstOrDefault(a => a.recruitment_id == id);

            return result ?? throw new Exception($"ID ({id}) không tồn tại!");
        }
        catch (Exception ex)
        {
            throw new Exception($"Lỗi khi tìm kiếm: {ex.Message}");
        }
    }
    
    public async Task<bool> CreateRecruitment(Recruitment model)
    {
        try
        {
            // Lấy danh sách hiện có
            var datas = await GetsRecruitments();

            // Kiểm tra tồn tại trùng số tài khoản và mã ngân hàng
            if (datas.Any(a => a.recruitment_id == model.recruitment_id))
            {
                throw new Exception($"ID này đã tồn tại.");
            }
            
            var _job = await GetCompany(model.job_id);
            if (_job == null)
            {
                throw new Exception($"Job ID ({model.job_id}) không tồn tại");
            }

            var informationId = await GetCompany(model.information_id);
            if (informationId == null)
            {
                throw new Exception($"Information ID ({model.information_id}) không tồn tại");
            }

            //Create extensions
            await GGSExtensions.APICreateValues(sheetsService, spreadSheetId, $"{sheetRecruitments}!A2:G", new ValueRange 
            {
                Values = new List<IList<object>> 
                { 
                    new List<object> { 
                        Guid.NewGuid(), 
                        model.job_id,
                        model.information_id,
                        model.cover_letter,
                        model.resume_link,
                        model.status,
                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    } 
                }
            });

            Console.WriteLine("Create success!");
            return true;
        }
        catch (Exception ex)
        {

            throw new Exception($"Không thể tạo mới. {ex.Message}");
        }
    }
    
    public async Task<Recruitment> UpdateRecruitment(Recruitment model)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var datas = await GetsRecruitments();
            
            // Tìm vị trí đối tượng cập nhật
            var byId = datas.FindIndex(a => a.recruitment_id == model.recruitment_id);
            if (byId == -1) // Nếu không tìm thấy information_id trả về -1
            {
                throw new Exception($"Không tồn tại không thể cập nhật");
            }

            await GGSExtensions.APIUpdateValues(sheetsService, spreadSheetId, $"{sheetRecruitments}!D{byId+2}:F", new ValueRange
            {
                Values = new List<IList<object>> 
                { 
                    new List<object> { 
                        model.cover_letter,
                        model.resume_link,
                        model.status,
                    }
                }
            });

            Console.WriteLine("Update successfully.");
            return model;
        }
        catch (Exception ex)
        {
            throw new Exception($"Không thể cập nhật. {ex.Message}");
        }
    }
    
    public async Task<bool> DeleteRecruitment(string id)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var datas = await GetsRecruitments();
            
            // Tìm vị trí đối tượng xóa
            var byId = datas.FindIndex(a => a.recruitment_id == id);
            if (byId == -1)
            {
                throw new Exception($"ID ({id}) này không tồn tại không thể xóa");
            }
            await GGSExtensions.APIRemoveValues(sheetsService, spreadSheetId, $"{sheetRecruitments}!A{byId+2}:G{byId+2}");

            Console.WriteLine("Clear successfully.");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Không thể xóa. {ex.Message}");
        }
    }
    
    public async Task<bool> DeleteRowRecruitment(string id)
    {
        try
        {
            // Lấy toàn bộ danh sách
            var datas = await GetsRecruitments();
            
            // Tìm vị trí đối tượng xóa
            var byId = datas.FindIndex(a => a.recruitment_id == id);
            if (byId == -1)
            {
                throw new Exception($"ID ({id}) này không tồn tại không thể xóa");
            }
            
            // GOOGLE SHEET ID
            var gid = 137168348;
            await GGSExtensions.APIRemoveRows(sheetsService, spreadSheetId, gid, byId);

            Console.WriteLine("Row deleted successfully.");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Không thể xóa row. {ex.Message}");
        }
    }
    
    #endregion
}