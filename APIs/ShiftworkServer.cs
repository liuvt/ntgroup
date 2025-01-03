using ntgroup.APIs.Contracts;
using ntgroup.Data.Models;
using Microsoft.AspNetCore.Identity;
using ntgroup.Data.Entities;
using ntgroup.Data;
namespace ntgroup.APIs;

public class ShiftworkServer : IShiftworkServer
{

    //Call dbContext to database
    private readonly ntgroupDbContext context;

    //Constructor
    public ShiftworkServer(ntgroupDbContext _context)
    {
        this.context = _context;
    }

    public async Task<Shiftwork> Create(ShiftworkCreateDTO models)
    {
        try
        {   
            //Create new Shiftwork
            var new_SW = new Shiftwork
            {
                sw_Id = Guid.NewGuid().ToString(),
                sw_TimeStart = models.sw_TimeStart, //Thời gian bắt đầu ca
                sw_TimeEnd = models.sw_TimeEnd, //Thời gian kết thúc
                sw_Status = models.sw_Status, //Trạng thái lên ca
                sw_CreatedAt = DateTime.Now, //Ngày tạo
                sw_DeletedAt = null, //Default null
                car_Id = models.car_Id, // ID xe
                driver_Id = models.driver_Id, // ID tài xế
            };
            await context.AddAsync(new_SW);
            context.SaveChanges();
            return new_SW;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Create method}: " + ex.Message);
        }
    }

    public async Task<string> CreateShiftworks(List<ShiftworkCreateDTO> models)
    {
        try
        {   
            var new_SWs = new List<Shiftwork>();
            foreach(var model in models)
            {
                new_SWs.Add(new Shiftwork
                {
                    sw_Id = Guid.NewGuid().ToString(),
                    sw_TimeStart = model.sw_TimeStart, //Thời gian bắt đầu ca
                    sw_TimeEnd = model.sw_TimeEnd, //Thời gian kết thúc
                    sw_Status = model.sw_Status, //Trạng thái lên ca
                    sw_CreatedAt = DateTime.Now, //Ngày tạo
                    sw_DeletedAt = null, //Default null
                    car_Id = model.car_Id, // ID xe
                    driver_Id = model.driver_Id, // ID tài xế
                });
            }
            await context.AddRangeAsync(new_SWs);
            context.SaveChanges();
            var result = "Đã tạo thành công " + new_SWs.Count + " ca làm việc. Lỗi " + (models.Count - new_SWs.Count) + " ca";
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Create method}: " + ex.Message);
        }
    }

    public async Task<bool> Deleted(string sw_Id)
    {
        try
        {   
            var sw = context.Shiftworks.FirstOrDefault(x => x.sw_Id == sw_Id);
            if(sw == null)
            {
                throw new Exception("Shiftwork not found");
            }
            context.Shiftworks.Remove(sw);
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Create method}: " + ex.Message);
        }
    }

    public async Task<bool> Update(ShiftworkDTO models)
    {
        try
        {   
            var sw = context.Shiftworks.FirstOrDefault(x => x.sw_Id == models.sw_Id);
            if(sw == null)
            {
                throw new Exception("Shiftwork not found");
            }

            sw.sw_TimeStart = models.sw_TimeStart;
            sw.sw_Status = models.sw_Status;
            sw.sw_CreatedAt = models.sw_CreatedAt;
            sw.sw_DeletedAt = models.sw_DeletedAt;
            sw.car_Id = models.car_Id;
            sw.sw_TimeEnd = models.sw_TimeEnd;
            sw.driver_Id = models.driver_Id;

            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Create method}: " + ex.Message);
        }
    }
}