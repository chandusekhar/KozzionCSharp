using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KozzionNetwork.HTTPServer
{
    public interface IResponseGenerator
    {
        void Generate(HttpListenerContext context);


    }
}
