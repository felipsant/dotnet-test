using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using TechTest.Repositories;
using System.Collections.Generic;
using TechTest.Models;
using Moq.Protected;
using System.Linq.Expressions;
using System.Linq;

namespace TechTest.Services.UnitTests
{
    [TestClass]
    public class RobotServiceTests
    {
        [TestMethod]
        public async Task GetAvailableRobots_Returns_AvailableRobot()
        {
            //Arrange
            string condition = "";
            int id = 1;
            DateTime timeAvailable = DateTime.Now;
            Robot robot1 = new Robot() { ConditionExpertise = condition, Id = id };
            var repository = new Mock<IRepository>();
            
            repository.Setup(c => c.GetRobots(It.IsAny<Expression<Func<Robot, bool>>>()))
                    .ReturnsAsync(new List<Robot>() { robot1 });

            var robotService = new RobotService(repository.Object);

            //Act
            var result = await robotService.getAvailableRobots(condition, timeAvailable);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetAvailableRobots_Returns_NullObject()
        {
            //Arrange
            string condition = "";
            int id = 1;
            DateTime timeAvailable = DateTime.Now;
            Robot robot1 = new Robot() { ConditionExpertise = condition, Id = id };
            var repository = new Mock<IRepository>();

            repository.Setup(c => c.GetRobots(It.IsAny<Expression<Func<Robot, bool>>>()))
                    .ReturnsAsync(new List<Robot>());

            var robotService = new RobotService(repository.Object);

            //Act
            var result = await robotService.getAvailableRobots(condition, timeAvailable);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAvailableRobots_Returns_ThrowsException()
        {
            //Arrange
            string condition = "";
            int id = 1;
            DateTime timeAvailable = DateTime.Now;
            var repository = new Mock<IRepository>();
            var robotService = new RobotService(repository.Object);

            //Act
            try
            {
                var result = await robotService.getAvailableRobots(condition, timeAvailable);
            }
            //Assert
            catch (Exception ex)
            {
                return;
            }
            Assert.Fail("If reachs this, something went wrong, no exception was throw");

        }

    }
}
