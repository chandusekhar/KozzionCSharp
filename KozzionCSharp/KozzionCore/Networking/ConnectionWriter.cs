using KozzionCore.Concurrency;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.Networking
{
    public class ConnectionWriter : ATaskCycling
    {
        private ConcurrentQueue<Message> message_queue;
        private BinaryWriter socket_writer;

        public ConnectionWriter(BinaryWriter socket_writer)
        {
            this.message_queue = new ConcurrentQueue<Message>();
            this.socket_writer = socket_writer;
        }

        public void SendMessage(Message message)
        {
            message_queue.Enqueue(message);
        }

        protected override void DoTask()
        {
            Message message;
            if (message_queue.TryDequeue(out message))
            {
                socket_writer.WriteEnum(message.MessageType);
                socket_writer.WriteByteArray1D(message.Payload);
            }
        }
    }
}
