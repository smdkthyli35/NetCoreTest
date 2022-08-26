﻿using Domain;
using Domain.Dtos;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerAssembler _customerAssembler;

        public CustomerService(ICustomerRepository customerRepository, ICustomerAssembler customerAssembler)
        {
            _customerRepository = customerRepository;
            _customerAssembler = customerAssembler;
        }

        public void CreateNew(CustomerDto customerDto)
        {
            var customer = _customerAssembler.ToCustomer(customerDto);
            _customerRepository.Save(customer);
        }

        public void Delete(Guid id)
        {
            var isExists = _customerRepository.Get(id);
            if (isExists is null)
                throw new Exception("Id is not exists.");
            _customerRepository.Delete(id);
        }

        public List<CustomerDto> GetAll()
        {
            var all = _customerRepository.All().ToList();
            return _customerAssembler.ToCustomerDtoList(all);
        }

        public List<CustomerDto> GetByCityCode(string cityCode)
        {
            var list = _customerRepository.Find(c => c.CityCode == cityCode).ToList();
            return _customerAssembler.ToCustomerDtoList(list);
        }

        public CustomerDto GetById(Guid id)
        {
            var customer = _customerRepository.Get(id);
            if (customer == null)
            {
                throw new Exception("Customer with this id : " + id + " not found.");
            }
            var customerDto = _customerAssembler.ToCustomerDto(customer);
            return customerDto;
        }

        public CustomerDto Update(CustomerDto customer)
        {
            var existing = _customerRepository.Get(customer.Id);

            existing.SetFields(customer.FullName, customer.CityCode, customer.BirthDate);

            _customerRepository.Update(existing);

            var customerDto = _customerAssembler.ToCustomerDto(existing);

            return customerDto;
        }
    }
}
