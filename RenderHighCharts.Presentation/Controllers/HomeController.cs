
using System;
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
            var highChartsData = GetUserEngagementHighChartsData();
            var format = highChartsData.options.toJson();   //you can plug this into export.higcharts.com to see what it really should look like.
            Console.WriteLine( format);
            using (HighChartsRenderServer server = new HighChartsRenderServer())
            {
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
                        categories = new List<string> {   "1",
                                                            "2",
                                                            "3",
                                                            "4"},
                        labels = new HighChartsAxisLabels()
                        {
               
                            style = highChartsStyle
                   

                        }


                    },
                    yAxis = new HighChartsYAxis()
                    {

                        gridLineWidth = 1,
                        labels = new HighChartsAxisLabels()
                        {
                            align = "right",
                    
                      
                            style = highChartsStyle
                   

                        },
                        title = new HighChartsTitle() { text = "test" },
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