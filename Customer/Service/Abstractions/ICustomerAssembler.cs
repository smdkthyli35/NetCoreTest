using Domain.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface ICustomerAssembler
    {
        Customer ToCustomer(CustomerDto customerDto);
        CustomerDto ToCustomerDto(Customer customer);
        List<CustomerDto> ToCustomerDtoList(List<Customer> customerList);
    }
}
