using Demo1.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo1.Api.Test.Services
{
    public class INameServiceTest
    {
        private INameService _nameService;

        public INameServiceTest()
        {
            _nameService = new NameService();
        }

        [Fact]
        public void Should_isValidName_Return_False_When_ParamisEmpty()
        {
            string name = string.Empty;
            var result = _nameService.isValidName(name);
            Assert.False(result);
        }

        [Fact]
        public void Shoudl_isValidName_Return_True_When_ParamisNotEmpty()
        {
            string name = "Samed Kütahyalı";
            var result = _nameService.isValidName(name);
            Assert.True(result);
        }
    }
}
