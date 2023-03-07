using System;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
	/// <summary>
	/// Class Login Model
	/// </summary>
	public class LoginViewModel
	{
		[Required(ErrorMessage = "The e-mail is required !!")]
		public string? Email { get; set; }

		[Required(ErrorMessage = " The Password is required !! ")]
		public string? Password { get; set; }

	}
}

