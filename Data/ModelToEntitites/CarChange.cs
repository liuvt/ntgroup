
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.ModelToEntities;

public static class CarChange
{
    // Thêm "this" để không phải truyền tham số
    public static CarDTO CarToDo(this Car _car)
    {
        return new CarDTO
        {
            car_Id = _car.car_Id,
            car_NumberId = _car.car_NumberId,
            car_NumberPlate = _car.car_NumberPlate,
            car_CreatedAt = _car.car_CreatedAt,
            car_DeletedAt = _car.car_DeletedAt,
            driver = new DriverDTO
            {
                driver_Id = _car.driver.driver_Id,
                driver_Name = _car.driver.driver_Name,
                driver_EmployeeID = _car.driver.driver_EmployeeID,
                driver_Phone = _car.driver.driver_Phone,
                driver_Address = _car.driver.driver_Address,
                driver_CreatedAt = _car.driver.driver_CreatedAt,
                driver_DeleteddAt = _car.driver.driver_DeleteddAt
            }
        };
    }

    // Thêm "this" để không phải truyền tham số
    // Truyền một Driver từ Database
    public static IEnumerable<CarDTO> CarToDo(this IEnumerable<Car> _car, IEnumerable<Driver> _driver)
    {
        return(from c in _car
                    join d in _driver on c.driver_Id equals d.driver_Id
                    select new CarDTO
                    {
                        car_Id = c.car_Id,
                        car_NumberId = c.car_NumberId,
                        car_NumberPlate = c.car_NumberPlate,
                        car_CreatedAt = c.car_CreatedAt,
                        car_DeletedAt = c.car_DeletedAt,
                        driver = new DriverDTO
                        {
                            driver_Id = d.driver_Id,
                            driver_Name = d.driver_Name,
                            driver_EmployeeID = d.driver_EmployeeID,
                            driver_Phone = d.driver_Phone,
                            driver_Address = d.driver_Address,
                            driver_CreatedAt = d.driver_CreatedAt,
                            driver_DeleteddAt = d.driver_DeleteddAt
                        }
                    });
    }
}
