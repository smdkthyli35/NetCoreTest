using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string CityCode { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
