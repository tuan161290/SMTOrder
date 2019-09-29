using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WiSolSMTRepo;
using WiSolSMTRepo.Model;

namespace WiSolSMTRepo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LineInfosController : ControllerBase
    {
        private readonly SMTDbContext _context;

        public LineInfosController(SMTDbContext context)
        {
            _context = context;
        }

        // GET: api/LineInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineInfo>>> GetLineInfos()
        {
            return await _context.LineInfos.ToListAsync();
        }

        // GET: api/LineInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineInfo>> GetLineInfo(int id)
        {
            var lineInfo = await _context.LineInfos.FindAsync(id);

            if (lineInfo == null)
            {
                return NotFound();
            }

            return lineInfo;
        }

        // PUT: api/LineInfoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLineInfo(int id, LineInfo lineInfo)
        {
            if (id != lineInfo.LineInfoID)
            {
                return BadRequest();
            }

            _context.Entry(lineInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LineInfoes
        [HttpPost]
        public async Task<ActionResult<LineInfo>> PostLineInfo(LineInfo lineInfo)
        {
            _context.LineInfos.Add(lineInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLineInfo", new { id = lineInfo.LineInfoID }, lineInfo);
        }

        // DELETE: api/LineInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LineInfo>> DeleteLineInfo(int id)
        {
            var lineInfo = await _context.LineInfos.FindAsync(id);
            if (lineInfo == null)
            {
                return NotFound();
            }

            _context.LineInfos.Remove(lineInfo);
            await _context.SaveChangesAsync();

            return lineInfo;
        }

        private bool LineInfoExists(int id)
        {
            return _context.LineInfos.Any(e => e.LineInfoID == id);
        }
    }
}
