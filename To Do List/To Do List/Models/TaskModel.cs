using System.ComponentModel.DataAnnotations;

namespace To_Do_List.Models
{
    public class TaskModel
    {
        
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool InProgress { get; set; }
        public bool Completed { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deadline { get; set; }

        public bool IsDeactive { get; set; }

    }
}
