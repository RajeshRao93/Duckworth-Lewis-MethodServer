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

        // Converts overs to balls
        public int getBalls(double input)
        {

            int wholePart = (int)input;
            float floatValue;
            string decimal_places = "";
            double overs;
            string str1 = input.ToString();
            var regex = new System.Text.RegularExpressions.Regex("(?<=[\\.])[0-9]+");
            if (regex.IsMatch(str1))
            {
                decimal_places = regex.Match(str1).Value;
                floatValue = float.Parse(decimal_places[0].ToString(), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            }
            else
            {
                floatValue = 0;
            }

            overs = input;

            if (floatValue == 6)
            {
                wholePart++;
                overs = (int)input + 1;
                floatValue = 0;
                //System.err.println("incorrect cricket values Resetting values to" + this.input);
            }


            return (int)((wholePart * 6) + (floatValue * 1));

        }
    }
}