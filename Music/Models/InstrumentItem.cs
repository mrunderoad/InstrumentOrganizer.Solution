namespace Music.Models
{
  public class InstrumentItem
  {       
    public int InstrumentItemId { get; set; }
    public int ItemId { get; set; }
    public int InstrumentId { get; set; }
    public virtual Item Item { get; set; }
    public virtual Instrument Instrument { get; set; }
  }
}