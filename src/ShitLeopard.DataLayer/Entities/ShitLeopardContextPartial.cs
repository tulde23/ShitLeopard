using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ShitLeopard.DataLayer.Entities
{
    public partial class ShitLeopardContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _cs;

        public ShitLeopardContext(string cs)
        {
            _cs = cs;
        }

        public ShitLeopardContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        partial void OnConfiguringPartial(DbContextOptionsBuilder optionsBuilder)
        {
            var cs = _cs ?? _configuration["connectionString"];
            optionsBuilder.UseSqlServer(cs);
        }
    }
}