using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using DocumentFormat.OpenXml.Presentation;

namespace ntgroup.Data.Models;

public class SpreadsAuth
{
}

public partial class Driver
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty; //Tài khoản đăng nhập
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty; // Họ tên
    public string PhoneNumber { get; set; } = string.Empty;
    public string EmplyeeID { get; set; } = string.Empty; //Mã nhân viên
    public DateTime CreatedAt { get; set; }
    public string Static { get; set; } = string.Empty; // True / False
}

public partial class Drive
{
    public string Id { get; set; } = string.Empty;
    public string NumberPlate { get; set; } = string.Empty; 
    public string NumberDrive { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; 
    public DateTime CreatedAt { get; set; }
    public string Static { get; set; } = string.Empty; // True / False
}

public partial class UserRole
{
    public string user_Id { get; set; } = string.Empty;
    public string role_Id { get; set; } = string.Empty; 
    public string area_Id { get; set; } = string.Empty;
    public Driver? driver { get; set; } 
    public Role? role { get; set; } 
    public Area? area { get; set; } //Bổ sung khu vực cho tài khoản
}

public partial class Role
{
    public string role_Id { get; set; } = string.Empty;
    public string role_Name { get; set; } = string.Empty; 
    public string role_NormalizedName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}