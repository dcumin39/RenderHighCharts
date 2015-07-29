using System.Collections.Generic;

namespace RenderHighCharts.Domain.Entities
{
    public class HighChartsDataGrouping
    {
        public string approximation { get; set; }

        public HighChartsDateFormatting dateTimeLabelFormats { get; set; }

        public bool enabled { get; set; } = true;

        public bool forced { get; set; } = false;

        public int groupPixelWidth { get; set; } = 2;

        public bool smoothed { get; set; } = false;

        public Dictionary<string, List<object>> units { get; set; }
            =
            new Dictionary<string, List<object>>()
            {
                { "millisecond",new List<object> {new List<int> { 1, 2, 5, 10, 20, 25, 50, 100, 200, 500 } }},
                { "second",new List<object> {new List<int> { 1, 2, 5, 10, 15, 30 } }},
                { "minute",new List<object> {new List<int> { 1, 2, 5, 10, 15, 30 } }},
                { "hour",new List<object> {new List<int> { 1, 2, 3, 4, 6, 8, 12 } }},
                { "day",new List<object> {new List<int> { 1 } }},
                { "week",new List<object> {new List<int> { 1 } }},
                { "month",new List<object> {new List<int> { 1,3,6 } }},
                { "year",null}
            };

    }
}