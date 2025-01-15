using System.Security.Cryptography;
using DocumentFormat.OpenXml.Presentation;

namespace ntgroup.Data.Models;

public class SpreadsAuth
{
}

public class Driver
{
    public string Id {get; set;} = string.Empty;
    public string Username {get; set;} = string.Empty;
    public string PasswordHash {get; set;} = string.Empty;
}
