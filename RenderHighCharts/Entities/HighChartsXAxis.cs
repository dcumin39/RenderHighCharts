using System.Collections.Generic;

namespace RenderHighCharts.Entities
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".


    public class HighChartsXAxis
    {
        public HighChartsTitle title { get; set; }
        public IEnumerable<string> categories { get; set; }
    }

    public class HighChartsYAxis
    {
        public HighChartsTitle title { get; set; }
    }

    public class HighChartsTitle
    {
        public string text { get; set; }
    }
}
