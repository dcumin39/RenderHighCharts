
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
                var response = server.ProcessHighChartsRequest(highChartsData);
                return File(response, "image.png");
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
                type = HighChartsExportFormat.png,width = 500,scale=null,constr = null
            


            };
        }
    }
}