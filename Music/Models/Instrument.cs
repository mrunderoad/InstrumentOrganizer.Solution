using System.Collections.Generic;

namespace Music.Models
{
    public class Instrument
    {
        public Instrument()
        {
          this.JoinEntities = new HashSet<InstrumentItem>();
        }

        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<InstrumentItem> JoinEntities { get; set; }
    }
}