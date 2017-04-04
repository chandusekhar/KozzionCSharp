using KozzionCore.Concurrency;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.Networking
{
    public class ConnectionReader : ATaskCycling
    {
        private BinaryReader socket_reader;
        private IMessageHandler handler;

        public ConnectionReader(BinaryReader socket_reader, IMessageHandler handler)
        {
            this.socket_reader = socket_reader;
            this.handler = handler;
        }

        protected override void DoTask()
        {
            MessageType message_type = socket_reader.ReadEnum<MessageType>();
            byte[] payload = socket_reader.ReadByteArray1D(); 
            Message message = new Message(message_type, payload);
            handler.Handle(message);
        }
    }
}
