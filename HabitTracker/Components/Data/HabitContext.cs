using Microsoft.EntityFrameworkCore;
using HabitTracker.Components.Models;

namespace HabitTracker.Components.Data
{
    public class HabitContext : DbContext
    {
        //Create a table to hold Habits in the context database
        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitCompletion> HabitCompletions { get; set; }

        public DbSet<LastResetDate> LastResetDate { get; set; }

        //Constructor for the database context
        public HabitContext(DbContextOptions<HabitContext> options) : base(options) { }

        //Create the table for Habits inside the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Habit>().HasData(
                new Habit() { Id = 1, Name="Go for a walk", IsDone = false },
                new Habit() { Id = 2, Name="Drink 2 liters of water", IsDone = false },
                new Habit() { Id = 3, Name="Walk the dog", IsDone = false }
                );

            //Create table for HabitCompletions
            modelBuilder.Entity<Habit>()
                .HasMany(h => h.HabitCompletions)
                .WithOne(hc => hc.Habit)
                .HasForeignKey(hc => hc.HabitId)
                .OnDelete(DeleteBehavior.Cascade);

        }



    }
}
