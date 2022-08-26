using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Repository.Test
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public void Save_Should_Save_The_Customer_And_Should_Return_All_Count_As_One()
        {
            var customer1 = new Customer("Samed Kütahyalı", "IZM", DateTime.Today.AddYears(22));

            var options = new DbContextOptionsBuilder<CustomerDbContext>()
               .UseSqlServer("CustomerTestDb")
               .Options;

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.Save(customer1);
                context.SaveChanges();
            };

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.All().Count().Should().Be(1);
            }
        }

        [Fact]
        public void Delete_Should_Delete_The_Customer_And_Should_Return_All_Count_As_One()
        {
            var customer1 = new Customer("Samed Kütahyalı", "IZM", DateTime.Today.AddYears(22));
            var customer2 = new Customer("Samed Kütahyalı", "IZM", DateTime.Today.AddYears(22));

            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseSqlServer("CustomerTestDb")
                .Options;

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.Save(customer1);
                repository.Save(customer2);
                context.SaveChanges();
            }

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.Delete(customer1.Id);
                context.SaveChanges();
            }

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.All().Count().Should().Be(1);
            }
        }

        [Fact]
        public void Update_Should_Update_The_Consumer()
        {
            var customer = new Customer("Samed Kütahyalı", "IZM", DateTime.Today.AddYears(22));

            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseSqlServer("CustomerTestDb")
                .Options;

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.Save(customer);
                context.SaveChanges();
            }

            customer.SetFields("Samed K.", "IZMIR", customer.BirthDate);

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.Update(customer);
                context.SaveChanges();
            }

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.Get(customer.Id);

                result.Should().NotBe(null);
                result.FullName.Should().Be(customer.FullName);
                result.CityCode.Should().Be(customer.CityCode);
                result.BirthDate.Should().Be(customer.BirthDate);
            }
        }

        [Fact]
        public void Find_Should_Find_The_Customer_And_Should_Return_All_Count_As_One()
        {
            var customer1 = new Customer("Samed Kütahyalı", "IST", DateTime.Today.AddYears(22));
            var customer2 = new Customer("Samed Kütahyalı", "IZM", DateTime.Today.AddYears(22));

            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseSqlServer("CustomerTestDb")
                .Options;

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                repository.Save(customer1);
                repository.Save(customer2);
                context.SaveChanges();
            }

            using (var context = new CustomerDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var result = repository.Find(c => c.CityCode == customer1.CityCode);
                result.Should().NotBeNull();
                result.Count().Should().Be(1);
            }
        }
    }
}
