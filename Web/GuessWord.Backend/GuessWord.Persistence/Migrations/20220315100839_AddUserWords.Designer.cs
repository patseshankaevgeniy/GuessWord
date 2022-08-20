﻿// <auto-generated />
using GuessWord.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GuessWord.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220315100839_AddUserWords")]
    partial class AddUserWords
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GuessWord.Persistence.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles", "dbo");
                });

            modelBuilder.Entity("GuessWord.Persistence.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles", "dbo");
                });

            modelBuilder.Entity("GuessWord.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", "dbo");
                });

            modelBuilder.Entity("GuessWord.Domain.Entities.UserWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Complexity")
                        .HasColumnType("int");

                    b.Property<int>("RepeatNumber")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TargetRepeatNumber")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WordId", "UserId")
                        .IsUnique();

                    b.ToTable("UserWords", "dbo");
                });

            modelBuilder.Entity("GuessWord.Domain.Entities.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Words", "dbo");
                });

            modelBuilder.Entity("GuessWord.Domain.Entities.WordTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("TranslationId")
                        .HasColumnType("int");

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TranslationId");

                    b.HasIndex("WordId");

                    b.ToTable("WordTranslations", "dbo");
                });

            modelBuilder.Entity("GuessWord.Persistence.UserRole", b =>
                {
                    b.HasOne("GuessWord.Persistence.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuessWord.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GuessWord.Domain.Entities.UserWord", b =>
                {
                    b.HasOne("GuessWord.Domain.Entities.User", "User")
                        .WithMany("UserWords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuessWord.Domain.Entities.Word", "Word")
                        .WithMany("UserWords")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("GuessWord.Domain.Entities.WordTranslation", b =>
                {
                    b.HasOne("GuessWord.Domain.Entities.Word", "Translation")
                        .WithMany()
                        .HasForeignKey("TranslationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GuessWord.Domain.Entities.Word", "Word")
                        .WithMany("Translations")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Translation");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("GuessWord.Persistence.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("GuessWord.Domain.Entities.User", b =>
                {
                    b.Navigation("UserRoles");

                    b.Navigation("UserWords");
                });

            modelBuilder.Entity("GuessWord.Domain.Entities.Word", b =>
                {
                    b.Navigation("Translations");

                    b.Navigation("UserWords");
                });
#pragma warning restore 612, 618
        }
    }
}