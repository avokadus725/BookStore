using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BookStore.Models.Domain
{
    public class Reader
    {
        [Key]
        public int id_reader { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }

        [NotMapped]
        public int total_readers_domain { get; set; }
    }
}
