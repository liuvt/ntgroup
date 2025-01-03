using System.ComponentModel.DataAnnotations;
using ntgroup.Data.Models;
namespace ntgroup.Data.Entities;

public class CarDTO
{
    public string car_Id { get; set; } = string.Empty; //Guid ID
    public string car_NumberId { get; set; } = string.Empty; // Số hiệu taxi => có thể thay đổi xe
    public string car_NumberPlate { get; set; } = string.Empty; //Biển số
    public DateTimeOffset car_CreatedAt {get ; set;}
    public DateTimeOffset car_DeletedAt {get ; set;}
    public DriverDTO? driver { get; set; } // Thông tin người lái xe
}

public class CarCreateDTO
{
    [Required]
    public string car_NumberId { get; set; } = string.Empty; // Số hiệu taxi => có thể thay đổi xe
    [Required]
    public string car_NumberPlate { get; set; } = string.Empty; // Biển số
    [Required]
    public string drive_Id { get; set; } = string.Empty; // Đăng ký người lái
}

public class CarUpdateDTO
{
    public string car_NumberId { get; set; } = string.Empty; // Số hiệu taxi => có thể thay đổi xe
    public string car_NumberPlate { get; set; } = string.Empty; //Biển số
    public string driver_Id { get; set; } = string.Empty; // Thay đổi người lái xe
}
