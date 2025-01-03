using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ntgroup.Data.Models;
public class WalletTransaction
{
    [Key]
    public string transaction_Id { get; set; } = string.Empty; // Primary Key
    public decimal? transaction_Amount { get; set; } // Số tiền giao dịch
    public string transaction_Description { get; set; } = string.Empty; // Mô tả nội dung giao dịch
    public DateTimeOffset? transaction_CreatedAt { get; set; } // Ngày giao dịch
    public DateTimeOffset? transaction_DeletedAt { get; set; } // Ngày giao dịch

    // Loại giao dịch Nạp / rút / thanh toán transactionType_Id
    public string transactiontype_Id { get; set; } = string.Empty; 
    [ForeignKey("transactiontype_Id")]
    public WalletTransactionType? wallettransactiontype {get; set;}

    // Giao dịch này thuộc ví wallet_Id
    public string wallet_Id { get; set; } = string.Empty;
    [ForeignKey("wallet_Id")]
    public Wallet? wallet { get; set; } // Quan hệ nhiều-một với Wallet
}