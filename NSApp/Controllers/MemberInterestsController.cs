using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NSApp.Data;
using NSApp.Models;

namespace NSApp.Controllers
{
    public class MemberInterestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberInterestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MemberInterests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MemberInterests.Include(m => m.Interests).Include(m => m.Members);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MemberInterests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MemberInterests == null)
            {
                return NotFound();
            }

            var memberInterest = await _context.MemberInterests
                .Include(m => m.Interests)
                .Include(m => m.Members)
                .FirstOrDefaultAsync(m => m.MemberInterestId == id);
            if (memberInterest == null)
            {
                return NotFound();
            }

            return View(memberInterest);
        }

        public List<ListItem> GetList()
        {
            var db = _context;
            return db.Members.Select(m => new ListItem { ID = m.MemberId, Text = m.FirstName + " " + m.LastName }).ToList();
        }
        // GET: MemberInterests/Create
        public IActionResult Create()
        {
            ViewData["FK_InterestId"] = new SelectList(_context.Interests, "InterestId", "Title");
            ViewBag.FK_MemberId = new SelectList(GetList(), "ID", "Text");
            return View();
        }

        // POST: MemberInterests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberInterestId,FK_MemberId,FK_InterestId,URL")] MemberInterest memberInterest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberInterest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_InterestId"] = new SelectList(_context.Interests, "InterestId", "Title", memberInterest.FK_InterestId);
            ViewData["FK_MemberId"] = new SelectList(_context.Members, "MemberId", "FirstName", memberInterest.FK_MemberId);
            return View(memberInterest);
        }

        // GET: MemberInterests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MemberInterests == null)
            {
                return NotFound();
            }

            var memberInterest = await _context.MemberInterests.FindAsync(id);
            if (memberInterest == null)
            {
                return NotFound();
            }
            ViewData["FK_InterestId"] = new SelectList(_context.Interests, "InterestId", "Title", memberInterest.FK_InterestId);
            ViewData["FK_MemberId"] = new SelectList(_context.Members, "MemberId", "FirstName", memberInterest.FK_MemberId);
            return View(memberInterest);
        }

        // POST: MemberInterests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberInterestId,FK_MemberId,FK_InterestId,URL")] MemberInterest memberInterest)
        {
            if (id != memberInterest.MemberInterestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberInterest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberInterestExists(memberInterest.MemberInterestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_InterestId"] = new SelectList(_context.Interests, "InterestId", "Title", memberInterest.FK_InterestId);
            ViewData["FK_MemberId"] = new SelectList(_context.Members, "MemberId", "FirstName", memberInterest.FK_MemberId);
            return View(memberInterest);
        }

        // GET: MemberInterests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MemberInterests == null)
            {
                return NotFound();
            }

            var memberInterest = await _context.MemberInterests
                .Include(m => m.Interests)
                .Include(m => m.Members)
                .FirstOrDefaultAsync(m => m.MemberInterestId == id);
            if (memberInterest == null)
            {
                return NotFound();
            }

            return View(memberInterest);
        }

        // POST: MemberInterests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberInterests == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MemberInterests'  is null.");
            }
            var memberInterest = await _context.MemberInterests.FindAsync(id);
            if (memberInterest != null)
            {
                _context.MemberInterests.Remove(memberInterest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberInterestExists(int id)
        {
          return (_context.MemberInterests?.Any(e => e.MemberInterestId == id)).GetValueOrDefault();
        }
    }
}
