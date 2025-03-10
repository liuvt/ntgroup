
using System.ComponentModel.DataAnnotations;
using ntgroup.Data.Models;

namespace ntgroup.Data.Entities;
public class SpreadsRecruitmentDTO
{
}

public class InformationApplyDTO
{
    [Key]
    public string information_id { get; set; } = string.Empty;
    [Required, MaxLength(100)]
    public string information_FullName { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string information_Email { get; set; } = string.Empty;
    [MaxLength(20)]
    public string information_Phone { get; set; } = string.Empty;
    public string createdAt { get; set; } = string.Empty;
    public List<RecruitmentDTO>? Applications { get; set; }
}

public class CompanyDTO
{
    [Key]
    public string company_id { get; set; } = string.Empty;
    [Required, MaxLength(255)]
    public string company_name { get; set; } = string.Empty;
    public string website { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string location { get; set; } = string.Empty;
    public string created_at { get; set; } = string.Empty;

}

public class JobCreateDTO
{
    [Key]
    public string job_id { get; set; } = string.Empty;
    [Required]
    public string company_id { get; set; } = string.Empty;
    [Required, MaxLength(255)]
    public string title { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string location { get; set; } = string.Empty;
    public string salary_range { get; set; } = string.Empty;
    public string job_type { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
    public string createdAt { get; set; } = string.Empty;
    public string img { get; set; } = string.Empty;
    public CompanyDTO? Company { get; set; }
}

public class RecruitmentDTO
{
    [Key]
    public string recruitment_id { get; set; } = string.Empty;
    [Required]
    public string job_id { get; set; } = string.Empty;
    [Required]
    public string information_id { get; set; } = string.Empty;
    public string cover_letter { get; set; } = string.Empty;
    public string resume_link { get; set; } = string.Empty;
    public string status { get; set; } = "Pending";
    public string applied_at { get; set; } = string.Empty;
    public Job? Job { get; set; }
    public InformationApplyDTO? InformationApply { get; set; }
}