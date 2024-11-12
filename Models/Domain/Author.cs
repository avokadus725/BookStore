using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Domain
{
    public class Author
    {
        [Key]
        public int id_author { get; set; }
        public string first_name{ get; set; }
        public string last_name { get; set; }
    }
}
