using Domain.Dtos;
using Domain.Models;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class CustomerAssembler : ICustomerAssembler
    {
        public Customer ToCustomer(CustomerDto customerDto)
        {
            return new Customer(customerDto.FullName, customerDto.CityCode, customerDto.BirthDate);
        }

        public CustomerDto ToCustomerDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                BirthDate = customer.BirthDate,
                CityCode = customer.CityCode,
                FullName = customer.FullName
            };
        }

        public List<CustomerDto> ToCustomerDtoList(List<Customer> customerList)
        {
            var response = new List<CustomerDto>();
            foreach (var customer in customerList)
            {
                var customerDto = ToCustomerDto(customer);
                response.Add(customerDto);
            }

            return response;
        }
    }
}
