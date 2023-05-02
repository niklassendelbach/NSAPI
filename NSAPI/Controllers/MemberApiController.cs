using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NSAPI.Models;
using NSAPI.Repository.IRepository;
using NSApp.Models;

namespace NSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberApiController : Controller
    {
        private readonly IRepository<Member> _context;
        public MemberApiController(IRepository<Member> context)
        {
           _context = context;
        }
        //GET 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            IEnumerable<Member> memberList = await _context.GetAllAsync();
            return Ok(memberList);

        }
        //GET MED Spec ID
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var member = await _context.GetAsync(ap => ap.MemberId == id);
            if (member == null)
            {
                return NotFound(); //kod 404
            }
            return Ok(member);
        }
        
        
    }
}
