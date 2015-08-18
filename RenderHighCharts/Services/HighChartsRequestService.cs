using System;
using System.IO;
using System.Net;
using System.Text;
using RenderHighCharts.Entities;

namespace RenderHighCharts.Services
{
    public class HighChartsRequestService
    {
        public int MaxBytesToRead { get; set; } = 500000;
        public string HighChartsRequestUrl { get; set; } = "http://export.highcharts.com";

        public byte[] RequestGraph(string format, HighCharts chart)
        {
            var postData = chart.GetSerializedData();



            HttpWebRequest request =
            (HttpWebRequest)WebRequest.Create(HighChartsRequestUrl);

            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Method = "POST";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {

                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {


                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Get the stream associated with the response.
                    using (Stream receiveStream = response.GetResponseStream())
                    {

                        return ReadBytesFromResponse(receiveStream);
                    }

                }
            }
            catch (WebException ex)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append($"Status:{ex.Status}");
                if (ex.Response != null)
                {
                    // can use ex.Response.Status, .StatusDescription
                    if (ex.Response.ContentLength != 0)
                    {
                        using (var stream = ex.Response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream))
                            {
                               builder.Append($"\n\n Response: {reader.ReadToEnd()}");
                            }
                        }
                    }
                }
                throw new Exception(builder.ToString(),ex);
            }

        }

        private byte[] ReadBytesFromResponse(Stream receiveStream)
        {
            byte[] imageBytes;
            using (BinaryReader br = new BinaryReader(receiveStream))
            {

                imageBytes = br.ReadBytes(MaxBytesToRead);
                br.Close();
            }
            return imageBytes;
        }
    }
}
