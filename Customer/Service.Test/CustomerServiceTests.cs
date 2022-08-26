using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Domain;
using Domain.Dtos;
using Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Abstractions;
using Service.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Service.Test
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerAssembler> _customerAssemblerMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public CustomerServiceTests()
        {
            _customerAssemblerMock = new Mock<ICustomerAssembler>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
        }

        [Theory, AutoMoqData]
        public void CreateNewCustomer_Should_Success(CustomerDto customerDto, Customer customer, CustomerService sut)
        {
            _customerAssemblerMock.Setup(c => c.ToCustomer(customerDto)).Returns(customer);
            _customerRepositoryMock.Setup(c => c.Save(customer)).Returns(It.IsAny<Guid>());

            Action action = () => sut.CreateNew(customerDto);

            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void UpdateCustomer_Should_Success(CustomerDto customerDto, Customer customer, CustomerService sut)
        {
            _customerAssemblerMock.Setup(c => c.ToCustomer(customerDto)).Returns(customer);
            _customerRepositoryMock.Setup(c => c.Update(customer));

            Action action = () => sut.Update(customerDto);

            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void GetAll_Should_Success([Frozen] Mock<ICustomerAssembler> assembler, [Frozen] Mock<ICustomerRepository> repository, List<Customer> customers, List<CustomerDto> customerDtos, CustomerService sut)
        {
            repository.Setup(c => c.All()).Returns(customers.AsQueryable);
            assembler.Setup(c => c.ToCustomerDtoList(customers)).Returns(customerDtos);

            Action action = () =>
            {
                var result = sut.GetAll();
                result.Count().Should().Be(customerDtos.Count);
            };

            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void GetByCityCode_Should_Success([Frozen] Mock<ICustomerAssembler> assembler, [Frozen] Mock<ICustomerRepository> repository, string cityCode, List<Customer> customers, List<CustomerDto> customerDtos, CustomerService sut)
        {
            assembler.Setup(c => c.ToCustomerDtoList(customers)).Returns(customerDtos);
            repository.Setup(x => x.Find(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(customers.AsQueryable);

            Action action = () =>
            {
                var result = sut.GetByCityCode(cityCode);
                result.Should().BeEquivalentTo(customerDtos);
            };

            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void GetById_ExistingGuidPassed_Should(CustomerService sut)
        {
            var testGuid = new Guid("b5184b2c-371b-4cfe-a074-2e21a3992177");

            Action action = () => sut.GetById(testGuid);

            action.Should().NotThrow<Exception>();
        }

        public class AutoMoqDataAttribute : AutoDataAttribute
        {
            public AutoMoqDataAttribute() : base(new Fixture().Customize(new AutoMoqCustomization()))
            {
            }
        }
    }
}