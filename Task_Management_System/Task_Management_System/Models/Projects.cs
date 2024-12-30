using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Task_Management_System.Models
{
    public class Projects
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Tasks> tasks { get; set; }
    }
}