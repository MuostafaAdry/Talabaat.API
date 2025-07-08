namespace Domain.Models.Identity
{
    public class Addrerss
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
       
        public ApplicationUser User  { get; set; }
        public string UserId { get; set; }
    }
}