namespace VotingData.Controllers
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using VotingData.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class VoteDataController : ControllerBase
    {
        private readonly VotingDBContext _context;

        public VoteDataController(VotingDBContext context)
        {
            _context = context;
        }

        // GET api/VoteData
        [HttpGet]
        public async Task<ActionResult<IList<Counts>>> Get()
        {
            return await _context.Counts.ToListAsync();
        }

        // PUT api/VoteData/name
        [HttpPut("{name}")]
        public async Task<IActionResult> Put(string name)
        {
            var candidate = await _context.Counts.FirstOrDefaultAsync(c => c.Candidate == name);
            if (candidate == null)
            {
                await _context.Counts.AddAsync(new Counts {
                    Candidate = name,
                    Count = 1
                });
            }
            else
            {
                candidate.Count++;
                _context.Entry(candidate).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/VoteData/name
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var candidate = await _context.Counts.FirstOrDefaultAsync(c => c.Candidate == name);
            if (candidate != null)
            {
                _context.Counts.Remove(candidate);
                await _context.SaveChangesAsync();
            }

            return new OkResult();
        }
    }
}
