using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Music.Models
{
  public class MusicContext : DbContext
  {
    public DbSet<Item> Items { get; set; }
    public DbSet<Instrument> Instruments { get; set; }
    public DbSet<InstrumentItem> InstrumentItem { get; set; }
    public MusicContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbCOntextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}