using System.Collections.Concurrent;
using System.Numerics;
using KozzionCryptography.multiparty;

public class ChannelTwoWayTerminalBigInteger
    : IChannel
{
    private BlockingCollection<BigInteger> queue_send;
    private BlockingCollection<BigInteger> queue_recieve;

    public ChannelTwoWayTerminalBigInteger(
        BlockingCollection<BigInteger> queue_send,
        BlockingCollection<BigInteger> queue_recieve)
    {
        this.queue_send = queue_send;
        this.queue_recieve = queue_recieve;
    }

    public BigInteger Recieve()
    {   
        return queue_recieve.Take();
    }

    public void Send(
        BigInteger value)
    {
        queue_send.Add(value);
    }
}
