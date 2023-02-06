namespace Arts.Models
{
    public class Carts
    {
        public int Id { get; set; }
        public int id_user { get; set; }
        public int id_item { get; set; }
        public string? name { get; set; }
        public string? thumnail { get; set; }
        public double price { get; set; }
    }
}
