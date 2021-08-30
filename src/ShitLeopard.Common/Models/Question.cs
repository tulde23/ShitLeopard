using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShitLeopard.Api.Models
{
    public class Question
    {
        [StringLength(255)]
        public string Text { get; set; }

        public bool IsFuzzy { get; set; }

        public int Limit { get; set; } = 500;

        public bool IsPhrase()
        {
            return !string.IsNullOrEmpty(Text) && (Text.Contains("\"") || Text.Contains(" "));
        }

        public bool IsWord()
        {
            return !IsPhrase() && !IsNumber() && !IsGuid();
        }

        public bool IsNumber()
        {
            return int.TryParse(Text, out var i);
        }

        public bool IsGuid()
        {
            return Guid.TryParse(Text, out var g);
        }

        public bool ContainsWildcards()
        {
            return Text.Contains("*");
        }

        public string FormatText()
        {
            if (!string.IsNullOrEmpty(Text))
            {
                var sb = new StringBuilder(Text);
                sb.Replace("\"", string.Empty);
                if (this.IsPhrase())
                {
                    sb.Insert(0, "\"");
                    sb.Append("\"");
                }
                return sb.ToString();
            }
            return Text;
        }
    }
}