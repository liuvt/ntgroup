
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.ModelToEntities;

public static class WalletChange
{
    // Thêm "this" để không phải truyền tham số
    public static WalletDTO WalletToDo(this Wallet wallet)
    {
        return new WalletDTO
        {

        };
    }
}