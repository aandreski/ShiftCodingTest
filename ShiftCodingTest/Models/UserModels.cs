
using System.ComponentModel.DataAnnotations;

namespace ShiftCodingTest.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public Address? Address { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }
        public Company? Company { get; set; }
    }

    public class Address
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? ZIPCode { get; set; }
        public Geo? Geo { get; set; }
    }

    public class Company
    {
        public string? Name { get; set; }
        public string? CatchPhrase { get; set; }
        public string? BS { get; set; }
    }

    public class Geo
    {
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
    }
}
