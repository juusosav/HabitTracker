using HabitTracker.Components.Data;
using HabitTracker.Components.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Components.Services
{
    public class HabitService
    {
        // Links the HabitService to the Context -> Database
        private readonly HabitContext _context;

        public HabitService(HabitContext context)
        {
            _context = context;
        }

        public async Task<List<Habit>> GetAllHabitsAsync()
        {
            return await _context.Habits.ToListAsync();
        }

        public async Task AddHabitAsync(Habit habit)
        {
            habit.IsDone = false;
            _context.Habits.Add(habit);
            await _context.SaveChangesAsync();
        }

        public async Task EditHabitAsync(Habit habit, int id)
        {
            //Find the habit by id from database, that will be edited
            var existingHabit = await _context.Habits.FindAsync(id);

            if (existingHabit != null)
            {
                Console.WriteLine($"Updating Habit: Id = {existingHabit.Id}, IsDone = {habit.IsDone}");
                //The found habit will be edited here, sending the edits to the Habit model
                existingHabit.Name = habit.Name;
                existingHabit.IsDone = habit.IsDone;

                //The found habit updates and changes saved to database
                _context.Habits.Update(existingHabit);
                int savedChanges = await _context.SaveChangesAsync();
                Console.WriteLine($"Changes saved: {savedChanges} rows affected!");
            }
        }

        public async Task DeleteHabitAsync(int habitId)
        {
            var habit = await _context.Habits.FindAsync(habitId);
            if (habit != null)
            {
                _context.Habits.Remove(habit);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Habit?> GetHabitByIdAsync(int habitId)
        {
            var habit = await _context.Habits.FindAsync(habitId);
            return habit;
        }

        // Defines the timeframe to compile completed habits
        public async Task<List<HabitCompletion>> GetCompletedHabitsForTimeframe(int habitId, string timeframe)
        {
            var now = DateTime.UtcNow;
            DateTime startDate;

            if (timeframe == "weekly")
            {
                startDate = now.AddDays(-7);
            }

            else if (timeframe == "monthly")
            {
                startDate = new DateTime(now.Year, now.Month, 1);
            }
            else
            {
                startDate = new DateTime(2025, 11, 22);
            }

            return await _context.HabitCompletions
            .Where(hc => hc.HabitId == habitId && hc.CompletionDate >= startDate)
            .ToListAsync();
        }

        
        public async Task HandleIsDoneChanged(int habitId, bool isDone)
        {
            var habit = await _context.Habits.FirstOrDefaultAsync(h => h.Id == habitId);

            if (habit != null)
            {
                habit.IsDone = isDone;

                if (isDone)
                {
                    bool alreadyCompletedToday = await _context.HabitCompletions
                        .AnyAsync(hc => hc.HabitId == habitId && hc.CompletionDate.Date == DateTime.Today);

                    if (!alreadyCompletedToday)
                    {
                        var completion = new HabitCompletion
                        {
                            HabitId = habitId,
                            CompletionDate = DateTime.UtcNow
                        };

                        await _context.HabitCompletions.AddAsync(completion);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
        
        public async Task<LastResetDate?> GetLastResetDateAsync()
        {
            return await _context.LastResetDate.OrderByDescending(l => l.Date).FirstOrDefaultAsync();
        }

        // This then updates existing reset date, or adds a new one if none yet exist
        public async Task SaveLastResetDateAsync(LastResetDate lastResetDate)
        {
            var existingRecord = await _context.LastResetDate.FirstOrDefaultAsync();

            if (existingRecord != null)
            {
                existingRecord.Date = lastResetDate.Date;
            }
            else
            {
                _context.LastResetDate.Add(lastResetDate);
            }
            await _context.SaveChangesAsync();
        }
    }
}
