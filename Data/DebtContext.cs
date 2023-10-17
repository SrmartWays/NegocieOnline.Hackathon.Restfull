using Microsoft.EntityFrameworkCore;
using NegocieOnline.Hackathon.Restfull.Models;

public class DebtContext : DbContext
{
    public DebtContext(DbContextOptions<DebtContext> options) : base(options) { }

    public DbSet<Debt> Debts { get; set; }

}
