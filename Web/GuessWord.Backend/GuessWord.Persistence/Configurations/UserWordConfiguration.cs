using GuessWord.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuessWord.Persistence.Configurations
{
    public class UserWordConfiguration : IEntityTypeConfiguration<UserWord>
    {
        public void Configure(EntityTypeBuilder<UserWord> builder)
        {
            builder.ToTable("UserWords", "dbo");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.WordId, x.UserId }).IsUnique();

            builder
                .HasOne(x => x.Word)
                .WithMany(x => x.UserWords)
                .HasForeignKey(x => x.WordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.UserWords)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
