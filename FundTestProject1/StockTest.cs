using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplicationtest1.Controllers;
using WebApplicationtest1.Models;
using WebApplicationtest1.Services;

namespace FundTestProject1
{
    public class StockTest
    {
        private StockController _stockController;
        private Mock<IStockService> _stockServiceMock;

        [SetUp]
        public void Setup()
        {
            _stockServiceMock = new Mock<IStockService>();
            _stockController = new StockController(_stockServiceMock.Object);
        }

        [Test]
        public async Task GetAllStocks_ReturnsOkResult()
        {
            IEnumerable<Stock> expectedStocks = new List<Stock>
            {
                new Stock
                {
                    StockId=100001,
                    StockName= "平安银行",
                    StockType= "深证A股"
                },
                new Stock {
                    StockId=100027,
                    StockName= "深证能源",
                    StockType= "深证A股"
                }
            };
            _stockServiceMock.Setup(service => service.GetAllStocks())
                .ReturnsAsync(expectedStocks);
            var result = await _stockController.GetAllStocks() as OkObjectResult;
            Assert.IsNotNull(result);
            var actualStocks = result.Value as IEnumerable<Stock>;
            Assert.IsNotNull(actualStocks);
            Assert.AreEqual(expectedStocks, actualStocks);
        }

        [Test]
        public async Task GetStockById_WithExistingId_ReturnsOkResult()
        {
            int stockId = 100001;
            Stock expectedStock = new Stock
            {
               
                    StockId=100001,
                    StockName= "平安银行",
                    StockType= "深证A股"
              
            };
            _stockServiceMock.Setup(service => service.GetStockById(stockId))
                .ReturnsAsync(expectedStock);
            var result = await _stockController.GetStockById(stockId) as OkObjectResult;

            Assert.IsNotNull(result);
            var actualStock = result.Value as Stock;
            Assert.IsNotNull(actualStock);
            Assert.AreEqual(expectedStock, actualStock);
        }

        [Test]
        public async Task GetStockById_WithNonExistingId_ReturnsNotFoundResult()
        {
            int stockId = 999;

            _stockServiceMock.Setup(service => service.GetStockById(stockId))
                .ReturnsAsync((Stock)null);
            var result = await _stockController.GetStockById(stockId);
            Assert.IsInstanceOf<NotFoundResult>(result);
            //Assert.IsNotNull(result);
            //Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Test]
        public async Task AddStock_ValidStock_ReturnsOkResult()
        {
            Stock stock = new Stock
            {
                StockId = 100099,
                StockName = "横琴银行",
                StockType = "深证A股"

            };
            var result = await _stockController.AddStock(stock) as OkResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [Test]
        public async Task DeleteStock_WithExistingId_ReturnsOkResult()
        {
            int stockId = 100001;
            var result = await _stockController.DeleteStock(stockId) as OkResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

    }
}
