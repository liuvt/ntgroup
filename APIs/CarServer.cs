using ntgroup.APIs.Contracts;
using ntgroup.Data.Models;
using Microsoft.AspNetCore.Identity;
using ntgroup.Data.Entities;
using ntgroup.Data;
using Microsoft.EntityFrameworkCore;
using ntgroup.ModelToEntities;

namespace ntgroup.APIs;

public class CarServer : ICarServer
{

    //Call dbContext to database
    private readonly ntgroupDbContext context;

    //Constructor
    public CarServer(ntgroupDbContext _context)
    {
        this.context = _context;
    }
    public async Task<CarDTO> Create(CarCreateDTO model)
    {
        try
        {   
            // Không được phép tạo xe trùng biển số
            if(await Exist(model.car_NumberPlate))
                throw new Exception("Biển số xe đã tồn tại!");

            // Tạo một class model mới để lưu vào Database
            var new_Car = new Car
            {
                car_Id = Guid.NewGuid().ToString(), // Tự động tạo
                car_NumberId = model.car_NumberId, // Không được bỏ trống
                car_NumberPlate = model.car_NumberPlate, // Không được bỏ trống
                car_CreatedAt = DateTimeOffset.Now, // Tư động
                driver_Id = model.drive_Id // Không gán người lái xe
            };
            await context.AddAsync(new_Car);
            await context.SaveChangesAsync();


            var car = await this.Get(new_Car.car_Id);
            return car;    
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Create method}: " + ex.Message);
        }
    }

    public async Task<string> CreateCars(List<CarCreateDTO> models)
    {
        try
        {
            // Tạo danh sách mới
            var newCar = new List<Car>();
            // Duyệt qua danh sách các model tham số
            foreach(var model in models)
            {
                // Kiểm tra biển số xe đã tồn tại chưa nếu có thì bỏ qua
                if(await Exist(model.car_NumberPlate))
                {
                    continue;
                }

                // Thêm vào danh sách mới
                newCar.Add(new Car
                {
                    car_Id = Guid.NewGuid().ToString(), // ID tự tạo
                    car_NumberId = model.car_NumberId, // Không được bỏ trống
                    car_NumberPlate = model.car_NumberPlate, // Không được bỏ trống
                    car_CreatedAt = DateTimeOffset.Now, // Tư động tạo thời gian
                    driver_Id = model.drive_Id // Không gán người lái xe
                });
            }
            // Thêm danh sách mới vào DbSet
            await context.AddRangeAsync(newCar);
            // Lưu thay đổi vào cơ sở dữ liệu
            await context.SaveChangesAsync();
            // Trả về thông báo
            var result = "Đã tạo thành công " + newCar.Count +"/"+models.Count+ " xe";
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Create method}: " + ex.Message);
        }
    }

    public async Task<bool> Delete(string car_Id)
    {
        try
        {
            var car = await this.context.Cars.FirstOrDefaultAsync(x => x.car_Id == car_Id);

            // Nếu không tìm thấy xe thì thông báo
            if(car == null)
                throw new Exception("Không tìm thấy xe!");

            context.Remove(car);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Delete method}: " + ex.Message);
        }
    }

    public async Task<string> DeleteCars(List<string> listCars)
    {
        try
        {
            // Danh sách các Car cần xóa
            List<string> CarIds = listCars;

            // Tìm tất cả các Car có DriverId nằm trong danh sách driverIdsToRemove
            var carsToRemove = context.Cars.Where(d => CarIds.Contains(d.car_Id)).ToList();

            if (carsToRemove.Any())
            {
                // Xóa tất cả các driver tìm được từ DbSet
                context.Cars.RemoveRange(carsToRemove);
                // Lưu thay đổi vào cơ sở dữ liệu
                await context.SaveChangesAsync();
            }
            var result = "Đã xóa thành công " + carsToRemove.Count +"/"+listCars.Count+ " xe";
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Delete method}: " + ex.Message);
        }
    }

    /// <summary>
    /// Thay đổi biển số xe
    /// Thay đổi số hiệu
    /// Thay đổi người lái xe
    /// </summary>
    /// <param name="car_Id"></param>
    /// <param name="model"></param>
    /// <returns>CarDTO</returns>
    public async Task<CarDTO> Update(string car_Id, CarUpdateDTO model)
    {
        try
        {
            var car = await context.Cars
                    .Include(e => e.driver)
                    .FirstOrDefaultAsync(x => x.car_Id == car_Id);
            
            // Nếu không tìm thấy xe thì thông báo
            if(car == null)
                throw new Exception("Không tìm thấy xe!");

            // Cập nhật
            car.car_NumberId = model.car_NumberId;
            car.car_NumberPlate = model.car_NumberPlate;
            car.driver_Id = model.driver_Id;

            // Lưu thay đổi
            await context.SaveChangesAsync();
            return new CarDTO
            {
                car_Id = car.car_Id,
                car_NumberId = car.car_NumberId,
                car_NumberPlate = car.car_NumberPlate,
                car_CreatedAt = car.car_CreatedAt,
                car_DeletedAt = car.car_DeletedAt,
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {Delete method}: " + ex.Message);
        }
    }

    public async Task<IEnumerable<CarDTO>> Gets()
    {
        try
        {
            // Lấy ds dữ liệu từ bảng Car
            var car = await context.Cars.Include(x => x.driver) // Lấy dữ liệu từ bảng driver
                                    .OrderByDescending(x => x.car_NumberId) // Sắp xếp theo car_NumberId
                                    .Select(x => x).ToListAsync();

            // Nếu không có xe nào thì thông báo
            if(car.Count == 0)
                throw new Exception("Không có xe nào!");

            // Chuyển đổi dữ liệu từ Car sang CarDTO
            return car.CarToDo(car.Select(e => e.driver).ToList());
        }
        catch (Exception ex)
        {
            throw new Exception("Error system {GetCar method}: " + ex.Message);
        }
    }

    public async Task<CarDTO> Get(string car_Id)
    {
        var car = await context.Cars
                        .Include(x => x.driver)
                        .FirstOrDefaultAsync(x => x.car_Id == car_Id); 

        // Nếu không có xe nào thì thông báo
        if(car == null)
                throw new Exception("Không có xe nào!");

        // Chuyển đổi dữ liệu từ Car sang CarDTO
        return car.CarToDo();
    } 

    //Kiểm tra tồn tại biễn số xe
    private async Task<bool> Exist(string car_NumberPlate) 
        => await context.Cars.AnyAsync(x => x.car_NumberPlate == car_NumberPlate);
}