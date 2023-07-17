using Moq;
using WebApplicationtest1.Controllers;
using WebApplicationtest1.Models;
using WebApplicationtest1.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplicationtest1.ViewModels;

namespace FundTestProject1
{

    public class FundTest
    {
        private Mock<IFundService> fundServiceMock;
        private FundController fundController;

        [SetUp]
        public void Setup()
        {
            fundServiceMock = new Mock<IFundService>();
            fundController = new FundController(fundServiceMock.Object);
        }
        [Test]
        public async Task GetAllFunds_ReturnsOkResultWithFunds()
        {
            IEnumerable<Fund> expectedFunds = new List<Fund>
            {
            new Fund {
                FundName="Metori Number 1",
                FundInitialassets= (decimal?)500000.00,
                FundShare= 400000,
                FundId=100001,
                FundEstablishDate= DateTime.Parse("2023-06-01"),
                FundInitialPrice= (decimal?)1.25},
            new Fund {
                FundName="Metori Number 2",
                FundInitialassets= (decimal?) 600000.00,
                FundShare= 500000,
                FundId=100002,
                FundEstablishDate= DateTime.Parse("2023-06-01"),
                FundInitialPrice= (decimal?)1.20},
            };
            fundServiceMock.Setup(service => service.GetAllFunds()).ReturnsAsync(expectedFunds);
            var result = await fundController.GetAllFunds() as OkObjectResult;
            Assert.IsNotNull(result);
            var actualFunds = result.Value as IEnumerable<Fund>;
            Assert.IsNotNull(actualFunds);
            Assert.AreEqual(expectedFunds, actualFunds);
        }
        [Test]
        public async Task GetFundById_WithExistingId_ReturnsOkResultWithFund()
        {
            int id = 100001;
            Fund expectedFund = new Fund
            {
                FundName = "Metori Number 1",
                FundInitialassets = (decimal?)500000.00,
                FundShare = 400000,
                FundId = 100001,
                FundEstablishDate = DateTime.Parse("2023-06-01"),
                FundInitialPrice = (decimal?)1.25
            };
            fundServiceMock.Setup(service => service.GetFundById(id)).ReturnsAsync(expectedFund);
            var result = await fundController.GetFundById(id) as OkObjectResult;
            Assert.IsNotNull(result);
            var actualFund = result.Value as Fund;
            Assert.IsNotNull(actualFund);
            Assert.AreEqual(expectedFund, actualFund);


            //var result = await fundController.GetFundById(id);
            //var okResult = Assert.IsInstanceOf<OkObjectResult>(result);
            //var actualFund = Assert.IsInstanceOf<Fund>(okResult.Value);
            //Assert.AreEqual(expectedFund.FundName, actualFund.FundName);
            //Assert.AreEqual(expectedFund.FundInitialassets, actualFund.FundInitialassets);
            //Assert.AreEqual(expectedFund.FundShare, actualFund.FundShare);
            //Assert.AreEqual(expectedFund.FundId, actualFund.FundId);
            //Assert.AreEqual(expectedFund.FundEstablishDate, actualFund.FundEstablishDate);
            //Assert.AreEqual(expectedFund.FundInitialPrice, actualFund.FundInitialPrice);
        }
        [Test]
        public async Task GetFundById_WithNonExistingId_ReturnsNotFoundResult()
        {
            int id = 100008;
            Fund expectedFund = null;
            fundServiceMock.Setup(service => service.GetFundById(id)).ReturnsAsync(expectedFund);
            var result = await fundController.GetFundById(id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetAllFundTrendByDate_ReturnsOkResultWithFundTrendDtos()
        {
            DateTime date = new DateTime(2023, 6, 1);
            IEnumerable<FundTrendDto> expectedFundTrendDtos = new List<FundTrendDto>
            {
            new FundTrendDto {
                FundName= "Metori Number 1",
                FundInitialassets= (decimal)500000.00,
                FundShare= 400000,
                FundInitialPrice= (decimal)1.25,
                FundLatestnetworth= (decimal)1.21,
                FtrendDate= DateTime.Parse("2023-06-01") },
            new FundTrendDto {
                FundName= "Metori Number 2",
                FundInitialassets= (decimal)600000.00,
                FundShare= 500000,
                FundInitialPrice= (decimal)1.20,
                FundLatestnetworth= (decimal)1.11,
                FtrendDate= DateTime.Parse("2023-06-01") },
            };
            fundServiceMock.Setup(service => service.GetAllFundTrendByDate(date)).ReturnsAsync(expectedFundTrendDtos);
            var result = await fundController.GetAllFundTrendByDate(date) as OkObjectResult;
            Assert.IsNotNull(result);
            var actualFundTrendDtos = result.Value as IEnumerable<FundTrendDto>;
            Assert.IsNotNull(actualFundTrendDtos);
            Assert.AreEqual(expectedFundTrendDtos, actualFundTrendDtos);

        }
        [Test]
        public async Task GetAllFundCure_ReturnsOkResultWithFundCureDtos()
        {
            IEnumerable<FundCureDto> expectedFundCureDtos = new List<FundCureDto>
            {
            new FundCureDto {
                FundName= "Metori Number 1",
                FundLatestnetworth= (decimal)1.21,
                FtrendDate= DateTime.Parse("2023-06-01") },
            new FundCureDto {
                FundName= "Metori Number 2",
                FundLatestnetworth= (decimal)1.11,
                FtrendDate= DateTime.Parse("2023-06-01") },
            };
            fundServiceMock.Setup(service => service.GetAllFundCure()).ReturnsAsync(expectedFundCureDtos);
            var result = await fundController.GetAllFundCure() as OkObjectResult;
            Assert.IsNotNull(result);
            var actualFundCureDtos = result.Value as IEnumerable<FundCureDto>;
            Assert.IsNotNull(actualFundCureDtos);
            Assert.AreEqual(expectedFundCureDtos, actualFundCureDtos);
        }



    }
}