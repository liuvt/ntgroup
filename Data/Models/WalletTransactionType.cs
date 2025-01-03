using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;
public class WalletTransactionType
{
    [Key]
    public string transactiontype_Id { get; set; } = string.Empty; // Primary Key
    public string transactiontype_Name { get; set; } = string.Empty; // Loại giao dịch Nạp / rút / thanh toán
    public string transactiontype_Description { get; set; } = string.Empty; // Mô tả nội dung giao dịch
    public List<WalletTransaction>? wallettransactions {get; set;}
}