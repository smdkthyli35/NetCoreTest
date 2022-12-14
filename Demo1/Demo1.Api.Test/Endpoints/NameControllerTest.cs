using Demo1.Api.Controllers;
using Demo1.Api.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo1.Api.Test.Endpoints
{
    public class NameControllerTest
    {
        private readonly Mock<INameService> _nameService;
        private readonly NameController _nameController;
        private readonly string name;

        public NameControllerTest()
        {
            name = string.Empty;
            _nameService = new Mock<INameService>();
            _nameController = new NameController(_nameService.Object);
        }

        [Fact]
        public void Should_Get_Return_StatusCode200_When_NameService_isValidName_Returns_True()
        {
            _nameService.Setup(x => x.isValidName(It.IsAny<string>())).Returns(true);
            var result = _nameController.Get(name);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Should_Get_Return_StatusCode400_When_NameService_isValidName_Returns_False()
        {
            _nameService.Setup(x => x.isValidName(It.IsAny<string>())).Returns(false);
            var result = _nameController.Get(name);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
