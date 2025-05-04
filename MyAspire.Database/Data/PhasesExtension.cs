using Microsoft.EntityFrameworkCore;

public static class PhasesExtension
{
    public static void SeedPhases(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Phase>().HasData(
            new Phase { Id = 1, Name = "Phase 1" },
            new Phase { Id = 2, Name = "Phase 2" },
            new Phase { Id = 3, Name = "Phase 3" }
        );
    }
}