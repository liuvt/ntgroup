using ntgroup.APIs.Contracts;
using ntgroup.Data.Models;
using Microsoft.AspNetCore.Identity;
using ntgroup.Data.Entities;
using ntgroup.Data;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using ntgroup.ModelToEntities;

namespace ntgroup.APIs;

public class DriverServer : IDriverServer
{

    //Call dbContext to database
    private readonly ntgroupDbContext context;

    //Constructor
    public DriverServer(ntgroupDbContext _context)
    {
        this.context = _context;
    }

    public async Task<DriverDTO> Create(DriverCreateDTO model)
    {
        try
        {   
            //Create new driver
            var driver = new Driver
            {   
                driver_Id = Guid.NewGuid().ToString(),
                driver_Name = model.driver_Name,
                driver_EmployeeID = model.driver_EmployeeID,
                driver_Phone = model.driver_Phone,
                driver_Address = model.driver_Address,
                driver_CreatedAt = DateTimeOffset.Now,
            };
            
            //Add driver to database
            await context.Drivers.AddAsync(driver);
            await context.SaveChangesAsync();
                                    
            return new DriverDTO
            {
                driver_Id = driver.driver_Id,
                driver_Name = driver.driver_Name,
                driver_EmployeeID = driver.driver_EmployeeID,
                driver_Phone = driver.driver_Phone,
                driver_Address = driver.driver_Address,
                driver_CreatedAt = driver.driver_CreatedAt
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Create method}: " + ex.Message);
        }
    }

    public async Task<string> CreateDrivers(List<DriverCreateDTO> models)
    {
        try
        {
            // Tạo danh sách mới
            var newDriver = new List<Driver>();
            // Duyệt qua danh sách các model tham số
            foreach(var model in models)
            {
                //Create new driver
                newDriver.Add(new Driver
                {   
                    driver_Id = Guid.NewGuid().ToString(),
                    driver_Name = model.driver_Name,
                    driver_EmployeeID = model.driver_EmployeeID,
                    driver_Phone = model.driver_Phone,
                    driver_Address = model.driver_Address,
                    driver_CreatedAt = DateTimeOffset.Now,
                }); 
            }
            // Thêm danh sách mới vào DbSet
            await context.AddRangeAsync(newDriver);
            // Lưu thay đổi vào cơ sở dữ liệu
            await context.SaveChangesAsync();
            // Trả về thông báo
            var result = "Đã tạo thành công " + newDriver.Count +"/"+models.Count+ " bản ghi!";
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Create method}: " + ex.Message);
        }
    }

    public async Task<bool> Delete(string driver_Id)
    {
        try
        {
            var driver = await this.context.Drivers.FirstOrDefaultAsync(x => x.driver_Id == driver_Id);

            // Nếu không tìm thấy xe thì thông báo
            if(driver == null)
                throw new Exception("Không tìm thấy xe!");

            context.Remove(driver);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Delete method}: " + ex.Message);
        }
    }

    public async Task<string> DeleteDrivers(List<string> listDrivers)
    {
        try
        {
            // Danh sách các Car cần xóa
            List<string> driverIds = listDrivers;

            // Tìm tất cả các Car có DriverId nằm trong danh sách driverIdsToRemove
            var drivesToRemove = context.Drivers.Where(d => driverIds.Contains(d.driver_Id)).ToList();

            if (drivesToRemove.Any())
            {
                // Xóa tất cả các driver tìm được từ DbSet
                context.Drivers.RemoveRange(drivesToRemove);
                // Lưu thay đổi vào cơ sở dữ liệu
                await context.SaveChangesAsync();
            }
            var result = "Đã xóa thành công " + drivesToRemove.Count +"/"+listDrivers.Count+ " xe";
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Delete method}: " + ex.Message);
        }
    }

    public async Task<IEnumerable<DriverDTO>> Gets()
    {
        try
        {
            // Lấy ds dữ liệu từ bảng Car
            var driver = await context.Drivers
                                    .Include(x => x.car)
                                    .Include(x => x.wallet)
                                    .OrderByDescending(x => x.driver_EmployeeID)
                                    .Select(x => x).ToListAsync();

            // Nếu không có xe nào thì thông báo
            if(driver.Count == 0)
                throw new Exception("Dữ liệu rỗng!");

            var cars = driver.Select(x => x.car).ToList();
            var wallet = driver.Select(x => x.wallet).ToList();

            // Chuyển đổi dữ liệu từ Car sang CarDTO
            return driver.DriverToDo(cars, wallet);
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {GetCar method}: " + ex.Message);
        }
    }

    public Task<DriverDTO> Update(CarUpdateDTO model)
    {
        throw new NotImplementedException();
    }
    
    public async Task<DriverDTO> Get(string driver_Id)
    {
        throw new NotImplementedException();
    }
}