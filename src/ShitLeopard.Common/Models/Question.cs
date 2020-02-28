using System.ComponentModel.DataAnnotations;

namespace ShitLeopard.Api.Models
{
    public class Question
    {
        [StringLength(255)]
        public string Text { get; set; }
    }
}