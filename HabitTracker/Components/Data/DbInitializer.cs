using HabitTracker.Components.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Components.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HabitContext context)
        {
            context.Database.Migrate();
        }
    }
}
