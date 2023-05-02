using Microsoft.Build.Framework;

namespace NSAPI.Models
{
    public class MemberInterestUpdateDTO
    {
        [Required]
        public int MemberInterestId { get; set; }
        public int FK_MemberId { get; set; }
        public int FK_InterestId { get; set; }
        public string? URL { get; set; }
    }
}
