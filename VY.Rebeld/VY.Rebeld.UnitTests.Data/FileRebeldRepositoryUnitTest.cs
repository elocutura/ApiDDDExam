using System;
using Xunit;
using Moq;
using VY.Rebeld.Data.Impl.Repositories;
using System.Collections.Generic;
using VY.Rebeld.Dtos;
using VY.Rebeld.Infrastructure.Contracts;
using System.Threading.Tasks;
using System.Linq;

namespace VY.Rebeld.UnitTests.Data
{
    public class FileRebeldRepositoryUnitTest
    {
        [InlineData("Ani")]
        [InlineData("Rey")]
        [Theory]
        public async void WhenSearchForName_NameNotFound(string name)
        {
            Dictionary<string, RebeldSightingDto> fileReturn = new Dictionary<string, RebeldSightingDto>();
            fileReturn.Add("Test", new RebeldSightingDto() { SightText = "TestSighting_Nonsense"});
            var parseFileToDictFuncReturn = new OperationResult<Dictionary<string, RebeldSightingDto>>();
            parseFileToDictFuncReturn.Result = fileReturn;

            Mock<FileRebeldRepository> repoMock = new Mock<FileRebeldRepository>();
            repoMock.Setup(c => c.ParseFileToDict()).Returns(Task.FromResult(parseFileToDictFuncReturn));

            var result = await repoMock.Object.GetSightingByNameAsync(name);

            Assert.NotNull(result);
            Assert.Null(result.Result);
            Assert.True(result.HasErrors());
            Assert.True(result.GetAllErrors().Where(c => c.Code == 204).Any());
        }

        [Fact]
        public async void WhenSearchForName_SightingFound()
        {
            Dictionary<string, RebeldSightingDto> fileReturn = new Dictionary<string, RebeldSightingDto>();
            fileReturn.Add("Test", new RebeldSightingDto() { SightText = "TestSighting_Nonsense" });
            var parseFileToDictFuncReturn = new OperationResult<Dictionary<string, RebeldSightingDto>>();
            parseFileToDictFuncReturn.Result = fileReturn;

            Mock<FileRebeldRepository> repoMock = new Mock<FileRebeldRepository>();
            repoMock.Setup(c => c.ParseFileToDict()).Returns(Task.FromResult(parseFileToDictFuncReturn));

            var result = await repoMock.Object.GetSightingByNameAsync("Test");

            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.False(result.HasErrors());
            Assert.True(result.Result.SightText == "TestSighting_Nonsense");
        }
    }

}
