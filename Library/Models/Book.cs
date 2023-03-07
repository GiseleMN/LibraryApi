using System;
namespace Library.Models
{
    /// <summary>
    /// Class Book
    /// </summary>
	public class Book
	{
        // Book class properties
        public int BookId { get; set; }

        public string? Title { get; set; }

        public string? NumberOfPages { get; set; }

        public bool Availability { get; set; }
    }
}

