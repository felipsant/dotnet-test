using System;
using Microsoft.AspNetCore.Mvc;
using TechTest.Data;
using TechTest.Domain;
using TechTest.Services;

namespace TechTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RobotsController : ControllerBase
    {
        private readonly DataContext context;

        public RobotsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("required_rooms")]
        public IActionResult GetRequiredRoomsForDay(DateTime date)
        {
            throw new NotImplementedException();
        }

        [HttpPost("available")]
        public IActionResult GetAvailable(string condition)
        {
            var repository = new Repository(context);

            var robots = repository.GetRobots().Result;
            Robot robotResult = null;

            int i = 0;
            while(i < robots.Count)
            {
                if(robotResult != null)
                {
                    break;
                }
                else if (robots[i].ConditionExpertise == condition)
                {
                    robotResult = robots[i];
                }else if (robotResult == null && robots.Count == i + 1)
                    throw new IndexOutOfRangeException();

                ++i;
            }

            if(robotResult != null)
            {
                (new EngineeringNotificationService()).NotifyRobotSelected(robotResult.Id);
                (new CustomerNotificationService()).NotifyRobotSelected(robotResult.Id);
                (new InvoicingNotificationService()).NotifyRobotSelected(robotResult.Id);
            }

            return base.Ok(new { id = robotResult.Id, conditionExpertise = robotResult.ConditionExpertise });
        }
    }
}
