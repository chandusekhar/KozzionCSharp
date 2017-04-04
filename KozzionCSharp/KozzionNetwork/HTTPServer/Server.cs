using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;

namespace KozzionNetwork.HTTPServer
{ 
    public class Server
    {
        private readonly HttpListener listener = new HttpListener();
        private readonly IResponseGenerator ResponseGenerator;

        public Server(string[] prefixes, IResponseGenerator response_generator)
        {
            if (!HttpListener.IsSupported)
            {
                throw new Exception("Needs Windows XP SP2, Server 2003 or later.");
            }

            // URI prefixes are required, for example 
            // "http://localhost:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
            {
                throw new ArgumentException("prefixes");
            }

            // A response generator is required
            if (response_generator == null)
            {
                throw new ArgumentException("method");
            }

            foreach (string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }

            ResponseGenerator = response_generator;
            listener.Start();
        }

        public Server(string prefix, IResponseGenerator response_generator)
            :this(new string []{prefix}, response_generator)
        {
        }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                Console.WriteLine("Webserver running...");
                try
                {
                    while (listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((state) =>
                        {

                            HttpListenerContext context = (HttpListenerContext)state;
                            try
                            {
                                ResponseGenerator.Generate(context);
                            }
                            catch
                            {
                                //TODO log any exceptions
                            } 
                            finally
                            {
                                context.Response.OutputStream.Close();
                            }
                        }, listener.GetContext());
                    }
                }
                catch
                {
                    // suppress any exceptions
                }
            });
        }

        public void Stop()
        {
            listener.Stop();
            listener.Close();
        }
    }
}
