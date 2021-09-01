using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechTest.Models;
using TechTest.Repositories;
using TechTest.Services;

namespace TechTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RobotsController : ControllerBase
    {
        private readonly DataContext context;
        private IRobotService RobotService;

        public RobotsController(DataContext context)
        {
            this.context = context;
            var repository = new Repository(context);
            RobotService = new RobotService(repository);
        }

        [HttpGet("required_rooms")]
        public async Task<IActionResult> GetRequiredRoomsForDay(DateTime date)
        {
            int result = 0;
            try
            {
                result = await RobotService.getRequiredRooms(date);
            }
            catch(Exception ex)
            {
                throw;
            }
            return base.Ok(result);
        }

        [HttpPost("available")]
        public async Task<IActionResult> GetAvailable(string condition, DateTime timeAvailable)
        {
            Robot robotResult = null;
            try
            {
                robotResult = await RobotService.getAvailableRobots(condition, timeAvailable);
                (new EngineeringNotificationService()).NotifyRobotSelected(robotResult.Id);
                (new CustomerNotificationService()).NotifyRobotSelected(robotResult.Id);
                (new InvoicingNotificationService()).NotifyRobotSelected(robotResult.Id);
                //TODO: Fix this because the object will be null in case it doesn't find, and this will throw exception;
                return base.Ok(new { id = robotResult.Id, conditionExpertise = robotResult.ConditionExpertise });
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
    }
}
