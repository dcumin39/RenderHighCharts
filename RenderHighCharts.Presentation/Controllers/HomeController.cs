
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RenderHighCharts.Constants;
using RenderHighCharts.Entities;
using RenderHighCharts.Services;


namespace RenderHighCharts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HighChartsRequestService serivce = new HighChartsRequestService();
            var bytes = serivce.RequestGraph("image/png", chart: new HighCharts( "User Engagement")

            {
        
                async = false,
                content = "options",
                options = new HighChartsOptions()
                {
                   
                    yAxis = new HighChartsYAxis()
                    {
                        title = new HighChartsTitle() { text = "Number of Occurences" }
                    },
                    xAxis = new HighChartsXAxis()
                    {
                        categories = new List<string> { "Week of 7/24/2015", "Week of 7/31/2015", "Week of 8/04/2015" }

                    },
                    series = new List<HighChartsSeries>{ new HighChartsSeries()
                  {
                      name="Applause",
                      type = HighChartType.Spline,
                      data = new List<double> {44.9,33 ,3},
                      color="#CCCCCC"
                  },

                    new HighChartsSeries()
                  {
                        name = "Comments",
                      type =HighChartType.Spline,
                      data = new List<double> {2.9,52,10 },
                      color="#748c9c"
                  }
                    ,

                    new HighChartsSeries()
                  {
                        name = "Logins",
                      type =HighChartType.Spline,
                      data = new List<double> {27,4,22 },
                      color="#748c9c"
                  }
                    }


                },
                type = "image/png"
            
     
            });

            return File(bytes, "image.png");
        }


    }
}