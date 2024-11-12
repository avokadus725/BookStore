using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Domain
{
    public class Loan
    {
        [Key]
        public int id_loan { get; set; }
        public DateTime? loan_date { get; set; }
        public DateTime? return_date { get; set; }
        [Required]
        public int id_reader { get; set; }
		[Required]
		public int id_book { get; set; }


		[NotMapped]
		public string? first_name { get; set; }
		[NotMapped]
		public string? last_name { get; set; }
		[NotMapped]
		public string? title { get; set; }

		[NotMapped]
		public List<SelectListItem>? ReaderList { get; set; }
		[NotMapped]
		public List<SelectListItem>? BookList { get; set; }
	}
}
