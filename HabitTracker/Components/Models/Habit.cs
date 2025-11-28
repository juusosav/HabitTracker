using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Components.Models
{
    public class Habit
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Habit name cannot exceed 50 characters.")]
        public string? Name { get; set; }

        public bool IsDone { get; set; } = false;

        public ICollection<HabitCompletion>? HabitCompletions { get; set; }
    }
}
