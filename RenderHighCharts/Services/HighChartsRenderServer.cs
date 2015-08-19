using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using RenderHighCharts.Entities;

namespace RenderHighCharts.Services
{
    internal class HighChartsRenderServerWrapper
    {
        public string infile { get; set; }
        public string constr { get; set; }
        public string outfile { get; set; }

        public string callback { get; set; }
    }

    /// <summary>
    /// Needed to run phantomjs highcharts-convert.js -host 127.0.0.1 -port 3003
    /// https://github.com/highslide-software/highcharts.com/blob/master/exporting-server/phantomjs/highcharts-convert.js
    /// </summary>
    public class HighChartsRenderServer : IDisposable
    {
        private readonly string _port;
        private readonly string IP;
        private readonly string PhantomDirectory;
        private readonly string PhantomJSDirectory;
        private readonly string PathRooth;
        private readonly string highChartsConvertJSFile;
        private JsonSerializerSettings _jsonSerializerSettings;

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private Process exeProcess { get; set; }
        public List<string> CreatedTempFiles { get; set; } 
        public HighChartsRenderServer(string ip = "127.0.0.1", string port = "3003",
            string scriptsDirectory = "scripts")
        {
            CreatedTempFiles = new List<string>();
            _jsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            PhantomDirectory = AssemblyDirectory;
            PathRooth = Path.GetPathRoot(PhantomDirectory);
            PhantomJSDirectory = Path.Combine(HttpContext.Current.Server.MapPath($"~/{scriptsDirectory}"),
                "phantomjs");

            _port = port;
            IP = ip;
            var fileName = "phantomjs.exe";
            highChartsConvertJSFile = Path.Combine(PhantomJSDirectory, "highcharts-convert.js");
            string arguments =
                $" -host {IP} -port {port}";
            exeProcess = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.Verb = "runas";
            exeProcess.StartInfo = info;

            exeProcess.Start();
            using (StreamWriter sw = exeProcess.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine($"cd \"{PhantomDirectory}\"");
                    sw.WriteLine($"{PathRooth.Replace("\\", "")}");
                    string format = $"{fileName} \"{highChartsConvertJSFile}\" {arguments}";
                    sw.WriteLine(format);

                }
            }

            //string output;
            //using (System.IO.StreamReader myOutput = exeProcess.StandardOutput)
            //{
            //    output = myOutput.ReadToEnd();
            //}
            //Console.WriteLine(output);
        }

        public byte[] ProcessHighChartsRequest(HighCharts chart)
        {
            Guid newGuid = Guid.NewGuid();

            string outfile = $"C://Temp//{newGuid}.png";
            HighChartsRenderServerWrapper wrapper = new HighChartsRenderServerWrapper()
            {
                infile = JsonConvert.SerializeObject(chart.options,
                Formatting.Indented,
                _jsonSerializerSettings),
                constr = "Chart",
                outfile = outfile,
              //  callback = chart.callback

            };
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create($"http://localhost:{_port}");
            request.Method = "POST";
            var postData = JsonConvert.SerializeObject(wrapper, _jsonSerializerSettings);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {

                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var bytesToReturn = File.ReadAllBytes(outfile);
                    CreatedTempFiles.Add(outfile);
                    
                    return bytesToReturn;
                }
            }

            return null;
    



        }

        public void Dispose()
        {
            foreach (var createdTempFile in CreatedTempFiles)
            {
                File.Delete(createdTempFile);
            }
            if (!exeProcess.HasExited)
            {

                exeProcess.Kill();
            }
        }

       
    }
}

