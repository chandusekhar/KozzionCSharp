using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.Networking
{
    public class Connection
    {
        private ConnectionReader reader;
        private ConnectionWriter writer;

        public Connection(TcpClient client, IMessageHandler handler)
        {
            reader = new ConnectionReader(new BinaryReader(client.GetStream()), handler);
            writer = new ConnectionWriter(new BinaryWriter(client.GetStream()));
        }

        public void Start()
        {
            reader.Start();
            writer.Start();
        }

        public void Stop()
        {
            reader.Stop();
            writer.Stop();
        }

        public void SendMessage(Message message)
        {
            writer.SendMessage(message);
        }
    }
}
