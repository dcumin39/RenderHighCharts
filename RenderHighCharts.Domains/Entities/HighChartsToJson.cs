using System;
using System.Collections.Specialized;
using System.Web;
using Newtonsoft.Json;

namespace RenderHighCharts.Domain.Entities
{
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