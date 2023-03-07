using System;
using Library.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library.Contexts
{
	public class LibraryContext :DbContext
	{
		public LibraryContext()
		{
		}

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Initial Catalog = LibraryWeb ; Data Source=localhost,1450;User ID=SA; Password= SQL@SERVER2022; TrustServerCertificate = true;");
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}

