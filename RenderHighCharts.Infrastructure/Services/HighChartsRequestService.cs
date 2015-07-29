using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenderHighCharts.Infrastructure.Services
{
    public class HighChartsRequestService
    {

        public Object RequestGraph(string format)
        {
            WebRequest request = WebRequest.Create("http://www.contoso.com/");
        }
    }
}
