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
    public class TransRecordTest
    {
        private TransRecordController _transRecordController;
        private Mock<ITransRecordService> _transRecordServiceMock;

        [SetUp]
        public void Setup()
        {
            _transRecordServiceMock = new Mock<ITransRecordService>();
            _transRecordController = new TransRecordController(_transRecordServiceMock.Object);
        }

        [Test]
        public async Task GetTransRecordByFundNameAndDate_WithValidParameters_ReturnsOkResult()
        {
            // Arrange
            string fundName = "Metori Number 1";
            DateTime date = new DateTime(2023, 6, 1);

            IEnumerable<TransRecordDto> expectedTransRecords = new List<TransRecordDto>
            {
               new TransRecordDto {
                    TranscationId=1,
                    FundName="Metori Number 1",
                    StockName= "深证能源",
                    TransQuantity=10000,
                    TransType= "Purchase",
                    TransDate= DateTime.Parse("2023-06-01"),
                    UnitPrice=(decimal)6.83,
                    TransAmount=(decimal) 683000.00
                   }
            };

            _transRecordServiceMock.Setup(service => service.GetTransRecordByFundNameAndDateAsync(fundName, date))
                .ReturnsAsync(expectedTransRecords);
            var result = await _transRecordController.GetTransRecordByFundNameAndDate(fundName, date) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var actualTransRecords = result.Value as IEnumerable<TransRecordDto>;
            Assert.IsNotNull(actualTransRecords);
            Assert.AreEqual(expectedTransRecords, actualTransRecords);
        }
    }
}
