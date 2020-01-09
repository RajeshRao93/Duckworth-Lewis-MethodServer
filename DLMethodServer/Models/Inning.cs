using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DLMethodServer.Models
{
    // Request from UI for both innings 1 and innings 2
    public class Inning
    {
        // Overs available at the inning's start
        public float initialOvers_1 { get; set; }

        // List of all the interruptions during the inning
        public List<Interruption> interruptions_1 { get; set; }

        // Runs score at the end of innings
        public int runsScored_1 { get; set; }
        
        // Overs left at the time of termination
        public float oversLeftAtTermination_1 { get; set; }

        // Wickets lost at the time of termination
        public int wicketsLostAtTermination_1 { get; set; }

        // Overs available at the inning's start
        public float initialOvers_2 { get; set; }

        // List of all the interruptions during the inning
        public List<Interruption> interruptions_2 { get; set; }

        // Runs score at the end of innings
        public int runsScored_2 { get; set; }

        // Overs left at the time of termination
        public float oversLeftAtTermination_2 { get; set; }

        // Wickets lost at the time of termination
        public int wicketsLostAtTermination_2 { get; set; }
    }
}