
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NLog;
using RenderHighCharts.Constants;
using RenderHighCharts.Entities;
using RenderHighCharts.Services;


namespace RenderHighCharts.Controllers
{
    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            //HighChartsRequestService service = new HighChartsRequestService();
            //
            //logger.Debug(Newtonsoft.Json.JsonConvert.SerializeObject(highChartsData));
            //var bytes = service.RequestGraph("image/png", highChartsData);

            //return File(bytes, "image.png");

            using (HighChartsRenderServer server= new HighChartsRenderServer())
            {
                var highChartsData = GetUserEngagementHighChartsData();
                var response1 = server.ProcessHighChartsRequest(highChartsData);
                var response2 = server.ProcessHighChartsRequest(highChartsData);
                var response3 = server.ProcessHighChartsRequest(highChartsData);
                var response4 = server.ProcessHighChartsRequest(highChartsData);
                var response5 = server.ProcessHighChartsRequest(highChartsData);
                var response6 = server.ProcessHighChartsRequest(highChartsData);
                var response7 = server.ProcessHighChartsRequest(highChartsData);
                var response8 = server.ProcessHighChartsRequest(highChartsData);
                var response9 = server.ProcessHighChartsRequest(highChartsData);
                var response10 = server.ProcessHighChartsRequest(highChartsData);
                var response11 = server.ProcessHighChartsRequest(highChartsData);
                var response12 = server.ProcessHighChartsRequest(highChartsData);
                var response13 = server.ProcessHighChartsRequest(highChartsData);
                var response14 = server.ProcessHighChartsRequest(highChartsData);
                var response15 = server.ProcessHighChartsRequest(highChartsData);
                var response16 = server.ProcessHighChartsRequest(highChartsData);
                var response17 = server.ProcessHighChartsRequest(highChartsData);
                var response18 = server.ProcessHighChartsRequest(highChartsData);
                var response19 = server.ProcessHighChartsRequest(highChartsData);
                var response20 = server.ProcessHighChartsRequest(highChartsData);
                var response21 = server.ProcessHighChartsRequest(highChartsData);
                var response22 = server.ProcessHighChartsRequest(highChartsData);
                var response23 = server.ProcessHighChartsRequest(highChartsData);
                var response24 = server.ProcessHighChartsRequest(highChartsData);
                var response25 = server.ProcessHighChartsRequest(highChartsData);
                var response26 = server.ProcessHighChartsRequest(highChartsData);
                var response27 = server.ProcessHighChartsRequest(highChartsData);
                var response28 = server.ProcessHighChartsRequest(highChartsData);
                var response29 = server.ProcessHighChartsRequest(highChartsData);
                return File(response1, "image.png");
            }
        }

        private static HighCharts GetUserEngagementHighChartsData()
        {

            var highChartsStyle = new HighChartsStyle() { color = "#000000", fontWeight = "normal" };

       
            return new HighCharts("User Engagement")
            {

                async = false,
                content = "options",
                title = new HighChartsTitle() { text = "" },

                options = new HighChartsOptions(HighChartType.Spline)
                {

                   
                    xAxis = new HighChartsXAxis()
                    {
                        gridLineWidth = 0,
                        categories = new List<string> {   "Week of 7/25/2015",
                                                            "Week of 8/1/2015",
                                                            "Week of 8/8/2015",
                                                            "Week of 8/15/2015"},
                        labels = new HighChartsAxisLabels()
                        {
                            align = "center",
                            format = "{value}",
                            formatter = null,
                            autoRotation = "[-45]",
                            rotation = null,
                            style = highChartsStyle,
                            y = null,
                            x = null

                        }


                    },
                    yAxis = new HighChartsYAxis()
                    {

                        gridLineWidth = 1,
                        labels = new HighChartsAxisLabels()
                        {
                            align = "center",
                            format = "{value}",
                            formatter = null,
                            autoRotation = "[-45]",
                            rotation = null,
                            style = highChartsStyle,
                            y = null,
                            x = null

                        },
                        title = new HighChartsTitle() { text = "" },
                        gridLineColor = "#8C8C8C",

                    },
                    series = new List<HighChartsSeries>{ new HighChartsSeries()
                    {
                        name="Logins",
                        data = new List<int> {   24,
               30,
               22,
               30},
                        color="#2D5F91",type=""
                    },
                    

                      
                    },credits = new HighChartsCredits() { enabled = false}
                    

                },
                type = HighChartsExportFormat.png,width = 500,scale=null,constr = null,
                callback = @"function(chart) {                        chart.setTitle({text:''});                            };"


            };
        }
    }
}