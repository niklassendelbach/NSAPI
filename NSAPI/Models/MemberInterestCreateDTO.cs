using System.ComponentModel.DataAnnotations.Schema;

namespace NSAPI.Models
{
    public class MemberInterestCreateDTO
    {
        public int FK_MemberId { get; set; }
        public int FK_InterestId { get; set; }
        public string? URL { get; set; }
    }
}
