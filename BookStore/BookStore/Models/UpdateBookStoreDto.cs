using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class UpdateBookStoreDto
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}
