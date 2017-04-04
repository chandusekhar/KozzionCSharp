using System;

namespace KozzionMathematics.Datastructure.k_d_tree
{
    /// <summary>
    /// A MinHeap is a smallest-first queue based around a binary heap so it is quick to insert / remove items.
    /// </summary>
    /// <typeparam name="DomainType">The type of data this MinHeap stores.</typeparam>
    /// <remarks>This is based on this: https://bitbucket.org/rednaxela/knn-benchmark/src/tip/ags/utils/dataStructures/trees/thirdGenKD/ </remarks>
    public class MinHeap<DomainType, DistanceType>
        where DistanceType : IComparable<DistanceType>
    {
        /// <summary>
        /// The default size for a min heap.
        /// </summary>
        private static int DEFAULT_SIZE = 64;

        /// <summary>
        /// The data array.  This stores the data items in the heap.
        /// </summary>
        private DomainType[] values;

        /// <summary>
        /// The key array.  This determines how items are ordered. Smallest first.
        /// </summary>
        private DistanceType[] keys;

        /// <summary>
        /// Create a new min heap with the default capacity.
        /// </summary>
        public MinHeap()
            : this(DEFAULT_SIZE)
        {
        }

        /// <summary>
        /// Create a new min heap with a given capacity.
        /// </summary>
        /// <param name="capacity"></param>
        public MinHeap(int capacity)
        {
            this.values = new DomainType[capacity];
            this.keys = new DistanceType[capacity];
            this.Capacity = capacity;
            this.Size = 0;
        }

        /// <summary>
        /// The number of items in this queue.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// The amount of space in this queue.
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Insert a new element.
        /// </summary>
        /// <param name="key">The key which represents its position in the priority queue (ie. distance).</param>
        /// <param name="value">The value to be stored at the key.</param>
        public void Insert(DistanceType key, DomainType value)
        {
            // If we need more room, double the space.
            if (Size >= Capacity)
            {
                // Calcualte the new capacity.
                Capacity *= 2;

                // Copy the data array.
                var newData = new DomainType[Capacity];
                Array.Copy(values, newData, values.Length);
                values = newData;

                // Copy the key array.
                DistanceType [] newKeys = new DistanceType[Capacity];
                Array.Copy(keys, newKeys, keys.Length);
                keys = newKeys;
            }

            // Insert new value at the end
            values[Size] = value;
            keys[Size] = key;
            SiftUp(Size);
            Size++;
        }

        /// <summary>
        /// Remove the smallest element.
        /// </summary>
        public void RemoveMin()
        {
            if (Size == 0)
                throw new Exception();

            Size--;
            values[0] = values[Size];
            keys[0] = keys[Size];
            values[Size] = default(DomainType);
            SiftDown(0);
        }

        /// <summary>
        /// Get the data stored at the minimum element.
        /// </summary>
        public DomainType Min
        {
            get
            {
                if (Size == 0)
                    throw new Exception();

                return values[0];
            }
        }

        /// <summary>
        /// Get the key which represents the minimum element.
        /// </summary>
        public DistanceType MinKey
        {
            get
            {
                if (Size == 0)
                    throw new Exception();

                return keys[0];
            }
        }

        /// <summary>
        /// Bubble a child item up the tree.
        /// </summary>
        /// <param name="child"></param>
        private void SiftUp(int child)
        {
            // For each parent above the child, if the parent is smaller then bubble it up.
            for (int iParent = (child - 1) / 2;
                child != 0 && (keys[child].CompareTo(keys[iParent]) == -1);
                child = iParent, iParent = (child - 1) / 2)
            {
                DomainType kData = values[iParent];
                DistanceType dDist = keys[iParent];

                values[iParent] = values[child];
                keys[iParent] = keys[child];

                values[child] = kData;
                keys[child] = dDist;
            }
        }

        /// <summary>
        /// Bubble a parent down through the children so it goes in the right place.
        /// </summary>
        /// <param name="parent">The index of the parent.</param>
        private void SiftDown(int parent)
        {
            // For each child.
            for (int iChild = parent * 2 + 1; iChild < Size; parent = iChild, iChild = parent * 2 + 1)
            {
                // If the child is larger, select the next child.
                if (iChild + 1 < Size && (keys[iChild].CompareTo(keys[iChild + 1]) == 1))
                    iChild++;

                // If the parent is larger than the largest child, swap.
                if (keys[parent].CompareTo(keys[iChild]) == 1)
                {
                    // Swap the points
                    DomainType pData = values[parent];
                    DistanceType pDist = keys[parent];

                    values[parent] = values[iChild];
                    keys[parent] = keys[iChild];

                    values[iChild] = pData;
                    keys[iChild] = pDist;
                }

                // TODO: REMOVE THE BREAK
                else
                {
                    break;
                }
            }
        }
    }
}