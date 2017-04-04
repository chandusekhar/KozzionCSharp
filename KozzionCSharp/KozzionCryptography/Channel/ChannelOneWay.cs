using System.Collections.Concurrent;
using System.Numerics;
using KozzionCryptography.multiparty;
using System;


public class ChannelOneWay : IChannel, IDisposable
{
    private BlockingCollection<BigInteger> d_queue;

    public ChannelOneWay()
    {
        d_queue = new BlockingCollection<BigInteger>();
    }

    public BigInteger Recieve()
    {
        return d_queue.Take();     
    }

    public void Send(
        BigInteger big_integer)
    {
        d_queue.Add(big_integer);
    }


    //Disposable
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
    //Disposable
    protected virtual void Dispose(bool disposing)
    {
        if (disposing && this.d_queue != null)
        {
            this.d_queue.Dispose();
        }
    }
}
