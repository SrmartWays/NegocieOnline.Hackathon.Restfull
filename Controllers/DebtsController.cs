using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegocieOnline.Hackathon.Restfull.Models;

namespace NegocieOnline.Hackathon.Restfull.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtsController : ControllerBase
    {
        private readonly DebtContext _context;

        public DebtsController(DebtContext context)
        {
            _context = context;
        }

        [HttpGet("getDebts")]
        public async Task<ActionResult<IEnumerable<Debt>>> GetDebts(string customerId)
        {
            var debts = await _context.Debts
                .Where(d => d.CustomerId == customerId && !d.isPaid) 
                .ToListAsync();

            if (debts.Count > 0)
            {
                return debts;
            }
            else
            {
                return NotFound("Customer not found or no unpaid debts");
            }
        }

        [HttpPost("addDebt")]
        public async Task<ActionResult<Debt>> AddDebt(Debt debt)
        {
            _context.Debts.Add(debt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDebts), new { customerId = debt.CustomerId }, debt);
        }
    }
}
