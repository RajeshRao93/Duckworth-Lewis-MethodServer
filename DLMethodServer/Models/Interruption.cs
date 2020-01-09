using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DLMethodServer.Models
{
    //Interruption in any innings
    //Enter all three values to be 0 if no interruption
    public class Interruption
    {        
        public float oversLeft { get; set; }

        public float oversLost { get; set; }

        public int wicketsLost { get; set; }
    }
}