using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DLMethodServer.Models
{
    // Data related to Inning 2 of Team B
    public class Inning2
    {
        public int ballsAtStartN { get; set; }

        public int targetScore { get; set; }

        public float resourceAtStart { get; set; }

        public float resourceAtSuspension { get; set; }

        public float resourceAtResumption { get; set; }

        public float resourceR2 { get; set; }
    }
}