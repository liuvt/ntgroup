
using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.ModelToEntities;

public static class DriverChange
{
    // Thêm "this" để không phải truyền tham số
    public static DriverDTO DriverToDo(this Driver driver)
    {
        return new DriverDTO
        {
            driver_Id = driver.driver_Id,
            driver_Name = driver.driver_Name,
            driver_EmployeeID =  driver.driver_EmployeeID,
            driver_Phone = driver.driver_Phone,
            driver_Address  = driver.driver_Address,
            driver_CreatedAt = driver.driver_CreatedAt,
            driver_DeleteddAt = driver.driver_DeleteddAt,
            car = driver.car.CarToDo(),
            wallet = driver.wallet.WalletToDo(),
        };
    }

    // Thêm "this" để không phải truyền tham số
    public static IEnumerable<DriverDTO> DriverToDo(this IEnumerable<Driver> _divers, IEnumerable<Car> _cars, IEnumerable<Wallet> _wallets)
    {
        return(from d in _divers 
                    join c in _cars on d.car?.car_Id equals c.car_Id
                    join w in _wallets on d.wallet?.wallet_Id equals w.wallet_Id
                    select new DriverDTO
                    {
                        driver_Id = d.driver_Id,
                        driver_Name = d.driver_Name,
                        driver_EmployeeID = d.driver_EmployeeID,
                        driver_Phone = d.driver_Phone,
                        driver_Address = d.driver_Address,
                        driver_CreatedAt = d.driver_CreatedAt,
                        driver_DeleteddAt = d.driver_DeleteddAt,
                        car = new CarDTO
                        {
                            car_Id = c.car_Id,
                            car_NumberId = c.car_NumberId,
                            car_NumberPlate = c.car_NumberPlate,
                            car_CreatedAt = c.car_CreatedAt,
                            car_DeletedAt = c.car_DeletedAt,
                        },
                        wallet = new WalletDTO
                        {
                            wallet_Id = w.wallet_Id,
                            wallet_Balance = w.wallet_Balance,
                            wallet_CreatedAt = w.wallet_CreatedAt,
                            wallet_DeletedAt = w.wallet_DeletedAt
                        }
                    });
    }
}