using ntgroup.Data.Entities;

namespace ntgroup.APIs.Contracts;

public interface ICustomerServer
{
    Task<bool> Create(List<CustomerDTO> listCustomers);
}