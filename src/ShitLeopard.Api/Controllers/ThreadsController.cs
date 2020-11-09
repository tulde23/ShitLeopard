using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Controllers
{
    public class ThreadsController : BaseController
    {
        public ThreadsController(ILogger<BaseController> logger, ShitLeopardContext shitLeopardContext) : base(logger, shitLeopardContext)
        {
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(TestResult))]
        public async Task<TestResult> RunTest()
        {
            var ids = Enumerable.Range(0, 10000).Select(x => Guid.NewGuid().ToString());

            var sb = new StringBuilder();
            int counter = 0;
            using (var queue = new BufferredQueue<string>(new BufferredQueueOptions
            {
                BufferSize = 50,
                Expiration = 1
            }, (data) =>
            {
                counter = counter + data.Count();
                sb.AppendLine($"{data.Count()}");
                return Task.CompletedTask;
            }))
            {
                foreach (var item in ids)
                {
                    await queue.SendAsync(item);
                }
            }

            var result = new TestResult
            {
                Posted = counter
            };
            try
            {
                var sm = new SemaphoreSlim(1);
                await sm.WaitAsync();
                sm.Release();
            }
            catch (Exception ex)
            {
                result.Sempahore = $"{ex.Message}";
            }

            try
            {
                EventWaitHandle waitHandle = new EventWaitHandle(true, EventResetMode.AutoReset, "TestResultsDatabase");
                waitHandle.WaitOne();
                waitHandle.Set();
            }
            catch (Exception ex)
            {
                result.WaitHandle = $"{ex.Message}";
            }
            return result;
        }
    }

    public class TestResult
    {
        public int Posted { get; set; }
        public string Sempahore { get; set; }

        public string WaitHandle { get; set; }
    }
}