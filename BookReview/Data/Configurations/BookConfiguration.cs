using BookReview.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReview.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Id)
            .HasDefaultValueSql("NEWID()");
            builder.Property(b => b.Price).HasColumnType("decimal(10,2)");
            builder.HasOne(x => x.Author).WithMany().HasForeignKey(x => x.AuthorId);
        }
    }
}
