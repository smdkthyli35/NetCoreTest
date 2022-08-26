using Api.Controllers;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Domain.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Abstractions;
using Xunit;

namespace Api.Test
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly CustomersController _customersController;

        public CustomerControllerTests()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _customersController = new CustomersController(_customerServiceMock.Object);
        }

        [Theory, AutoMoqData]
        public void GetAll_Should_Return_As_Expected(List<CustomerDto> expected)
        {
            _customerServiceMock.Setup(c => c.GetAll()).Returns(expected);

            var result = _customersController.GetAll();

            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<List<CustomerDto>>().Subject;

            Assert.Equal(expected, actual);
        }

        public class AutoMoqDataAttribute : AutoDataAttribute
        {
            public AutoMoqDataAttribute() : base(new Fixture().Customize(new AutoMoqCustomization()))
            {
            }
        }
    }
}
