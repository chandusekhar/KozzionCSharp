using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KozzionNetwork.HTTPServer
{
    public class ResponseGeneratorStaticString :IResponseGenerator
    { 
        private string response_string;
        private byte[] response_bytes;

        public ResponseGeneratorStaticString(string response_string)
        {
            this.response_string = response_string;
            this.response_bytes = Encoding.UTF8.GetBytes(response_string);
        }

        public void Generate(HttpListenerContext context)
        {
            context.Response.ContentLength64 = response_bytes.Length;
            context.Response.OutputStream.Write(response_bytes, 0, response_bytes.Length);
        }
    }
}
