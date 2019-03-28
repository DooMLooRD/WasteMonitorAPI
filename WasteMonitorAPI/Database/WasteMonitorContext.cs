using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WasteMonitorAPI.Database
{
    public class WasteMonitorContext : DbContext
    {
        public DbSet<WasteData> WasteData { get; set; }

        public WasteMonitorContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WasteData>(e =>
                e.HasData(new WasteData() {DateTime = DateTime.Now, FillingLevel = 0.5, Weight = 20, Id = 1}));
        }
    }
}
