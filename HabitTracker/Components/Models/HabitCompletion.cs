namespace HabitTracker.Components.Models
{
    public class HabitCompletion
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public DateTime CompletionDate {  get; set; }
        public Habit? Habit { get; set; }
    }
}
