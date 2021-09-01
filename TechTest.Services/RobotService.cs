using System;
using System.Linq;
using System.Threading.Tasks;
using TechTest.Models;
using TechTest.Repositories;

namespace TechTest.Services
{
    public interface IRobotService
    {
        Task<Robot> getAvailableRobots(string condition, DateTime timeAvailable);
        Task<int> getRequiredRooms(DateTime date);
    }

    public class RobotService : IRobotService
    {
        private IRepository Repository;
        public RobotService(IRepository repository)
        {
            Repository = repository;
        }

        public async Task<Robot> getAvailableRobots(string condition, DateTime timeAvailable)
        {
            Robot robotResult = null;
            try
            { 
                var robots = await Repository.GetRobots(c => c.ConditionExpertise == condition
                    && !c.Appointments.Any(x => x.StartDate >= timeAvailable && x.EndDate < timeAvailable));

                robotResult = robots.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            return robotResult;
        }

        public async Task<int> getRequiredRooms(DateTime date)
        {
            int result = 0;
            try
            {
                result = await Repository.GetTotalOverlapAppointments(date);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

    }
}
