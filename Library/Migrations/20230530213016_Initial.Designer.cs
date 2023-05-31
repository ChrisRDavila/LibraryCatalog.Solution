﻿// <auto-generated />
using System;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20230530213016_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Library.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AFirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("ALastName")
                        .HasColumnType("longtext");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Library.Models.AuthorBook", b =>
                {
                    b.Property<int>("AuthorBookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.HasKey("AuthorBookId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("AuthorBooks");
                });

            modelBuilder.Entity("Library.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PublicationYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library.Models.Copy", b =>
                {
                    b.Property<int>("CopyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("CopyId");

                    b.HasIndex("BookId");

                    b.ToTable("Copies");
                });

            modelBuilder.Entity("Library.Models.CopyPatron", b =>
                {
                    b.Property<int>("CopyPatronId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CheckoutDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CopyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsOverdue")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("PatronId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.HasKey("CopyPatronId");

                    b.HasIndex("CopyId");

                    b.HasIndex("PatronId");

                    b.ToTable("CopyPatrons");
                });

            modelBuilder.Entity("Library.Models.Librarian", b =>
                {
                    b.Property<int>("LibrarianId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LName")
                        .HasColumnType("longtext");

                    b.HasKey("LibrarianId");

                    b.ToTable("Librarians");
                });

            modelBuilder.Entity("Library.Models.Patron", b =>
                {
                    b.Property<int>("PatronId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PName")
                        .HasColumnType("longtext");

                    b.HasKey("PatronId");

                    b.ToTable("Patrons");
                });

            modelBuilder.Entity("Library.Models.AuthorBook", b =>
                {
                    b.HasOne("Library.Models.Author", "Author")
                        .WithMany("JoinEntities")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Models.Book", "Book")
                        .WithMany("JoinEntities")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Library.Models.Copy", b =>
                {
                    b.HasOne("Library.Models.Book", "Book")
                        .WithMany("Copies")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Library.Models.CopyPatron", b =>
                {
                    b.HasOne("Library.Models.Copy", "Copy")
                        .WithMany("JoinEntities")
                        .HasForeignKey("CopyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Models.Patron", "Patron")
                        .WithMany("JoinEntities")
                        .HasForeignKey("PatronId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Copy");

                    b.Navigation("Patron");
                });

            modelBuilder.Entity("Library.Models.Author", b =>
                {
                    b.Navigation("JoinEntities");
                });

            modelBuilder.Entity("Library.Models.Book", b =>
                {
                    b.Navigation("Copies");

                    b.Navigation("JoinEntities");
                });

            modelBuilder.Entity("Library.Models.Copy", b =>
                {
                    b.Navigation("JoinEntities");
                });

            modelBuilder.Entity("Library.Models.Patron", b =>
                {
                    b.Navigation("JoinEntities");
                });
#pragma warning restore 612, 618
        }
    }
}