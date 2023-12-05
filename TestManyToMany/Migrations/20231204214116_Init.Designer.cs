﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestManyToMany;

#nullable disable

namespace TestManyToMany.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231204214116_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookEntityCategoryEntity", b =>
                {
                    b.Property<int>("BooksBookEntityId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriesCategoryEntityId")
                        .HasColumnType("int");

                    b.HasKey("BooksBookEntityId", "CategoriesCategoryEntityId");

                    b.HasIndex("CategoriesCategoryEntityId");

                    b.ToTable("BookEntityCategoryEntity");
                });

            modelBuilder.Entity("TestManyToMany.BookEntity", b =>
                {
                    b.Property<int>("BookEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookEntityId"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookEntityId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("TestManyToMany.CategoryEntity", b =>
                {
                    b.Property<int>("CategoryEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryEntityId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryEntityId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BookEntityCategoryEntity", b =>
                {
                    b.HasOne("TestManyToMany.BookEntity", null)
                        .WithMany()
                        .HasForeignKey("BooksBookEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestManyToMany.CategoryEntity", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}