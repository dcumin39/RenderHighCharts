
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
            var btyes = serivce.RequestGraph("image/png", chart: new HighCharts()

            {
                title = new HighChartsTitle() { text = "Hello World" },
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
                        categories = new List<string> { "Jan", "Feb" }

                    },
                    series = new List<HighChartsSeries>{ new HighChartsSeries()
                  {
                      name="Test",
                      type = "column",
                      data = new List<double> {29.9,5 }
                  },

                    new HighChartsSeries()
                  {
                        name = "Test2",
                      type = "column",
                      data = new List<double> {2.9,52 }
                  }}

                },
                type = "image/png",
                constr = "Chart",
                callback = @"function(chart) { 
                            chart.setTitle({text:'test'});
                            }"
            });
            var bytesCount = btyes.Length;
            return File(btyes.Take(bytesCount - 600).ToArray(), "image.png");
        }


    }
}