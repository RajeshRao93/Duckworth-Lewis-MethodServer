using DLMethodServer.Models;
using System;
using System.IO;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DLMethodServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/uploaddata")]
    public class UploadDataController : ApiController
    {
     
        [Route("uploadodidata")]
        [HttpPost]
        public IHttpActionResult UploadOdiData([FromBody]MatchData request)
        {
            try
            {                
                string filePath = @"F:\Masters\Winter\ARPB\DLMethodServer\DLMethodServer\Data\odi.csv";
                
                var csv = new StringBuilder();
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                    request.mid, request.date, request.venue, request.bat_team, request.bowl_team, request.batsman, request.bowler,
                    request.runs, request.wickets, request.overs, request.runs_last_5, request.wickets_last_5, request.striker,
                    request.non_striker, request.total, Environment.NewLine);
                csv.Append(newLine);


                File.AppendAllText(filePath, csv.ToString());
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return Ok(new Response() { value = "Data added Successfully" });
        }

        [Route("uploadt20data")]
        [HttpPost]
        public IHttpActionResult Uploadt20Data([FromBody]MatchData request)
        {
            try
            {
                // uploading the file to local system.
                string filePath = @"F:\Masters\Winter\ARPB\DLMethodServer\DLMethodServer\Data\t20.csv";               

                var csv = new StringBuilder();
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                    request.mid, request.date, request.venue, request.bat_team, request.bowl_team, request.batsman, request.bowler,
                    request.runs, request.wickets, request.overs, request.runs_last_5, request.wickets_last_5, request.striker,
                    request.non_striker, request.total, Environment.NewLine);
                csv.Append(newLine);


                File.AppendAllText(filePath, csv.ToString());               
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return Ok(new Response() { value = "Data added Successfully" });
        }
    }
}
