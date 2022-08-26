using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Customer : Entity
    {
        public string FullName { get; protected set; }
        public string CityCode { get; protected set; }
        public DateTime BirthDate { get; protected set; }

        public Customer(string fullName, string cityCode, DateTime birthDate)
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(cityCode) || birthDate.Date == DateTime.Today)
            {
                throw new Exception("Fields are not valid to create a new customer.");
            }

            FullName = fullName;
            CityCode = cityCode;
            BirthDate = birthDate;
        }

        protected Customer()
        {
        }

        public void SetFields(string fullName, string cityCode, DateTime birthDate)
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(cityCode) || birthDate.Date == DateTime.Today)
            {
                throw new Exception("Fields are not valid to update.");
            }

            FullName = fullName;
            CityCode = cityCode;
            BirthDate = birthDate;
        }
    }
}
