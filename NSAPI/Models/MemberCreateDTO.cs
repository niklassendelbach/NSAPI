using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NSAPI.Models
{
    public class MemberCreateDTO
    {
        [Required]
        [StringLength(30)]
        [DisplayName("First name")]
        public string FirstName { get; set; } = default!;
        [Required]
        [StringLength(30)]
        [DisplayName("Last name")]
        public string LastName { get; set; } = default!;
        [Required]
        [StringLength(10)]
        [DisplayName("Phone number")]
        public string? PhoneNumber { get; set; }
    }
}
