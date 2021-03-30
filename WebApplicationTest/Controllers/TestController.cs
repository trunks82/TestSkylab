using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataLayer;
using Microsoft.Extensions.DependencyInjection;
using DataLayer.Interfaces;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace WebApplicationTest.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class TestController : ControllerBase
    {



        private IServiceProvider serviceProvider;

        public IServiceProvider ServiceProvider { get => this.serviceProvider; set => this.serviceProvider = value; }



        public TestController( IServiceProvider serviceProvider)
        {

            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [Route("api/Acknowledge")]
        public IEnumerable<DesRecord> GetRecord(int offset , int count )
        {
            using (var serviceScope = this.serviceProvider.CreateScope())
            {
                IDataLayer datalayer = serviceScope.ServiceProvider.GetRequiredService<IDataLayer>();
                return datalayer.GetDesRecord(offset, count);
                
            }
        }
        [HttpGet]
        [Route("api/GetAggregateRecord")]
        public IEnumerable<ResultLine> GetAggregateRecord(string aggregationType, int aggregationFilter)
        {
            using (var serviceScope = this.serviceProvider.CreateScope())
            {
                IDataLayer datalayer = serviceScope.ServiceProvider.GetRequiredService<IDataLayer>();
                return datalayer.GetRecord(aggregationType, aggregationFilter);

            }
        }

        [HttpGet]
        [Route("api/GetCSVRecord")]
        public HttpResponseMessage GetCSVRecord()
        {
            using (var serviceScope = this.serviceProvider.CreateScope())
            {
                IDataLayer datalayer = serviceScope.ServiceProvider.GetRequiredService<IDataLayer>();
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(Utilities.ToCsv(",", datalayer.GetRecordAll()));
                writer.Flush();
                stream.Position = 0;
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Export.csv" };
                return result;

            }
        }
    }
}
