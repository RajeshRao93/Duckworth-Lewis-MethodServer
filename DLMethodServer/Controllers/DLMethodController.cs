﻿using DLMethodServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

using DLMethodServer.Services;
using DLMethodServer.Models;

namespace DLMethodServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/dlmethod")]
    public class DLMethodController : ApiController
    {       
        [Route("gettargetscore")]
        [HttpPost]
        // API to get the target score due to any interruption
        public IHttpActionResult GetTargerScore([FromBody]List<Inning> request)
        {  
            int targetScore = Service.CaluclateResourcesAndTarget(request);

            return Ok(new Response() { value = targetScore.ToString() });            
        }

        [Route("getparscore")]
        [HttpPost]
        // API to get the Par score in case of match termination in between
        public IHttpActionResult GetParScore([FromBody]List<Inning> request)
        {                                      
            int parscore = Service.CalculateParScore(request);
            
            return Ok(new Response() { value = parscore.ToString() });
        }
    }      
}
