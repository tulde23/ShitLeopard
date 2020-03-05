using System;
using System.Collections.Generic;
using System.Text;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Common.Models
{
    public class QuestionAnswer
    {
        public Question Question { get; set; }
        public dynamic Answer { get; set; }
    }
}
