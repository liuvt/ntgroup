
using System.ComponentModel.DataAnnotations;
using ntgroup.Data.Models;
namespace ntgroup.Data.Entities;
public class WalletDTO
{
    public string wallet_Id { get; set; } = string.Empty; //Mã ví
    public string wallet_Description { get; set; } = string.Empty; //Mô tả
    public decimal? wallet_Balance { get; set; } //Số dư
    public DateTimeOffset? wallet_CreatedAt { get; set; } //Ngày tạo
    public DateTimeOffset? wallet_DeletedAt { get; set; } //Ngày xóa
    public string driver_Id { get; set; } = string.Empty;
    // 1-n giao dịch
    public List<WalletTransaction>? wallettransactions { get; set; }
}

public class WalletCreateDTO
{
    public string wallet_Description { get; set; } = string.Empty; //Mô tả
    public decimal? wallet_Balance { get; set; } //Số dư
}