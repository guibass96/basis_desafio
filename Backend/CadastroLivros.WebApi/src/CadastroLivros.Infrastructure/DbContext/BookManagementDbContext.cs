using CadastroLivros.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadastroLivros.Infrastructure.Context
{
    public class BookManagementDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookManagementDbContext(DbContextOptions<BookManagementDbContext> options) : base(options)
        {
        }

        public DbSet<BookPurchaseOptionEntity> BookPurchaseOptions { get; set; }
        public DbSet<AuthorEntity> Author { get; set; }
        public DbSet<SubjectEntity> Subject { get; set; }
        public DbSet<BookEntity> Book { get; set; }
        public DbSet<BookAuthorsEntity> BookAuthor { get; set; }
        public DbSet<BookSubjectEntity> BookSubject { get; set; }
        public DbSet<BookAuthorsEntity> BookAuthors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthorEntity>()
                .HasKey(a => a.AuthorId);

            modelBuilder.Entity<SubjectEntity>()
                .HasKey(a => a.SubjectId);

            modelBuilder.Entity<BookPurchaseOptionEntity>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<BookEntity>()
                .HasKey(a => a.BookId);

          
            modelBuilder.Entity<BookAuthorsEntity>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthorsEntity>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId)
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<BookAuthorsEntity>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);  

         
            modelBuilder.Entity<BookSubjectEntity>()
                .HasKey(bs => new { bs.BookId, bs.SubjectId });

            modelBuilder.Entity<BookSubjectEntity>()
                .HasOne(bs => bs.Book)
                .WithMany(b => b.BookSubjects)
                .HasForeignKey(bs => bs.BookId)
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<BookSubjectEntity>()
                .HasOne(bs => bs.Subject)
                .WithMany(s => s.BookSubjects)
                .HasForeignKey(bs => bs.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);  

          
            modelBuilder.Entity<BookPurchaseOptionEntity>()
                .HasOne(bpo => bpo.Book)
                .WithMany(b => b.PurchaseOptions)
                .HasForeignKey(bpo => bpo.BookId)
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<BookAuthorsEntity>()
                .Navigation(ba => ba.Book)
                .AutoInclude();
            modelBuilder.Entity<BookAuthorsEntity>()
                .Navigation(ba => ba.Author)
                .AutoInclude();

            modelBuilder.Entity<BookSubjectEntity>()
                .Navigation(bs => bs.Book)
                .AutoInclude();
            modelBuilder.Entity<BookSubjectEntity>()
                .Navigation(bs => bs.Subject)
                .AutoInclude();

            modelBuilder.Entity<BookPurchaseOptionEntity>()
                .Navigation(bpo => bpo.Book)
                .AutoInclude();
        }
    }
}
