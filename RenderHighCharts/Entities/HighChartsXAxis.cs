using System.Collections.Generic;

namespace RenderHighCharts.Entities
{
    /// <summary>
    ///  //http://api.highcharts.com/highstock#xAxis.labels
    /// </summary>
    public class HighChartsAxisLabels
    {
        public string align { get; set; } = "center";

        public string format { get; set; } = "{value}";
        /// <summary>
        /// 
        /// Callback JavaScript function to format the label. The value is given by this.value. Additional properties for this are axis, chart, isFirst and isLast. Defaults to:
        ////function()
        ///{
            //return this.value;
        ///}
        /// </summary>
        public string formatter { get; set; }

        public string autoRotation { get; set; } = "[-45]";

        public string rotation { get; set; }

        public HighChartsStyle style { get; set; } = new HighChartsStyle();

        public  int? y { get; set; }

        public int? x { get; set; } 


    }

    public class HighChartsStyle
    {

        public string color { get; set; } = "#6D869F";

        public string fontWeight { get; set; } = "bold";


    }
    /// <summary>
    /// http://api.highcharts.com/highstock#xAxis
    /// </summary>
    public abstract class HighChartsAxis
    {
        public HighChartsTitle title { get; set; }

        public virtual HighChartsAxisLabels labels { get; set; }
        public string alternateGridColor { get; set; }

        public string lineColor { get; set; }

        public string gridLineColor { get; set; }

        public string gridLineDashStyle { get; set; }

        public virtual int gridLineWidth { get; set; }

        public string minorGridLineColor { get; set; }

        public string minorGridLineDashStyle { get; set; }

        public string minorGridLineWidth { get; set; }

        public string minorTickPosition { get; set; } = "outside";

        public string minorTickColor { get; set; }

        public int minorTickLength { get; set; }

        public int minorTickWidth { get; set; }

        public string tickColor { get; set; }

        public int tickLength { get; set; }

        public int tickWidth { get; set; }

        public string TickPosition { get; set; } = "outside";

        public string Type { get; set; }

    }


    public class HighChartsXAxis : HighChartsAxis
    {
        public IEnumerable<string> categories { get; set; }
        public override int gridLineWidth { get; set; }

        public override HighChartsAxisLabels labels { get; set; }= new HighChartsAxisLabels() {x = 0};
    }

    public class HighChartsYAxis : HighChartsAxis
    {
            public override int gridLineWidth { get; set; } = 1;
        public override HighChartsAxisLabels labels { get; set; } = new HighChartsAxisLabels() { x = 0, y = -2 };
    }

    public class HighChartsTitle
    {
        public string text { get; set; }
    }
}
