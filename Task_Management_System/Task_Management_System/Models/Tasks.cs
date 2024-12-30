using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Management_System.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime DeadLine { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Projects project { get; set; }
        public int TeamMemberId { get; set; }
        [ForeignKey("TeamMemberId")]
        public TeamMember teamMember { get; set; }

    }
}
