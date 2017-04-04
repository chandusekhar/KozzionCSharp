using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure
{
    public class Pool<T> where T : new()
    {
        private readonly Queue<T> unused;

        public Pool(Action<T> factory)
        {
            unused = new Queue<T>(); //TODO make concurrent
      
        }

        public T GetItem()
        {
            if (this.unused.Any())
            {
                return this.unused.Dequeue();
            }
            else
            {
                return new T();
            }                       
        }

        public void PoolItem(T item)
        {
            this.unused.Enqueue(item);
        }
    }
}
