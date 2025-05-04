using Microsoft.EntityFrameworkCore;

public static class QuestionsExtensions
{
    public static void SeedQuestions(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>().HasData(
            // Round 1 Questions
            new Question { Id = 1, Text = "What is 5 + 3?", Answer = 8, RoundId = 1 },
            new Question { Id = 2, Text = "What is 12 - 4?", Answer = 8, RoundId = 1 },
            new Question { Id = 3, Text = "What is 7 * 6?", Answer = 42, RoundId = 1 },
            new Question { Id = 4, Text = "What is 81 / 9?", Answer = 9, RoundId = 1 },
            new Question { Id = 5, Text = "What is the square root of 64?", Answer = 8, RoundId = 1 },
            new Question { Id = 6, Text = "What is 15 % 4?", Answer = 3, RoundId = 1 },
            new Question { Id = 7, Text = "What is 2^3?", Answer = 8, RoundId = 1 },
            new Question { Id = 8, Text = "What is 100 - 45?", Answer = 55, RoundId = 1 },
            new Question { Id = 9, Text = "What is 14 + 27?", Answer = 41, RoundId = 1 },
            new Question { Id = 10, Text = "What is 9 * 9?", Answer = 81, RoundId = 1 },

            // Round 2 Questions
            new Question { Id = 11, Text = "What is 25 + 17?", Answer = 42, RoundId = 2 },
            new Question { Id = 12, Text = "What is 144 / 12?", Answer = 12, RoundId = 2 },
            new Question { Id = 13, Text = "What is 18 * 3?", Answer = 54, RoundId = 2 },
            new Question { Id = 14, Text = "What is 99 - 33?", Answer = 66, RoundId = 2 },
            new Question { Id = 15, Text = "What is 11^2?", Answer = 121, RoundId = 2 },
            new Question { Id = 16, Text = "What is the cube root of 27?", Answer = 3, RoundId = 2 },
            new Question { Id = 17, Text = "What is 7 * 8?", Answer = 56, RoundId = 2 },
            new Question { Id = 18, Text = "What is 50 % 7?", Answer = 1, RoundId = 2 },
            new Question { Id = 19, Text = "What is 64 / 8?", Answer = 8, RoundId = 2 },
            new Question { Id = 20, Text = "What is 123 - 23?", Answer = 100, RoundId = 2 },

            // Round 3 Questions
            new Question { Id = 21, Text = "What is 3^4?", Answer = 81, RoundId = 3 },
            new Question { Id = 22, Text = "What is 256 / 16?", Answer = 16, RoundId = 3 },
            new Question { Id = 23, Text = "What is 45 * 2?", Answer = 90, RoundId = 3 },
            new Question { Id = 24, Text = "What is 200 - 75?", Answer = 125, RoundId = 3 },
            new Question { Id = 25, Text = "What is the square root of 121?", Answer = 11, RoundId = 3 },
            new Question { Id = 26, Text = "What is 10^3?", Answer = 1000, RoundId = 3 },
            new Question { Id = 27, Text = "What is 81 % 7?", Answer = 4, RoundId = 3 },
            new Question { Id = 28, Text = "What is 15 * 15?", Answer = 225, RoundId = 3 },
            new Question { Id = 29, Text = "What is 500 / 25?", Answer = 20, RoundId = 3 },
            new Question { Id = 30, Text = "What is 99 + 101?", Answer = 200, RoundId = 3 }
        );
    }
}