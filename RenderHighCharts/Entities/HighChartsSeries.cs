using System.Collections;

namespace RenderHighCharts.Entities
{
    public class HighChartsSeries
    {
        public string type { get; set; }
        public IEnumerable data { get; set; }

        public HighChartsDataGrouping dataGrouping { get; set; }
        public string name { get; set; }
        public string color { get; set; }
    }
}