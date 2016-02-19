using System.Collections.Generic;
using Newtonsoft.Json;
using RenderHighCharts.Constants;

namespace RenderHighCharts.Entities
{
    public class HighChartsOptions
    {
        private HighChartChart _chart = new HighChartChart();

        public HighChartsOptions(string defaultSeriesType) 
        {
          _chart = new HighChartChart(defaultSeriesType) {marginLeft = 55};
            
        }


        public  HighChartsPlotOptions plotOptions { get; set; }


        /// <summary>
        /// http://api.highcharts.com/highstock#xAxis
        /// </summary>
        public HighChartsXAxis xAxis { get; set; }

        /// <summary>
        /// http://api.highcharts.com/highstock#yAxis
        /// </summary>
        public HighChartsYAxis yAxis { get; set; }


        /// <summary>
        /// http://api.highcharts.com/highstock#series
        /// </summary>
        public List<HighChartsSeries> series { get; set; }

        public HighChartsCredits credits { get; set; } = new HighChartsCredits();


        public HighChartChart chart
        {
            get { return _chart; }
            set { _chart = value; }
        }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class HighChartChart
    {
        public HighChartChart()
        {
            
        }
        public HighChartChart(string DefaultSeriesType):this()
        {
            defaultSeriesType = DefaultSeriesType;
        }
        public string backgroundColor { get; set; } = "transparent";
        public string borderColor { get; set; }

        public string borderRadius { get; set; }

        public string borderWidth { get; set; }

        public string defaultSeriesType { get; set; } = HighChartType.Line;
        public int marginRight { get; set; }
        public int marginLeft { get; set; }
    }
}