using ntgroup.Data.Entities;
using ntgroup.Data.Models;

namespace ntgroup.APIs.Contracts;

public interface ICarServer
{
    Task<CarDTO> Create(CarCreateDTO model); // Tạo 1 xe
    Task<string> CreateCars(List<CarCreateDTO> models); // Tạo nhiều xe
    Task<bool> Delete(string car_Id); // Xóa 1 xe
    Task<string> DeleteCars(List<string> listCars); // Xóa nhiều xe
    Task<CarDTO> Update(string car_Id, CarUpdateDTO model); // Cập nhật thông tin xe
    Task<IEnumerable<CarDTO>> Gets(); // Lấy danh sách xe
    Task<CarDTO> Get(string car_Id); // Lấy danh sách xe
}