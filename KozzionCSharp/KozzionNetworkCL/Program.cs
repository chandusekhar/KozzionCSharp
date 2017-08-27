using KozzionNetwork;
using KozzionNetwork.HTTPServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KozzionNetworkCL
{
    public class Program
    {
        static void Main(string[] args)
        {
            //string response_string = string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
            //ResponseGeneratorStaticString reponse = new ResponseGeneratorStaticString(response_string);
            string file_path = @"D:\Projects\PartileDraw\particle_paths_0.html";
            ResponseGeneratorFile reponse = new ResponseGeneratorFile(file_path);
            Server server = new Server("http://localhost:8080/render/", reponse);
            server.Run();
            Console.WriteLine("Running");
            Console.ReadLine();
            server.Stop();
        }
    }
}
