using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using PrinterApplication.Logic;
using PrinterApplication.Models;
using PrinterApplication.Providers;
using PrinterApplicationTests.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterApplication.Logic.Tests
{
    [TestClass()]
    public class PrinterTests
    {
        [TestMethod()]
        public void PrintUserInfoByIdTest()
        {
            // Arrange
            var userId = (new Random()).Next(5);
            var dbProvider = new DBProviderFake();
            var printer = new Printer(dbProvider);

            // Act
            printer.PrintUserInfoById(userId);

            //Assert 
            Assert.IsNotNull(printer.lastTextWrittenInConsole);
        }

        [TestMethod()]
        public void PrintUserInfoByIdTestByMock()
        {
            // Arrange
            var mock = new Mock<IDBProvider>();
            var printer = new Printer(mock.Object);

            // old school
            User user = new User() { UserId = 10, FirstName = "FirstName", SecondName = "SecondName" };
            // AutoFixture
            user = new Fixture().Create<User>();

            mock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(user);
            var userId = new Fixture().Create<int>();
            
            // Act
            printer.PrintUserInfoById(userId);

            //Assert 
            Assert.IsNotNull(printer.lastTextWrittenInConsole);
        }
    }
}