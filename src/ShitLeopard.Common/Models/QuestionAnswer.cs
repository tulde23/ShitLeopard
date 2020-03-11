using ShitLeopard.Api.Models;

namespace ShitLeopard.Common.Models
{
    public class QuestionAnswer
    {
        public Question Question { get; set; }
        public dynamic Answer { get; set; }

        public bool Match { get; set; }

        public string Comment { get; set; }
        public bool IsArray { get; set; }
    }
}