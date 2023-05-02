using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSApp.Models
{
    public class Interest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterestId { get; set; }
        [Required]
        [StringLength(50)]
        public string? Title { get; set; }
        [StringLength(200)]
        public string? Description { get; set; }
        public virtual ICollection<MemberInterest>? MemberInterests { get; set; } //nav
    }
}
