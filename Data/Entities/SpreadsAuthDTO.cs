using System.Security.Cryptography;
using DocumentFormat.OpenXml.Presentation;

namespace ntgroup.Data.Entities;

public class SpreadsAuthDTO
{
}

public class DriverLoginDTO
{
    public string Username {get; set;} = string.Empty;
    public string Password {get; set;} = string.Empty;
}

public class DriverRegisterDTO
{
    public string Username {get; set;} = string.Empty;
    public string Password {get; set;} = string.Empty;
    public string Area_Id {get; set;} = string.Empty;
}

