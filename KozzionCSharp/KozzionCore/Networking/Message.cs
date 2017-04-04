using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.Networking
{
    public class Message
    {
        public MessageType MessageType { get; private set; }
        public byte [] Payload { get; private set; }

        public Message(MessageType message_type, byte[] payload)//TODO copy?
        {
            this.MessageType = message_type;
            this.Payload = payload;
        }

   
    }
}
