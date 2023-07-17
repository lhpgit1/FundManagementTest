using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationtest1.Controllers;
using WebApplicationtest1.Services;
using WebApplicationtest1.ViewModels;

namespace FundTestProject1
{
    public class PortfolioTest
    {
        private PortfolioController _portfolioController;
        private Mock<IInvestmentPortfolioService> _portfolioServiceMock;

        [SetUp]
        public void Setup()
        {
            _portfolioServiceMock = new Mock<IInvestmentPortfolioService>();
            _portfolioController = new PortfolioController(_portfolioServiceMock.Object);
        }

        [Test]
        public async Task GetPortfolioByFundNameAndTime_WithValidParameters_ReturnsOkResult()
        {
            string fundName = "Metori Number 1";
            DateTime date = new DateTime(2023, 6, 1);

            IEnumerable<PortfolioDto> expectedPortfolios = new List<PortfolioDto>
            {
                new PortfolioDto{
                 PortfolioId= 1,
                 FundName="Metori Number 1",
                 StockName= "深证能源",
                 StockType= "深证A股",
                 HoldingQuantity= 10000,
                 PurchaseCoast= (decimal)68300.00,
                 HolidingDate= DateTime.Parse("2023-06-01"),
                 StockDailyCloseprice=(decimal) 6.83
                },
                new PortfolioDto{
                 PortfolioId= 3,
                 FundName="Metori Number 1",
                 StockName= "深证华强",
                 StockType= "深证A股",
                 HoldingQuantity= 20000,
                 PurchaseCoast= (decimal)235200.00,
                 HolidingDate= DateTime.Parse("2023-06-01"),
                 StockDailyCloseprice=(decimal) 11.76
                },

            };
            _portfolioServiceMock.Setup(service => service.GetPortfolioByFundNameAndTime(fundName, date))
                .ReturnsAsync(expectedPortfolios);
            var result = await _portfolioController.GetPortfolioByFundNameAndTime(fundName, date) as OkObjectResult;
            Assert.IsNotNull(result);
            var actualPortfolios = result.Value as IEnumerable<PortfolioDto>;
            Assert.IsNotNull(actualPortfolios);
            Assert.AreEqual(expectedPortfolios, actualPortfolios);
        }

    }
}
