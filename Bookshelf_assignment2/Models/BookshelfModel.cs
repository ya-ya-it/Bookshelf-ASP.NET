namespace Assignment1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BookshelfModel : DbContext
    {
        public BookshelfModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<author> authors { get; set; }
        public virtual DbSet<Book> books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<author>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<author>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.summary)
                .IsUnicode(false);
        }
    }
}
