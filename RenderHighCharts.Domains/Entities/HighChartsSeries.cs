using System.Collections;
using System.Security.AccessControl;

namespace RenderHighCharts.Domain.Entities
{
    public class HighChartsSeries
    {
        public string type { get; set; }
        public IEnumerable data { get; set; }

        public HighChartsDataGrouping dataGrouping { get; set; }
        public string name { get; set; }
    }
}