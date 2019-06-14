using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models.ViewModels
{
    public class GetDateViewModel
    {
        public DateTime currentdate { get; set; }

        public GetDateViewModel()
        {
            currentdate = DateTime.Now;
        }

    }
}
