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
    public class PlanController : ControllerBase
    {
        private readonly SMTDbContext _context;

        public PlanController(SMTDbContext context)
        {
            _context = context;
        }

        // GET: api/Plan
        [HttpGet("{LineID}/{ProductID}")]
        public async Task<ActionResult<PlanInfo>> GetCurrentPlan(int LineID, int ProductID)
        {
            var Plan = await _context.Plans.Where(x => x.LineInfoID == LineID && x.ProductID == ProductID && x.IsComplete == false).FirstOrDefaultAsync();
            return Plan;
        }

        [HttpPost]
        public async Task<ActionResult<PlanInfo>> CreatePlan(PlanInfo planInfo)
        {
            var CreatedPlans = await _context.Plans.Where(x => x.LineInfoID == planInfo.LineInfoID && x.IsComplete == false).ToListAsync();
            foreach (PlanInfo Plan in CreatedPlans)
            {
                if (Plan.ProductID == planInfo.ProductID)
                {
                    return BadRequest("Plan already created");
                }
                else
                {
                    Plan.FinishedTime = DateTime.Now;
                    Plan.IsComplete = true;
                    _context.Entry(Plan).State = EntityState.Modified;
                }
            }
            _context.Plans.Add(planInfo);
            await _context.SaveChangesAsync();
            return Ok("Plan created successfully");
        }
        [HttpPut("{LineID}")]
        public async Task<ActionResult<PlanInfo>> FinishPlan(int LineID)
        {
            try
            {
                var CreatedPlans = await _context.Plans.Where(x => x.LineInfoID == LineID && x.IsComplete == false).ToListAsync();
                foreach (PlanInfo Plan in CreatedPlans)
                {

                    Plan.FinishedTime = DateTime.Now;
                    Plan.IsComplete = true;
                    _context.Entry(Plan).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Plan Finished");
        }
        //
        [HttpGet]
        public async Task<ActionResult<List<PlanInfo>>> GetRunningPlans()
        {
            //var RunningPlans = await _context.LineInfos.Include(x => x.PlanInfos)
            //    .Select(p => new LineInfo()
            //    {
            //        LineInfoID = p.LineInfoID,
            //        CurrentPlan = new PlanInfo()
            //        {
            //            PlanInfoID = p.PlanInfos.Where(x => x.IsComplete == false).FirstOrDefault().PlanInfoID,
            //            Orders = p.Orders.Where(x => x.IsConfirmed == false).ToList()
            //        },
            //        Name = p.Name
            //    }
            //    ).ToListAsync();
            var RunningPlans = await _context.LineInfos.Include(x => x.PlanInfos).ThenInclude(x => x.Product).Include(x => x.Orders).
                Select(p => new LineInfo
                {
                    LineInfoID = p.LineInfoID,
                    Name = p.Name,
                    CurrentProduct = p.PlanInfos.Where(x => x.IsComplete == false).FirstOrDefault().Product,
                    CurrentPlan = p.PlanInfos.Where(x => x.IsComplete == false).FirstOrDefault(),
                    Orders = p.Orders.Where(x => x.IsConfirmed == false).ToList(),
                })
               .ToListAsync();
            return Ok(RunningPlans);
        }

        // GET: api/Plan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanInfo>> GetPlanInfo(int id)
        {
            var planInfo = await _context.Plans.FindAsync(id);

            if (planInfo == null)
            {
                return NotFound();
            }
            return planInfo;
        }

        // PUT: api/Plan/5
        [HttpPut("{LineId}")]
        public async Task<IActionResult> PutPlanInfo(int LineId, PlanInfo planInfo)
        {
            if (LineId != planInfo.LineInfoID)
            {
                return BadRequest();
            }
            _context.Entry(planInfo).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanInfoExists(planInfo.PlanInfoID))
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

        // POST: api/Plan
        [HttpPost]
        public async Task<ActionResult<PlanInfo>> PostPlanInfo(PlanInfo planInfo)
        {
            _context.Plans.Add(planInfo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPlanInfo", new { id = planInfo.PlanInfoID }, planInfo);
        }

        // DELETE: api/Plan/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlanInfo>> DeletePlanInfo(int id)
        {
            var planInfo = await _context.Plans.FindAsync(id);
            if (planInfo == null)
            {
                return NotFound();
            }

            _context.Plans.Remove(planInfo);
            await _context.SaveChangesAsync();

            return planInfo;
        }

        private bool PlanInfoExists(int id)
        {
            return _context.Plans.Any(e => e.PlanInfoID == id);
        }
    }
}
