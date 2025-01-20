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
    public DateTimeOffset? CreatedAt { get; set; }
    public string Static { get; set; } = string.Empty; //On/Off
}

