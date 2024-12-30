using Task_Management_System.Models;

namespace Task_Management_System.ViewModels
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectId { get; set; }
        public int TeamMemberId { get; set; }
        public List<Projects> Projects { get; internal set; }
        public List<TeamMember> TeamMembers { get; internal set; }
    }
}
