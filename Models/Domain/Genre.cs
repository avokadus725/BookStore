using System.ComponentModel.DataAnnotations;
namespace BookStore.Models.Domain
{
    public class Genre
    {
		[Key]
		public int id_genre { get; set; }
        public string name_genre { get; set; }
    }
}
