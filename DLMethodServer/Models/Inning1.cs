using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DLMethodServer.Models
{
    // Data related to Inning 1 of Team A
    public class Inning1
    {
        public float oversAtStartN { get; set; }

        public float resourceAtStart { get; set; }

        public float resourceAtSuspension { get; set; }

        public float resourceAtResumption { get; set; }

        public float resourceR1 { get; set; }

        public int scoredRuns { get; set; }
    }
}