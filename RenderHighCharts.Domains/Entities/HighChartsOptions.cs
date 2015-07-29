using System.Collections.Generic;

namespace RenderHighCharts.Domain.Entities
{
    public class HighChartsOptions
    {
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
     

    }
}