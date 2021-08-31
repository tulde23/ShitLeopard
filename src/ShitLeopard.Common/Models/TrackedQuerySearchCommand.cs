namespace ShitLeopard.Common.Models
{
    public class TrackedQuerySearchCommand
    {
        public int PageSize { get; set; } = 50;
        public int PageNumber { get; set; } = 0;
        public string Pattern { get; set; }
    }
}