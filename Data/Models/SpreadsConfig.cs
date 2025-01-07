
using System.ComponentModel.DataAnnotations;

namespace ntgroup.Data.Models;
public class SpreadsConfig
{

}

public class Area
{
    public string area_Id { get; set; } = string.Empty;
    public string area_Name { get; set; } = string.Empty;
    public string area_SpreadId { get; set; } = string.Empty;
    public string bank_Id { get; set; } = string.Empty;
}

public class Banking
{
    public string bank_Id { get; set; } = string.Empty;
    public string bank_Name { get; set; } = string.Empty;
    public string bank_Number { get; set; } = string.Empty;
    public string bank_Type { get; set; } = string.Empty;
    public string bank_AccountName { get; set; } = string.Empty;
    public string bank_Url { get; set; } = string.Empty;

    public List<Area>? listAreas { get; set; }
}