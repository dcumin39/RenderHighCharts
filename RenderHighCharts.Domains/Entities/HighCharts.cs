using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace RenderHighCharts.Domain.Entities
{


    public class HighCharts
    {
        /// <summary>
        /// Can be of true or false. Default is false.
        ///  When setting async to true a download link is returned to the client, 
        /// instead of an image. This download link can be reused for 30 seconds. 
        /// After that, the file will be deleted from the server. 
        /// </summary>
        public  bool async
        {
            get;
            set;
        } = false;
        public HighChartsTitle title { get; set; }
        public string content { get; set; }
        /// <summary>
        /// Use this parameter if you want to create a graph out of a Highcharts configuration. 
        /// </summary>
        public HighChartsOptions options { get; set; }

        /// <summary>
        /// The content-type of the file to output.
        ///  Can be one of 'image/png', 'image/jpeg', 'application/pdf', or 'image/svg+xml'.
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Set the exact pixel width of the exported image or pdf. 
        /// This overrides the -scale parameter. The maximum allowed width is 2000px
        /// </summary>
        public int ? width { get; set; }

        public int ? scale { get; set; }
        
        /// <summary>
        /// The constructor name. Can be one of 'Chart' or 'StockChart'. 
        /// This depends on whether you want to generate Highstock or basic Highcharts.
        ///  Only applicable when using this in combination with the options parameter.
        /// </summary>
        public string constr { get; set; }

        /// <summary>
        /// String containing a callback JavaScript. 
        /// The callback is a function which will be called in the constructor of Highcharts to be executed on chart load.
        /// All code of the callback must be enclosed by a function. 
        /// Only applicable when using this in combination with the options parameter. 
        /// Example of contents of the callback file:
        /// function(chart)
        /// { chart.renderer.arc(200, 150, 100, 50, -Math.PI, 0).attr({ fill: '#FCFFC5', stroke: 'black', 'stroke-width' : 1 }).add(); }
        /// </summary>
        public string callback { get; set; }

        
    }

    public static class HighChartsToJson
    {
        public static string GetSerializedData(this HighCharts highcharts)
        {


            NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);
            outgoingQueryString.Add("async", highcharts.async.ToString());
            outgoingQueryString.Add("content", highcharts.content);
            outgoingQueryString.Add("options", JsonConvert.SerializeObject(highcharts.options).Replace("\"","'"));
            outgoingQueryString.Add("type", highcharts.type);
            outgoingQueryString.Add("width", highcharts.width.ToString());
            outgoingQueryString.Add("scale", highcharts.scale.ToString());
            outgoingQueryString.Add("constr", highcharts.constr);
            outgoingQueryString.Add("callback", highcharts.callback);
            return outgoingQueryString.ToString();

        }
    }
}