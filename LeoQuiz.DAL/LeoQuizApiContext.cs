using LeoQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeoQuiz.DAL
{
    public class LeoQuizApiContext : DbContext
    {
        public DbSet<User> Admin { get; set; }

        public DbSet<Answer> Answer { get; set; }

        public DbSet<PassedQuiz> PassedQuiz { get; set; }

        public DbSet<Question> Question { get; set; }

        public DbSet<Quiz> Quiz { get; set; }

        public LeoQuizApiContext(DbContextOptions op) : base(op)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quiz>(entity => {
                entity.HasOne(e => e.User)
                .WithMany(e => e.Quizzes)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("Quiz_Admin");

                entity.HasMany(e => e.Questions)
                .WithOne(e => e.Quiz)
                .HasForeignKey(e => e.QuizId)
                .HasConstraintName("Quiz_Question");

                entity.HasMany(e => e.PassedQuizzes)
                .WithOne(e => e.Quiz)
                .HasForeignKey(e => e.QuizId)
                .HasConstraintName("PassedQuiz_Quiz");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasMany(e => e.Answers)
                .WithOne(e => e.Question)
                .HasForeignKey(e => e.QuestionId);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PassedQuiz>(entity =>
            {
                entity.HasOne(e => e.User)
                .WithMany(e => e.PassedQuizzes)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("Interviewee_PassedQuiz");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
        }
    }
}
