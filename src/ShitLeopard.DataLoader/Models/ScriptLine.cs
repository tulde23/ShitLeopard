namespace ShitLeopard.DataLoader.Models
{
    public partial class ScriptLine
    {
        public ScriptLine()
        {
        }

        public long Id { get; set; }
        public string Body { get; set; }
        public long ScriptId { get; set; }
        public long? CharacterId { get; set; }

        public string Start { get; set; }
        public string End { get; set; }

        public int Offset { get; set; }
    }
}