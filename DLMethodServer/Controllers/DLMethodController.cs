using DLMethodServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DLMethodServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/dlmethod")]
    public class DLMethodController : ApiController
    {
        private static Interruptions interrption = new Interruptions();

        private Inning1 in1 = new Inning1();

        private Inning2 in2 = new Inning2();

        [Route("gettargetscore")]
        [HttpPost]
        // API to get the target score due to any interruption
        public IHttpActionResult GetTargerScore([FromBody]List<Inning> request)
        {  
            CaluclateResources(request);

            ////*********************Target score calculation**************************////
            if (in2.resourceR2 > in1.resourceR1)
            {
                float target = in1.scoredRuns + 245 * (in2.resourceR2 - in1.resourceR1) / 100 + 1;
                in2.targetScore = (int)target;
            }
            else if (in2.resourceR2 < in1.resourceR1)
            {
                float target = in1.scoredRuns * (in2.resourceR2 / in1.resourceR1) + 1;
                in2.targetScore = (int)target;
            }
            else
            {
                in2.targetScore = in1.scoredRuns + 1;
            }

            return Ok(new Response() { value = in2.targetScore.ToString() });            
        }

        [Route("getparscore")]
        [HttpPost]
        // API to get the Par score in case of match termination in between
        public IHttpActionResult GetParScore([FromBody]List<Inning> request)
        {                                      
            int parscore = CalculateParScore(request);
            return Ok(new Response() { value = parscore.ToString() });
        }

        //Calculating the resources of both innings
        //Can be made generic! I am just too lazy for that now. If its working don't touch it!!
        public void CaluclateResources(List<Inning> request)
        {
            //********************Inning 1*********************************
            bool isInning1Interrupted = false;
            in1.oversAtStartN = request[0].initialOvers_1;
            in1.resourceAtStart = PercentileConversion.getPercentile(interrption.getBalls(in1.oversAtStartN), 0);
            for (int i = 0; i < request[0].interruptions_1.Count; i++)
            {
                interrption.ballsLeft = interrption.getBalls(request[0].interruptions_1[i].oversLeft);
                interrption.wicketsLost = request[0].interruptions_1[i].wicketsLost;
                interrption.oversLost = request[0].interruptions_1[i].oversLost;

                if (interrption.oversLost != 0)
                {
                    in1.resourceAtSuspension = PercentileConversion.getPercentile(interrption.ballsLeft, interrption.wicketsLost);
                    int ballsRemaining = interrption.ballsLeft - interrption.getBalls(interrption.oversLost);
                    in1.resourceAtResumption = PercentileConversion.getPercentile(ballsRemaining, interrption.wicketsLost);
                }

               // in1.resourceAtStart = PercentileConversion.getPercentile(interrption.getBalls(in1.oversAtStartN), 0);
                in1.resourceR1 = in1.resourceAtStart - (in1.resourceAtSuspension - in1.resourceAtResumption);
                in1.resourceAtStart = in1.resourceR1;
                isInning1Interrupted = true;
            }

            in1.resourceR1 = isInning1Interrupted == true ? in1.resourceR1 : in1.resourceAtStart;
            in1.scoredRuns = request[0].runsScored_1;

            //********************Inning 2*********************************
            bool isInning2Interrupted = false;
            in2.ballsAtStartN = interrption.getBalls(request[1].initialOvers_2);
            in2.resourceAtStart = PercentileConversion.getPercentile(in2.ballsAtStartN, 0);

            for (int j = 0; j < request[1].interruptions_2.Count; j++)
            {
                interrption.ballsLeft = interrption.getBalls(request[1].interruptions_2[j].oversLeft);
                interrption.wicketsLost = request[1].interruptions_2[j].wicketsLost; ;
                interrption.oversLost = request[1].interruptions_2[j].oversLost;

                if (interrption.oversLost != 0)
                {
                    in2.resourceAtSuspension = PercentileConversion.getPercentile(interrption.ballsLeft, interrption.wicketsLost);
                    int ballsRemaining = interrption.ballsLeft - interrption.getBalls(interrption.oversLost);
                    in2.resourceAtResumption = PercentileConversion.getPercentile(ballsRemaining, interrption.wicketsLost);
                }

                in2.resourceR2 = in2.resourceAtStart - (in2.resourceAtSuspension - in2.resourceAtResumption);
                in2.resourceAtStart = in2.resourceR2;
                isInning2Interrupted = true;
            }

            in2.resourceR2 = isInning2Interrupted == true ? in2.resourceR2 : in2.resourceAtStart;
        }

        //Calculating the par score of team batting second
        public int CalculateParScore(List<Inning> request)
        {
            CaluclateResources(request);
            //Console.WriteLine("Enter the overs left for Team2 and wickets lost so far in the order");
            int ballsLeft = interrption.getBalls(request[1].oversLeftAtTermination_2);
            int wicketLost = request[1].wicketsLostAtTermination_2;
            float resourceRemaining = PercentileConversion.getPercentile(ballsLeft, wicketLost);
            float resourcesUsed = in2.resourceR2 - resourceRemaining;
            int parScore = 0;
            if (resourcesUsed > in1.resourceR1)
            {
                float target = in1.scoredRuns + 245 * (resourcesUsed - in1.resourceR1) / 100;
                parScore = (int)target;
            }
            else if (resourcesUsed < in1.resourceR1)
            {
                float target = in1.scoredRuns * (resourcesUsed / in1.resourceR1);
                parScore = (int)target;
            }

            return parScore;
        }
    }  
    
    public class Response
    {
        public string value { get; set; }
    }  
}
