using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DLMethodServer.Models
{
    public class MatchData
    {
        //columns of the data files.    
        public string mid { get; set; }
        public string date { get; set; }
        public string venue { get; set; }
        public string bat_team { get; set; }
        public string bowl_team { get; set; }
        public string batsman { get; set; }
        public string bowler { get; set; }
        public string runs { get; set; }
        public string wickets { get; set; }
        public string overs { get; set; }
        public string runs_last_5 { get; set; }
        public string wickets_last_5 { get; set; }
        public string striker { get; set; }
        public string non_striker { get; set; }
        public string total { get; set; }
    }
}