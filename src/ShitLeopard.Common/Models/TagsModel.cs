namespace ShitLeopard.Api.Models
{
    public class TagsModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public long Frequency { get; set; }
    }
}