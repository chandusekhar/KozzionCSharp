using System.Collections.Generic;
using C5;

namespace KozzionCore.Collections
{
    public class PriorityQueueC5<ElementType> : KozzionCore.Collections.PriorityQueue.IPriorityQueue<ElementType>
    {
        IntervalHeap<ElementType> inner;

        public PriorityQueueC5(IComparer<ElementType> comparer)
        {
            inner = new IntervalHeap<ElementType>(0, comparer);
        }

        public int Count { get { return inner.Count; } }

        public void Enqueue(ElementType value)
        {
            inner.Add(value);
        }

        public void EnqueueAll(List<ElementType> values)
        {
            inner.AddAll(values);
        }

        public ElementType DequeueFirst()
        {
            return inner.DeleteMin();
        }

        public ElementType DequeueLast()
        {
            return inner.DeleteMax();
        }

        public ElementType PeekFirst()
        {
            return inner.FindMin();
        }

        public ElementType PeekLast()
        {
            return inner.FindMax();
        }
    }
}
