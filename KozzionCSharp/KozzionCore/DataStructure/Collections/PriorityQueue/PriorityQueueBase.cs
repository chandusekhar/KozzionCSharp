using System;
using System.Collections;
using System.Collections.Generic;

namespace KozzionCore.Collections
{
    /// <summary>
    /// Priority queue based on binary heap,
    /// Elements with minimum priority dequeued first
    /// </summary>
    /// <typeparam name="TPriority">Type of priorities</typeparam>
    /// <typeparam name="TValue">Type of values</typeparam>
    public class PriorityQueueBase<TPriority, TValue> : ICollection<KeyValuePair<TPriority, TValue>>
    {
        private List<KeyValuePair<TPriority, TValue>> base_heap;
        private IComparer<TPriority> comparer;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of priority queue with default initial capacity and default priority comparer
        /// </summary>
        public PriorityQueueBase()
            : this(Comparer<TPriority>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of priority queue with specified initial capacity and default priority comparer
        /// </summary>
        /// <param name="capacity">initial capacity</param>
        public PriorityQueueBase(int capacity)
            : this(capacity, Comparer<TPriority>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of priority queue with specified initial capacity and specified priority comparer
        /// </summary>
        /// <param name="capacity">initial capacity</param>
        /// <param name="comparer">priority comparer</param>
        public PriorityQueueBase(int capacity, IComparer<TPriority> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException();
            }
            base_heap = new List<KeyValuePair<TPriority, TValue>>(capacity);
            this.comparer = comparer;
        }

        /// <summary>
        /// Initializes a new instance of priority queue with default initial capacity and specified priority comparer
        /// </summary>
        /// <param name="comparer">priority comparer</param>
        public PriorityQueueBase(IComparer<TPriority> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException();
            }
            base_heap = new List<KeyValuePair<TPriority, TValue>>();
            this.comparer = comparer;
        }

        /// <summary>
        /// Initializes a new instance of priority queue with specified data and default priority comparer
        /// </summary>
        /// <param name="data">data to be inserted into priority queue</param>
        public PriorityQueueBase(IEnumerable<KeyValuePair<TPriority, TValue>> data)
            : this(data, Comparer<TPriority>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of priority queue with specified data and specified priority comparer
        /// </summary>
        /// <param name="data">data to be inserted into priority queue</param>
        /// <param name="comparer">priority comparer</param>
        public PriorityQueueBase(IEnumerable<KeyValuePair<TPriority, TValue>> data, IComparer<TPriority> comparer)
        {
            if (data == null || comparer == null)
                throw new ArgumentNullException();

            this.comparer = comparer;
            base_heap = new List<KeyValuePair<TPriority, TValue>>(data);
            // heapify data
            for (int pos = base_heap.Count / 2 - 1; pos >= 0; pos--)
                HeapifyFromBeginningToEnd(pos);
        }

        #endregion

        #region Merging

        /// <summary>
        /// Merges two priority queues
        /// </summary>
        /// <param name="pq1">first priority queue</param>
        /// <param name="pq2">second priority queue</param>
        /// <returns>resultant priority queue</returns>
        /// <remarks>
        /// source priority queues must have equal comparers,
        /// otherwise <see cref="InvalidOperationException"/> will be thrown
        /// </remarks>
        public static PriorityQueueBase<TPriority, TValue> MergeQueues(PriorityQueueBase<TPriority, TValue> pq1, PriorityQueueBase<TPriority, TValue> pq2)
        {
            if (pq1 == null || pq2 == null)
                throw new ArgumentNullException();
            if (pq1.comparer != pq2.comparer)
                throw new InvalidOperationException("Priority queues to be merged must have equal comparers");
            return MergeQueues(pq1, pq2, pq1.comparer);
        }

        /// <summary>
        /// Merges two priority queues and sets specified comparer for resultant priority queue
        /// </summary>
        /// <param name="pq1">first priority queue</param>
        /// <param name="pq2">second priority queue</param>
        /// <param name="comparer">comparer for resultant priority queue</param>
        /// <returns>resultant priority queue</returns>
        public static PriorityQueueBase<TPriority, TValue> MergeQueues(PriorityQueueBase<TPriority, TValue> pq1, PriorityQueueBase<TPriority, TValue> pq2, IComparer<TPriority> comparer)
        {
            if (pq1 == null || pq2 == null || comparer == null)
                throw new ArgumentNullException();
            // merge data
            PriorityQueueBase<TPriority, TValue> result = new PriorityQueueBase<TPriority, TValue>(pq1.Count + pq2.Count, pq1.comparer);
            result.base_heap.AddRange(pq1.base_heap);
            result.base_heap.AddRange(pq2.base_heap);
            // heapify data
            for (int pos = result.base_heap.Count / 2 - 1; pos >= 0; pos--)
                result.HeapifyFromBeginningToEnd(pos);

            return result;
        }

        #endregion

        #region Priority queue operations

        /// <summary>
        /// Enqueues element into priority queue
        /// </summary>
        /// <param name="priority">element priority</param>
        /// <param name="value">element value</param>
        public void Enqueue(TPriority priority, TValue value)
        {
            Insert(priority, value);
        }

        /// <summary>
        /// Dequeues element with minimum priority and return its priority and value as <see cref="KeyValuePair{TPriority,TValue}"/> 
        /// </summary>
        /// <returns>priority and value of the dequeued element</returns>
        /// <remarks>
        /// Method throws <see cref="InvalidOperationException"/> if priority queue is empty
        /// </remarks>
        public KeyValuePair<TPriority, TValue> Dequeue()
        {
            if (!IsEmpty)
            {
                KeyValuePair<TPriority, TValue> result = base_heap[0];
                DeleteRoot();
                return result;
            }
            else
                throw new InvalidOperationException("Priority queue is empty");
        }

        /// <summary>
        /// Dequeues element with minimum priority and return its value
        /// </summary>
        /// <returns>value of the dequeued element</returns>
        /// <remarks>
        /// Method throws <see cref="InvalidOperationException"/> if priority queue is empty
        /// </remarks>
        public TValue DequeueValue()
        {
            return Dequeue().Value;
        }

        /// <summary>
        /// Returns priority and value of the element with minimun priority, without removing it from the queue
        /// </summary>
        /// <returns>priority and value of the element with minimum priority</returns>
        /// <remarks>
        /// Method throws <see cref="InvalidOperationException"/> if priority queue is empty
        /// </remarks>
        public KeyValuePair<TPriority, TValue> Peek()
        {
            if (!IsEmpty)
                return base_heap[0];
            else
                throw new InvalidOperationException("Priority queue is empty");
        }

        /// <summary>
        /// Returns value of the element with minimun priority, without removing it from the queue
        /// </summary>
        /// <returns>value of the element with minimum priority</returns>
        /// <remarks>
        /// Method throws <see cref="InvalidOperationException"/> if priority queue is empty
        /// </remarks>
        public TValue PeekValue()
        {
            return Peek().Value;
        }

        /// <summary>
        /// Gets whether priority queue is empty
        /// </summary>
        public bool IsEmpty
        {
            get { return base_heap.Count == 0; }
        }

        #endregion

        #region Heap operations

        private void ExchangeElements(int pos1, int pos2)
        {
            KeyValuePair<TPriority, TValue> val = base_heap[pos1];
            base_heap[pos1] = base_heap[pos2];
            base_heap[pos2] = val;
        }

        private void Insert(TPriority priority, TValue value)
        {
            KeyValuePair<TPriority, TValue> val = new KeyValuePair<TPriority, TValue>(priority, value);
            base_heap.Add(val);

            // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];

            // heapify after insert, from end to beginning
            HeapifyFromEndToBeginning(base_heap.Count - 1);
        }


        private int HeapifyFromEndToBeginning(int pos)
        {
            if (pos >= base_heap.Count) return -1;

            while (pos > 0)
            {
                int parentPos = (pos - 1) / 2;
                if (comparer.Compare(base_heap[parentPos].Key, base_heap[pos].Key) > 0)
                {
                    ExchangeElements(parentPos, pos);
                    pos = parentPos;
                }
                else break;
            }
            return pos;
        }


        private void DeleteRoot()
        {
            if (base_heap.Count <= 1)
            {
                base_heap.Clear();
                return;
            }

            base_heap[0] = base_heap[base_heap.Count - 1];
            base_heap.RemoveAt(base_heap.Count - 1);

            // heapify
            HeapifyFromBeginningToEnd(0);
        }

        private void HeapifyFromBeginningToEnd(int pos)
        {
            if (pos >= base_heap.Count) return;

            // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];

            while (true)
            {
                // on each iteration exchange element with its smallest child
                int smallest = pos;
                int left = 2 * pos + 1;
                int right = 2 * pos + 2;
                if (left < base_heap.Count && comparer.Compare(base_heap[smallest].Key, base_heap[left].Key) > 0)
                    smallest = left;
                if (right < base_heap.Count && comparer.Compare(base_heap[smallest].Key, base_heap[right].Key) > 0)
                    smallest = right;

                if (smallest != pos)
                {
                    ExchangeElements(smallest, pos);
                    pos = smallest;
                }
                else break;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TPriority, TValue>> implementation

        /// <summary>
        /// Enqueus element into priority queue
        /// </summary>
        /// <param name="item">element to add</param>
        public void Add(KeyValuePair<TPriority, TValue> item)
        {
            Enqueue(item.Key, item.Value);
        }

        /// <summary>
        /// Clears the collection
        /// </summary>
        public void Clear()
        {
            base_heap.Clear();
        }

        /// <summary>
        /// Determines whether the priority queue contains a specific element
        /// </summary>
        /// <param name="item">The object to locate in the priority queue</param>
        /// <returns><c>true</c> if item is found in the priority queue; otherwise, <c>false.</c> </returns>
        public bool Contains(KeyValuePair<TPriority, TValue> item)
        {
            return base_heap.Contains(item);
        }

        /// <summary>
        /// Gets number of elements in the priority queue
        /// </summary>
        public int Count
        {
            get { return base_heap.Count; }
        }

        /// <summary>
        /// Copies the elements of the priority queue to an Array, starting at a particular Array index. 
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from the priority queue. The Array must have zero-based indexing. </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <remarks>
        /// It is not guaranteed that items will be copied in the sorted order.
        /// </remarks>
        public void CopyTo(KeyValuePair<TPriority, TValue>[] array, int arrayIndex)
        {
            base_heap.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets a value indicating whether the collection is read-only. 
        /// </summary>
        /// <remarks>
        /// For priority queue this property returns <c>false</c>.
        /// </remarks>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the priority queue. 
        /// </summary>
        /// <param name="item">The object to remove from the ICollection <(Of <(T >)>). </param>
        /// <returns><c>true</c> if item was successfully removed from the priority queue.
        /// This method returns false if item is not found in the collection. </returns>
        public bool Remove(KeyValuePair<TPriority, TValue> item)
        {
            // find element in the collection and remove it
            int elementIdx = base_heap.IndexOf(item);
            if (elementIdx < 0) return false;

            //remove element
            base_heap[elementIdx] = base_heap[base_heap.Count - 1];
            base_heap.RemoveAt(base_heap.Count - 1);

            // heapify
            int newPos = HeapifyFromEndToBeginning(elementIdx);
            if (newPos == elementIdx)
                HeapifyFromBeginningToEnd(elementIdx);

            return true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator</returns>
        /// <remarks>
        /// Returned enumerator does not iterate elements in sorted order.</remarks>
        public IEnumerator<KeyValuePair<TPriority, TValue>> GetEnumerator()
        {
            return base_heap.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator</returns>
        /// <remarks>
        /// Returned enumerator does not iterate elements in sorted order.</remarks>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
