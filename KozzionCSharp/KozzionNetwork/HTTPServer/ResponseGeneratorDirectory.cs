using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KozzionNetwork.HTTPServer
{
    public class ResponseGeneratorDirectory : IResponseGenerator
    { 
        private string file_path;
        private string response_string;
        private byte [] response_bytes;

        public ResponseGeneratorDirectory(string file_path)
        {
            this.file_path = file_path;
            this.response_string = File.ReadAllText(file_path);
            this.response_bytes = Encoding.UTF8.GetBytes(response_string);
        }

        public void Generate(HttpListenerContext context)
        {
            context.Response.ContentLength64 = response_bytes.Length;
            context.Response.OutputStream.Write(response_bytes, 0, response_bytes.Length);
        }
    }
}
