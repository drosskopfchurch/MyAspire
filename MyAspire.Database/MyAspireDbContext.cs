using Microsoft.EntityFrameworkCore;

public class MyAspireDbContext(DbContextOptions<MyAspireDbContext> options) : DbContext(options)
{
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Answer> Answers { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Phase> Phases { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.SeedPhases();
        modelBuilder.SeedQuestions();
    }
}