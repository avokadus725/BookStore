using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Models.Domain
{
    public class Book
    {
        [Key]
		public int id_book { get; set; }
		[Required]
		public string title { get; set; }
		[Required]
		public int published_year { get; set; }
		[Required]
		public string? description_book { get; set; }
		[Required]
		public int id_author { get; set; }
		[Required]
		public int id_genre { get; set; }

		[NotMapped]
		public string? first_name { get; set; }

		[NotMapped]
		public string? last_name { get; set; }
		[NotMapped]
        public string? name_genre { get; set; }

		[NotMapped]
		public List<SelectListItem> ? AuthorList { get; set; }
		[NotMapped]
		public List<SelectListItem> ? GenreList { get; set; }


		[NotMapped]
		public int LoanCount { get; set; }
	}
}
