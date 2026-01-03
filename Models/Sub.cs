using System.ComponentModel.DataAnnotations;

namespace INeed.Models
{
    public class Sub
    {
        [Key]
        [Display(Name = "ID subskrybcji: ")]
        public int SubId { get; set; }

        [Display(Name = "Czy subskrybcja jest aktywna: ")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Czy subskrybent zgodził się na newsletter: ")]
        public bool Newsletter { get; set; } = true;

        [EmailAddress]
        [Required(ErrorMessage = "Adres e-mail subskrybenta jest wymagany.")]
        [Display(Name = "Adres e-mail subskrybenta: ")]
        [StringLength(50, ErrorMessage = "Adres e-mail subskrybenta nie może być dłuższy niż 50 znaków.")]
        public string? Email { get; set; }

        [Display(Name = "Data dodania subskrybenta: ")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm:ss}")]
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
