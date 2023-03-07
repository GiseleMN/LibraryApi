using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    /// <summary>
    /// Class User
    /// </summary>
	public class User
	{
        // User class properties // use of the DataAnnotations for database migrations
        [Key]
        public int UserId { get; set; }

        [MaxLength(150)]
        [Required]
        public string? Email { get; set; }

        [MaxLength(150)]
        [Required]
        public string? Password { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(1)]
        public string? Type { get; set; }
    }
}

