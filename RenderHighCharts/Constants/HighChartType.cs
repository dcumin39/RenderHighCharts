using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderHighCharts.Constants
{
    public sealed class HighChartType
    {

        public const string Default = "";
        public const string Line = "line";
        public const string Spline = "spline";
        public const string Area = "area";
        public const string AreaSpline = "areaspline";
        public const string Column = "column";
        public const string Bar = "bar";
        public const string Pie = "pie";
        public const string Scatter = "scatter";

    }

    public sealed class HighChartsExportFormat
    {
        public const string png = "image/png";
        public const string jpeg = "image/jpeg";
        public const string svg = "image/svg+xml";
        public const string pdf = "application/pdf";
    }
}
