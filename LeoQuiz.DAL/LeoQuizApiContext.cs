using LeoQuiz.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeoQuiz.DAL
{
    public class LeoQuizApiContext : IdentityDbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Answer> Answer { get; set; }

        public DbSet<PassedQuiz> PassedQuiz { get; set; }

        public DbSet<Question> Question { get; set; }

        public DbSet<Quiz> Quiz { get; set; }

        public DbSet<PassedQuizAnswer> PassedQuizAnswer { get; set; }

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
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.QuizUrl).IsRequired();

               entity.Property<bool>("isDeleted");
               entity.HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasMany(e => e.Answers)
                .WithOne(e => e.Question)
                .HasForeignKey(e => e.QuestionId);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Text).IsRequired();

                entity.Property<bool>("isDeleted");
                entity.HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);

            });

            modelBuilder.Entity<PassedQuiz>(entity =>
            {
                entity.HasOne(e => e.User)
                .WithMany(e => e.PassedQuizzes)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("Interviewee_PassedQuiz");

                entity.HasMany(e => e.PassedQuizAnswers)
                .WithOne(e => e.PassedQuiz)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.PassedQuizId)
                .HasConstraintName("PassedQuizAnswers_PassedQuiz");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.PassDate).IsRequired();
                entity.Property(e => e.QuizId).IsRequired();

                entity.Property<bool>("isDeleted");
                entity.HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);

            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasMany(e => e.PassedQuizAnswers)
                .WithOne(e => e.Answer)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.AnswerId)
                .HasConstraintName("PassedQuizAnswers_Answer");

                entity.Property(e => e.Text).IsRequired();
                entity.Property(e => e.IsCorrect).IsRequired();
                entity.Property(e => e.QuestionId).IsRequired();

                entity.Property<bool>("isDeleted");
                entity.HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);

            });

            base.OnModelCreating(modelBuilder);
        }
                
    }
}
