using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NSAPI.Models;
using NSAPI.Repository.IRepository;
using NSApp.Models;
using System.Runtime.CompilerServices;

namespace NSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberInterestController : Controller
    {
        private readonly IRepository<MemberInterest> _context;
        private readonly IMapper _mapper;
        public MemberInterestController(IRepository<MemberInterest> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //GET
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MemberInterest>>> GetInterest(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var member = await _context.GetAllAsync(m => m.FK_MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }
        //CREATE/POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MemberInterestDTO>> AddInterest([FromBody] MemberInterestCreateDTO member)
        {
            if (await _context.GetAsync(i => i.URL.ToUpper() == member.URL.ToUpper()) != null)
            {
                ModelState.AddModelError("Custom error", "The URL already exists");
                return BadRequest(ModelState);
            }
            if (member == null)
            {
                return BadRequest(member);
            }
            MemberInterest model = _mapper.Map<MemberInterest>(member);
            await _context.CreateAsync(model);

            return CreatedAtAction(nameof(GetInterest), new { id = model.MemberInterestId }, model);
        }
        //PATCH
        [HttpPatch("{id:int}")] //lägga till så det blir specifik person också?
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddURLPartial(int id, JsonPatchDocument<MemberInterestUpdateDTO> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var interest = await _context.GetAsync(ap => ap.MemberInterestId == id);
            MemberInterestUpdateDTO memberUpdate = _mapper.Map<MemberInterestUpdateDTO>(interest); //mapper
            if (interest == null)
            {
                return BadRequest();
            }
            patchDto.ApplyTo(memberUpdate, ModelState);
            MemberInterest model = _mapper.Map<MemberInterest>(memberUpdate);
            await _context.UpdateAsync(model);
            return NoContent();
        }
    }
}
