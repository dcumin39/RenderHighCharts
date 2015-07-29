using System.IO;
using System.Net;
using RenderHighCharts.Entities;

namespace RenderHighCharts.Services
{
    public  class HighChartsRequestService
    {
        public  int MaxBytesToRead { get; set; }= 500000;
        public string HighChartsRequestUrl { get; set; } = "http://export.highcharts.com";

        public  byte[] RequestGraph(string format, HighCharts chart)
        {
            var postData = chart.GetSerializedData();



            HttpWebRequest request =
            (HttpWebRequest) WebRequest.Create(HighChartsRequestUrl);

            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Method = "POST";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
 
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();
            }


            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                // Get the stream associated with the response.
                using (Stream receiveStream = response.GetResponseStream())
                {

                    return ReadBytesFromResponse(receiveStream);
                }

            }
        }

        private  byte[] ReadBytesFromResponse(Stream receiveStream)
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
