using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechTest.Models;

namespace TechTest.Repositories
{

    public interface IRepository
    {
        Task<List<Robot>> GetRobots();
        Task<List<Robot>> GetRobots(Expression<Func<Robot, bool>> conditions);
        Task<int> GetTotalOverlapAppointments(DateTime date);
    }
    /// <summary>
    /// This class emulates the database access
    /// </summary>
    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public Task<List<Robot>> GetRobots()
        {
            return GetRobots(x => true);
        }

        public async Task<List<Robot>> GetRobots(Expression<Func<Robot, bool>> conditions)
        {
            return await context.Robots.Where(conditions).Include(x => x.Appointments).ToListAsync();
        }

        public async Task<int> GetTotalOverlapAppointments(DateTime date)
        {
            var appointmentList = await context.Appointments.Where(c => c.StartDate.Date == date.Date).ToListAsync();
            int total = appointmentList.Count(c => 
                appointmentList.Any(x=> x.StartDate >= c.StartDate && x.EndDate < c.StartDate)
            );
            return total;
        }
    }
}
