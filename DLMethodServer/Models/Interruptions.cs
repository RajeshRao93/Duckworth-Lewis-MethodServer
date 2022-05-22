using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DLMethodServer.Controllers
{
    //Interruption due to any circumstances in a match.
    public class Interruptions
    {
        // Balls left WRT initially available overs
        public int ballsLeft { get; set; }

        // Overs reduced due to the interruption
        public float oversLost { get; set; }

        // Wickets lost so far
        public int wicketsLost { get; set; }

    }
}
