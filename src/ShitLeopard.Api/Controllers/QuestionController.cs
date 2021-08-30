using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Controllers
{
    public class QuestionController : ServiceController<ISearchService>
    {
        public QuestionController(ILoggerFactory loggerFactory, ISearchService service) : base(loggerFactory, service)
        {
        }

        //    [InboundRequestInspectorFilter]
        [HttpPost]
        public async Task<QuestionAnswer> Ask([FromBody] Question question)
        {
            if (string.IsNullOrEmpty(question?.Text) || question.Text.Length > 255)
            {
                return new QuestionAnswer();
            }
            return await Service.AskQuestionAsync(question);
        }
    }
}