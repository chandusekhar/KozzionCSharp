using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KozzionCore.Concurrency
{
    public abstract class ATaskCycling
    {
        private bool running;
        private Thread thread;
        protected ATaskCycling()
        {
            running = false;
        }

        public void Start()
        {
            thread = new Thread(StartCycle);
            thread.Start();       
        }

        public void StartCycle()
        {
            running = true;
            while (running)
            {
                DoTask();
            }
        }

        public void Stop()
        {
            running = false;
        }

        protected abstract void DoTask();
    }
}
