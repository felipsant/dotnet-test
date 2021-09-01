using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechTest.Models
{
    public class Robot
    {
        [Key]
        public int Id { get; set; }
        public string ConditionExpertise { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
