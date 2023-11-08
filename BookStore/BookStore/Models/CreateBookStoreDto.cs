using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class CreateBookStoreDto
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? ContactEmail { get; set; }
        public int? ContactNumber { get; set; }
        [Required]
        public string City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}