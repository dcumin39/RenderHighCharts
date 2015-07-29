
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RenderHighCharts.Domain.Entities;
using RenderHighCharts.Domain.Services;


namespace RenderHighCharts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HighChartsRequestService serivce = new HighChartsRequestService();
            var bytes = serivce.RequestGraph("image/png", chart: new HighCharts( "Hello World")

            {
        
                async = false,
                content = "options",
                options = new HighChartsOptions()
                {
                   
                    yAxis = new HighChartsYAxis()
                    {
                        title = new HighChartsTitle() { text = "test title" }
                    },
                    xAxis = new HighChartsXAxis()
                    {
                        categories = new List<string> { "Logins", "Applause" }

                    },
                    series = new List<HighChartsSeries>{ new HighChartsSeries()
                  {
                      name="Last Week",
                      type = "column",
                      data = new List<double> {29.9,5 },
                      color="#364852"
                  },

                    new HighChartsSeries()
                  {
                        name = "This Week",
                      type = "column",
                      data = new List<double> {2.9,52 },
                      color="#748c9c"
                  }}

                },
                type = "image/png",
                constr = "Chart"
                
     
            });
            
            return File(bytes, "image.png");
        }


    }
}