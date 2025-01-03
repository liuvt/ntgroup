using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;

public class Wallet
{
    [Key]
    public string wallet_Id { get; set; } = string.Empty; //Mã ví
    public string wallet_Description { get; set; } = string.Empty; //Mô tả
    public decimal? wallet_Balance { get; set; } //Số dư
    public DateTimeOffset? wallet_CreatedAt { get; set; } //Ngày tạo
    public DateTimeOffset? wallet_DeletedAt { get; set; } //Ngày xóa

    public string driver_Id { get; set; } = string.Empty;
    [ForeignKey("driver_Id")]
    public Driver? driver { get; set; }
    // 1-n giao dịch
    public List<WalletTransaction>? wallettransactions { get; set; }
}
