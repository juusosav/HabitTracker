using Microsoft.EntityFrameworkCore;
using HabitTracker.Components.Models;

namespace HabitTracker.Components.Data
{
    public class HabitContext : DbContext
    {
        
        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitCompletion> HabitCompletions { get; set; }

        public DbSet<LastResetDate> LastResetDate { get; set; }

        
        public HabitContext(DbContextOptions<HabitContext> options) : base(options) { }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Habit>().HasData(
                new Habit() { Id = 1, Name="Go for a walk", IsDone = false },
                new Habit() { Id = 2, Name="Drink 2 liters of water", IsDone = false },
                new Habit() { Id = 3, Name="Walk the dog", IsDone = false }
                );

            
            modelBuilder.Entity<Habit>()
                .HasMany(h => h.HabitCompletions)
                .WithOne(hc => hc.Habit)
                .HasForeignKey(hc => hc.HabitId)
                .OnDelete(DeleteBehavior.Cascade);

        }



    }
}
