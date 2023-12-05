using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegocieOnline.Hackathon.Restfull.Models;

namespace NegocieOnline.Hackathon.Restfull.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealsController : ControllerBase
    {
        private readonly DebtContext _context;
        private readonly ILogger<DealsController> _logger;

        public DealsController(DebtContext context, ILogger<DealsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("setDeal")]
        public async Task<ActionResult> SetDeal(string customerId, bool isPaid)
        {
            Debt debt = null;

            try
            {
                debt = await _context.Debts
                    .FirstOrDefaultAsync(d => d.CustomerId == customerId);

                if (debt != null)
                {
                    debt.isPaid = isPaid;
                    await _context.SaveChangesAsync();

                    var updatedDebt = await _context.Debts
                        .AsNoTracking()
                        .FirstOrDefaultAsync(d => d.CustomerId == customerId && d.isPaid == isPaid);

                    if (updatedDebt != null)
                    {
                        return Ok("Deal status updated successfully");
                    }
                    else
                    {
                        return StatusCode(500, "Failed to update deal status");
                    }
                }
                else
                {
                    return NotFound("Debt not found for the specified customerId");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Concurrency exception: {ex.Message}");

                if (debt != null)
                {
                    var entry = _context.Entry(debt);
                    if (entry.State == EntityState.Detached)
                    {
                        entry = _context.Attach(debt);
                    }
                    await entry.ReloadAsync();

                    debt.isPaid = isPaid;
                    await _context.SaveChangesAsync();

                    var updatedDebt = await _context.Debts
                        .AsNoTracking()
                        .FirstOrDefaultAsync(d => d.CustomerId == customerId && d.isPaid == isPaid);

                    if (updatedDebt != null)
                    {
                        return Ok("Deal status updated successfully");
                    }
                    else
                    {
                        return StatusCode(500, "Failed to update deal status on retry");
                    }
                }
                else
                {
                    return NotFound("Debt not found for the specified customerId");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating deal status: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
