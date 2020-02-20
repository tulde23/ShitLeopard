using Microsoft.EntityFrameworkCore;
using ShitLeopard.Common.Contracts;

namespace ShitLeopard.DataLayer.Entities
{
    public partial class ShitLeopardContext
    {
        private readonly IConnectionStringProvider _configuration;
        private readonly string _cs;

        public ShitLeopardContext(string cs)
        {
            _cs = cs;
        }

        public ShitLeopardContext(IConnectionStringProvider configuration)
        {
            _configuration = configuration;
        }

        partial void OnConfiguringPartial(DbContextOptionsBuilder optionsBuilder)
        {
            var cs = _configuration.GetConnectionString();
            optionsBuilder.UseSqlServer(cs);
        }
    }
}