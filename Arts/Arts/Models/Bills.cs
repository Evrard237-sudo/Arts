namespace Arts.Models
{
    public class Bills
    {
        public int? Id { get; set; }
        public string? PaymentMode { get; set; }
        public string? TotalPrice { get; set; }
        public string? CreateAt { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? CardName { get; set; }
        public string? CardNumber { get; set; }
        public string? Adress { get; set; }
        public string? DetailAdress { get; set; }
        public int id_user { get; set; }
    }
}
