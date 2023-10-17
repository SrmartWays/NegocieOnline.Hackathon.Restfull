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
                .Where(d => d.CustomerId == customerId)
                .ToListAsync();

            if (debts.Count > 0)
            {
                return debts;
            }
            else
            {
                return NotFound("Customer not found");
            }
        }
    }
}
