using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Lafarge_TimeSheet.Models
{
	public class Register
	{
		[Display(Name = "First Name")]
		[Required(ErrorMessage = "First Name is required")]
		public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Display(Name = "Email address")]
		[Required(ErrorMessage = "Email adress is required")]
		public string EmailAdress { get; set; }


		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Confirm password")]
		[Required(ErrorMessage = "Confirm password is required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; }
	}
}
