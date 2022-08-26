using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Domain.Models;
using FluentAssertions;
using System;
using Xunit;

namespace Domain.Test
{
    public class CustomerTests
    {
        [Theory, AutoMoqData]
        public void Create_Customer_Should_Throw_Exception_When_FullName_Is_Empty(string cityCode, DateTime birthDate)
        {
            Assert.Throws<Exception>(() => new Customer(string.Empty, cityCode, birthDate));
        }

        [Theory, AutoMoqData]
        public void Create_Customer_Should_Throw_Exception_When_CityCode_Is_Empty(string fullName, DateTime birthDate)
        {
            Assert.Throws<Exception>(() => new Customer(fullName, string.Empty, birthDate));
        }

        [Theory, AutoMoqData]
        public void Create_Customer_Should_Throw_Exception_When_BirthDate_Is_Invalid(string fullName, string cityCode)
        {
            Assert.Throws<Exception>(() => new Customer(fullName, cityCode, DateTime.Today));
        }

        [Theory, AutoMoqData]
        public void Create_Customer_Should_Success(string fullName, string cityCode, DateTime birthDate)
        {
            var customer = new Customer(fullName, cityCode, birthDate);

            customer.FullName.Should().Be(fullName);
            customer.CityCode.Should().Be(cityCode);
            customer.BirthDate.Should().Be(birthDate);
        }

        [Theory, AutoMoqData]
        public void SetFields_Should_Update_Fields(string fullName, string cityCode, DateTime birthDate, Customer customer)
        {
            customer.SetFields(fullName, cityCode, birthDate);

            customer.FullName.Should().Be(fullName);
            customer.CityCode.Should().Be(cityCode);
            customer.BirthDate.Should().Be(birthDate);
        }
    }

    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}