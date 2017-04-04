using System.Collections.Generic;

namespace KozzionCore.Collections.PriorityQueue
{
    public interface IPriorityQueue<ElementType>
    {
        int Count { get; }

        void Enqueue(ElementType value);

        void EnqueueAll(List<ElementType> values);

        ElementType DequeueFirst();

        ElementType DequeueLast();

        ElementType PeekFirst();

        ElementType PeekLast();

 
    }
}
