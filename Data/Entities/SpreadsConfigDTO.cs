
using System.ComponentModel.DataAnnotations;

namespace ntgroup.Data.Entities;
public class SpreadsConfigDTO
{

}

public class AreaCreateDTO
{
    public string area_Id { get; set; } = string.Empty;
    public string area_Name { get; set; } = string.Empty;
    public string area_SpreadId { get; set; } = string.Empty;
    public string bank_Id { get; set; } = string.Empty;
}

public class BankingCreateDTO
{
    public string bank_Id { get; set; } = string.Empty;
    public string bank_Name { get; set; } = string.Empty;
    public string bank_Number { get; set; } = string.Empty;
    public string bank_AccountName { get; set; } = string.Empty;
    public string bank_Url { get; set; } = string.Empty;
    public string bank_Static { get; set; } = string.Empty; // Kiểm tra trạng thái tài khoản ngân hàng
}

public class BankingUpdateDTO
{
    public string bank_Name { get; set; } = string.Empty;
    public string bank_Number { get; set; } = string.Empty;
    public string bank_Type { get; set; } = string.Empty;
    public string bank_AccountName { get; set; } = string.Empty;
    public string bank_Url { get; set; } = string.Empty;
    public string bank_Static { get; set; } = string.Empty; // Kiểm tra trạng thái tài khoản ngân hàng
}