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
        private string _port;
        private string _ip;
        private JsonSerializerSettings _jsonSerializerSettings;
        public string TemporaryImagesDirectory { get; set; }
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

        private Process ExeProcess { get; set; }
        public List<string> CreatedTempFiles { get; set; }
         
        public HighChartsRenderServer(string ip = "127.0.0.1", string port = "3003",
            string scriptsDirectory = "scripts", string temporaryFolder= null)
        {
            InitServerSerializerAndTmpFileList(ip, port);

            TemporaryImagesDirectory = temporaryFolder ?? Path.GetTempPath();

            var phantomDirectory = HttpContext.Current.Server.MapPath("~/App_Data/phantomjs/");
            var pathRooth = Path.GetPathRoot(phantomDirectory);
            var phantomJsDirectory = Path.Combine(AssemblyDirectory,  "phantomjs");

            var highChartsConvertJsFile = Path.Combine(phantomJsDirectory, "highcharts-convert.js");

            string arguments =
                $" -host {_ip} -port {port}";

            InitializeCommandProcess();

            using (StreamWriter sw = ExeProcess.StandardInput)
            {
                if (!sw.BaseStream.CanWrite) return;
                sw.WriteLine($"cd \"{phantomDirectory}\"");
                sw.WriteLine($"{pathRooth.Replace("\\", "")}");
                string format = $"phantomjs.exe \"{highChartsConvertJsFile}\" {arguments}";
                sw.WriteLine(format);
            }

        }

        private void InitializeCommandProcess()
        {
            ExeProcess = new Process();
            var info = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Verb = "runas"
            };
            ExeProcess.StartInfo = info;

            ExeProcess.Start();
        }

        private void InitServerSerializerAndTmpFileList(string ip, string port)
        {
            _jsonSerializerSettings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            _port = port;
            _ip = ip;
            CreatedTempFiles = new List<string>();
        }

        public byte[] ProcessHighChartsRequest(HighCharts chart)
        {
            var newGuid = Guid.NewGuid();

            var temporaryGraphImageFile = Path.Combine(TemporaryImagesDirectory, $"{newGuid}.png");

            var wrapper = new HighChartsRenderServerWrapper()
            {
                infile = JsonConvert.SerializeObject(chart.options,
                Formatting.Indented,
                _jsonSerializerSettings),
                constr = "Chart",
                outfile = temporaryGraphImageFile,
                callback = chart.callback

            };

            var request = PostToPhantomJs(wrapper);

            return ReturnBytesOnSuccess(request, temporaryGraphImageFile);
        }

        private byte[] ReturnBytesOnSuccess(HttpWebRequest request, string temporaryGraphImageFile)
        {
            using (var response = (HttpWebResponse) request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var bytesToReturn = File.ReadAllBytes(temporaryGraphImageFile);
                    CreatedTempFiles.Add(temporaryGraphImageFile);
                    return bytesToReturn;
                }
            }

            return null;
        }

        private HttpWebRequest PostToPhantomJs(HighChartsRenderServerWrapper wrapper)
        {
            var request = (HttpWebRequest) WebRequest.Create($"http://{_ip}:{_port}");

            request.Method = "POST";

            var postData = JsonConvert.SerializeObject(wrapper, _jsonSerializerSettings);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }

        public void Dispose()
        {
            foreach (var createdTempFile in CreatedTempFiles)
            {
                File.Delete(createdTempFile);
            }
            if (!ExeProcess.HasExited)
            {

                ExeProcess.Kill();
            }
        }

       
    }
}

