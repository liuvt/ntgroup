using ntgroup.Data.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ntgroup.Services.Interfaces;

public interface ICustomerService
{
    //Read File
    Task<List<Customer>> GetsByExcel(IBrowserFile _file, string _customerType);
} 
