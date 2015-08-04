
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
            HighChartsRequestService service = new HighChartsRequestService();
            var highChartsData = GetUserEngagementHighChartsData();
            var bytes = service.RequestGraph("image/png", highChartsData);

            return File(bytes, "image.png");
        }

        private static HighCharts GetUserEngagementHighChartsData()
        {
            var lightColor = "#748c9c";
            var grayColor = "#CCCCCC";

            return new HighCharts("User Engagement")
            {

                async = false,
                content = "options",
                options = new HighChartsOptions(HighChartType.Spline)
                {
                  
                    yAxis = new HighChartsYAxis()
                    {
                        title = new HighChartsTitle() { text = "Number of Occurences" },gridLineColor = grayColor,
                        labels = new HighChartsAxisLabels()
                        {
                            style = new HighChartsStyle() { color = lightColor, fontWeight = "normal"}
                        }

                    },
                    xAxis = new HighChartsXAxis()
                    {
                        categories = new List<string> { "Week of 7/24/2015", "Week of 7/31/2015", "Week of 8/04/2015" },
                        labels = new HighChartsAxisLabels()
                        {
                            style = new HighChartsStyle() { color = lightColor, fontWeight = "normal" }
                        }

                    },
                    series = new List<HighChartsSeries>{ new HighChartsSeries()
                    {
                        name="Applause",
                        data = new List<double> {44.9,33 ,3},
                        color=grayColor
                    },

                        new HighChartsSeries()
                        {
                            name = "Comments",
                            data = new List<double> {2.9,52,10 },
                            color=lightColor
                        }
                        ,

                        new HighChartsSeries()
                        {
                            name = "Logins",
                            data = new List<double> {27,4,22 },
                            color=lightColor
                        }
                    }

                },
                type = HighChartsExportFormat.png
               

            };
        }
    }
}