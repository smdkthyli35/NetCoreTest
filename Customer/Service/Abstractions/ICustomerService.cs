using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface ICustomerService
    {
        void CreateNew(CustomerDto customerDto);
        void Delete(Guid id);
        CustomerDto Update(CustomerDto customer);
        List<CustomerDto> GetAll();
        List<CustomerDto> GetByCityCode(string cityCode);
        CustomerDto GetById(Guid id);
    }
}