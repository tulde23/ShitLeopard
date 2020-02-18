namespace ShitLeopard.Api.Models
{
    public class ScriptWordModel
    {
        public long Id { get; set; }
        public long ScriptLineId { get; set; }
        public string Word { get; set; }

        public virtual ScriptLineModel ScriptLine { get; set; }
    }
}