using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSApp.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }
        [Required]
        [StringLength(30)]
        [DisplayName("First name")]
        public string FirstName { get; set; } = default!;
        [Required]
        [StringLength(30)]
        [DisplayName("Last name")]
        public string LastName { get; set; } = default!;
        [StringLength(10)]
        [DisplayName("Phone number")]
        public string? PhoneNumber { get; set; }
        public virtual ICollection<MemberInterest>? MemberInterests { get; set; } //nav
    }
}
