using System;
using System.Collections.Generic;
using KozzionCore.Collections.PriorityQueue;

namespace KozzionCore.Collections
{
    public class PriorityQueueS1<ElementType> : IPriorityQueue<ElementType>
    {
        public int Count { get { return inner.Count; } }

        PriorityQueueBase<ElementType, ElementType> inner;    

        public PriorityQueueS1(IComparer<ElementType> comparer)
        {
            inner = new PriorityQueueBase<ElementType, ElementType>(0, comparer);
        }

        public void Enqueue(ElementType value)
        {
            inner.Enqueue(value, value);
        }

        public void EnqueueAll(List<ElementType> values)
        {
            foreach (ElementType value in values)
            {
                inner.Enqueue(value, value);
            }
        }

        public ElementType DequeueFirst()
        {
            return inner.DequeueValue();
        }

        public ElementType DequeueLast()
        {
            throw new NotImplementedException();
        }

        public ElementType PeekFirst()
        {
            return inner.PeekValue();
        }

        public ElementType PeekLast()
        {
            throw new NotImplementedException();
        }



  
   
    }
}
