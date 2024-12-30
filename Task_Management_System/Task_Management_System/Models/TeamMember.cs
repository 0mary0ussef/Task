using System.ComponentModel.DataAnnotations;

namespace Task_Management_System.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Role { get; set; }
        public ICollection<Tasks> tasks { get; set; }
    }
}