using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TechTest.Controllers;
using TechTest.Repositories;
using TechTestTests.Builders;

namespace TechTestTests
{
    [TestClass]
    public class RobotsControllerTests
    {
        [TestMethod]
        public async Task CheckItWorks()
        {
            //Arrange
            //Initialize empty in-memory db
            var options = new DbContextOptionsBuilder<DataContext>()
                      .UseInMemoryDatabase(databaseName: "techtestdb-test")
                      .Options;
            var context = new DataContext(options);
            
            //Set up test data
            context.Robots.Add(new RobotBuilder().WithConditionExpertise("Bloaty Head").Build());
            context.SaveChanges();

            var controller = new RobotsController(context);
            DateTime availableTime = DateTime.Now; 

            //Act
            //Call controller and get result
            var result = await controller.GetAvailable("Bloaty Head", availableTime);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [TestMethod]
        public async Task CheckItWorksOverLap()
        {
            //Arrange
            //Initialize empty in-memory db
            var options = new DbContextOptionsBuilder<DataContext>()
                      .UseInMemoryDatabase(databaseName: "techtestdb-test")
                      .Options;
            var context = new DataContext(options);

            //Set up test data
            context.Appointments.AddRange(new AppointmentBuilder().BuildList());
            context.SaveChanges();

            var controller = new RobotsController(context);
            DateTime availableTime = DateTime.Now;

            //Act
            //Call controller and get result
            var result = await controller.GetRequiredRoomsForDay(availableTime);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
