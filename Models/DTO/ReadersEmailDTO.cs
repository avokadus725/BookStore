namespace BookStore.Models.DTO
{
    public class ReadersEmailDTO
    {
        public int id_reader { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public int total_readers_domain { get; set; }
    }
}
