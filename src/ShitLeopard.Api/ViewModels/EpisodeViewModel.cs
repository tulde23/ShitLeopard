using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.ViewModels
{
    public class EpisodeViewModel
    {
        public List<ShowModel> Shows { get; set; }
        public List<EpisodeModel> Episodes { get; set; }
    }
}
