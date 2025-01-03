using ntgroup.APIs.Contracts;
using ntgroup.Data.Models;
using Microsoft.AspNetCore.Identity;
using ntgroup.Data.Entities;
using ntgroup.Data;

namespace ntgroup.APIs;

public class CustomerServer : ICustomerServer
{

    private readonly ntgroupDbContext context;

    //Constructor
    public CustomerServer(ntgroupDbContext _context)
    {
        this.context = _context;
    }

    /* Manager Gets
        Xem toàn bộ thông tin ds user
    public async Task<List<AppUser>> Gets()
        => await userManager.Users.OrderByDescending(p => p.PublishedAt).ToListAsync();
    */

    /* Create
        Tạo mới dữ liệu
    */
    public async Task<bool> Create(List<CustomerDTO> listCustomers)
    {
        try
        {
            //Kiểm tra mã Khách hàng tồn tại

            await context.AddRangeAsync(listCustomers);
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Create method}: " + ex.Message);
        }
    }
    /*
    /* Manager Delete
        Xóa user theo ID
    public async Task<IdentityResult> Delete(string userId)
    {
        try
        {
            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("Người dùng không tồn tại!");

            var result = await this.userManager.DeleteAsync(user);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Delete method}: " + ex.Message);
        }
    }
    */

    /* Manager DeleteSelect
        Xóa user theo ds ID
    public async Task<bool> DeleteSelect(IEnumerable<string> userIds)
    {
        try
        {
            // Tìm kiếm ds users
            var users = this.userManager.Users
                                        .Where(u => userIds.Contains(u.Id)).ToList();

            if (users.Count() <= 0 ) return false;

            //  Note: do không có hàm RemoveRange nên sử dụng vòng lập để xóa từng user
            foreach(var item in users)
            {
                var result = await this.userManager.DeleteAsync(item);
            }
            //Console.WriteLine($"{context.ChangeTracker.DebugView.LongView}");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {DeleteSelect method}: " + ex.Message);
        }
    }
    */
}