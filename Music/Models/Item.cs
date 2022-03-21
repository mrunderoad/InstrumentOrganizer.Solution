using System.Collections.Generic;

namespace Music.Models
{
  public class Item
  {
    public Item()
    {
        this.JoinEntities = new HashSet<InstrumentItem>();
    }

    public int ItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Search { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<InstrumentItem> JoinEntities { get;}
  }
}