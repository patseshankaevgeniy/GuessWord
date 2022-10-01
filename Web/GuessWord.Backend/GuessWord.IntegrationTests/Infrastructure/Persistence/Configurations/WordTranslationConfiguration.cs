﻿using GuessWord.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuessWord.IntegrationTests.Persistence.Configurations
{
    public class WordTranslationConfiguration : IEntityTypeConfiguration<WordTranslation>
    {
        public void Configure(EntityTypeBuilder<WordTranslation> builder)
        {
            builder.ToTable("WordTranslations", "dbo");
            builder.HasKey(t => t.Id);

            builder
                .HasOne(t => t.Word)
                .WithMany(t => t.Translations)
                .HasForeignKey(t => t.WordId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(t => t.Translation)
                .WithMany()
                .HasForeignKey(t => t.TranslationId)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}