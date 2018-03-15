using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.ViewModels
{
	public class ContactForm
	{
		[Required(ErrorMessage = "Please enter your name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter email address")]
		[Display(Name = "Email")]
		[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", 
			ErrorMessage = "Please enter correct email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter a subject")]
		public string Subject { get; set; }

		[Required(ErrorMessage = "Please enter a message")]
		public string Message { get; set; }
	}
}