using System.Collections.Concurrent;
using System.Numerics;

namespace KozzionCryptography.multiparty
{

    public class ChannelTwoWayBigInteger
    {
        public IChannel Channel0 { get; private set; }
        public IChannel Channel1 { get; private set; }

        public ChannelTwoWayBigInteger()
        {
            BlockingCollection<BigInteger> queue_0 = new BlockingCollection<BigInteger>();
            BlockingCollection<BigInteger> queue_1 = new BlockingCollection<BigInteger>();
            Channel0 = new ChannelTwoWayTerminalBigInteger(queue_0, queue_1);
            Channel1 = new ChannelTwoWayTerminalBigInteger(queue_1, queue_0);
        }

    }
}