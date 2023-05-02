using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSApp.Models
{
    public class MemberInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberInterestId { get; set; }
        [ForeignKey("Members")]
        public int FK_MemberId { get; set; }
        [ForeignKey("Interests")]
        public int FK_InterestId { get; set; }
        public string? URL { get; set; }
        public virtual Member? Members { get; set; } //nav
        public virtual Interest? Interests { get; set; } //nav
    }
}
