using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Local_Artisan_Marketplace.Models
{
    public class ArtisanApplication
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ShopName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Specialty { get; set; } = string.Empty;

        public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;

        [StringLength(30)]
        public string Status { get; set; } = "Pending";

        [Required]
        public int ApplicantId { get; set; }

        [ForeignKey("ApplicantId")]
        public ApplicationUser Applicant { get; set; } = null!;

        [StringLength(500)]
        public string? ShopImageUrl { get; set; }
    }
}
