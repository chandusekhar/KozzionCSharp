package com.kozzion.library.math.datastructure.kdtree;

import java.io.Serializable;
import java.lang.reflect.Array;
import java.util.Arrays;
import java.util.Collection;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Map;
import java.util.Set;

import com.kozzion.library.math.datastructure.matrix.DoubleVector;

// All the view classes are inefficient for anything other than iteration.
/**
 * <p>
 * A k-d tree divides a k-dimensional space relative to the points it contains by storing them in a binary tree,
 * discriminating by a different dimension at each level of the tree. It allows efficient point data retrieval (
 * <em>O(lg(n))</em>) and range searching.
 * </p>
 * <p>
 * KDTree conforms to the java.util.Map interface except that Iterator.remove is not supported by the returned views.
 * </p>
 */
public class KDTreeFloat<LabellingType> implements IRangeSearchTreeFloat<LabellingType>, Serializable
{
    /**
     * 
     */
    private static final long serialVersionUID = 1L;

    int                       d_size;
    int                       d_hash_code;
    private final int         d_dimension_count;
    KdNode                    d_root_node;

    /**
     * Creates a KDTree of the specified number of dimensions.
     * 
     * @param dimensions
     *            The number of dimensions. Must be greater than 0.
     */
    public KDTreeFloat(final int dimensions)
    {
        assert (dimensions > 0);
        d_dimension_count = dimensions;
        clear();
    }

    // Begin Map interface methods

    /**
     * Removes all elements from the container, leaving it empty.
     */
    @Override
    public void clear()
    {
        d_root_node = null;
        d_size = d_hash_code = 0;
    }

    /**
     * Returns true if the container contains a mapping for the specified key.
     * 
     * @param key
     *            The point key to search for.
     * @return true if the container contains a mapping for the specified key.
     * @exception ClassCastException
     *                if the key is not an instance of P.
     */
    @Override
    public boolean containsKey(final Object key) throws ClassCastException
    {
        return (getNode((float []) key) != null);
    }

    /**
     * Returns true if the container contains a mapping with the specified value. Note: this is very inefficient for
     * KDTrees because it requires searching the entire tree.
     * 
     * @param value
     *            The value to search for.
     * @return true If the container contains a mapping with the specified value.
     */
    @Override
    public boolean containsValue(final Object value)
    {
        return (findValue(d_root_node, value) != null);
    }

    /**
     * Returns a Set view of the point to value mappings in the KDTree. Modifications to the resulting set will be
     * reflected in the KDTree and vice versa, except that {@code Iterator.remove} is not supported.
     * 
     * @return A Set view of the point to value mappings in the KDTree.
     */
    @Override
    public Set<Map.Entry<float [], LabellingType>> entrySet()
    {
        return new MapEntrySet();
    }

    /**
     * Returns true if the object contains the same mappings, false if not.
     * 
     * @param o
     *            The object to test for equality.
     * @return true if the object contains the same mappings, false if not.
     */
    @Override
    public boolean equals(final Object object) throws ClassCastException
    {
        if (!(object instanceof Map))
        {
            return false;
        }

        if (object == this)
        {
            return true;
        }

        final Map<DoubleVector, LabellingType> map = (Map<DoubleVector, LabellingType>) object;
        return (entrySet().equals(map.entrySet()));
    }

    /**
     * Retrieves the value at the given location.
     * 
     * @param point
     *            The location from which to retrieve the value.
     * @return The value at the given location, or null if no value is present.
     * @exception ClassCastException
     *                If the given point is not of the expected type.
     */
    @Override
    public LabellingType get(final Object point) throws ClassCastException
    {
        final KdNode node = getNode((float []) point);
        if (node == null)
        {
            return null;
        }
        else
        {
            return node.getValue();
        }
    }

    /**
     * Returns the hash code value for this map.
     * 
     * @return The sum of the hash codes of all of the map entries.
     */
    @Override
    public int hashCode()
    {
        return d_hash_code;
    }

    /**
     * Returns true if the container has no elements, false if it contains one or more elements.
     * 
     * @return true if the container has no elements, false if it contains one or more elements.
     */
    @Override
    public boolean isEmpty()
    {
        return (d_root_node == null);
    }

    /**
     * Returns a Set view of the point keys for the mappings in the KDTree. Changes to the Set are reflected in the
     * KDTree and vice versa, except that {@code Iterator.remove} is not supported.
     * 
     * @return A Set view of the point keys for the mappings in the KDTree.
     */
    @Override
    public Set<float []> keySet()
    {
        return new KeySet();
    }

    /**
     * Inserts a point value pair into the tree, preserving the spatial ordering.
     * 
     * @param point
     *            The point serving as a key.
     * @param value
     *            The value to insert at the point.
     * @return The old value if an existing value is replaced by the inserted value.
     */
    @Override
    public LabellingType put(final float [] point, final LabellingType value)
    {
        final KdNode [] parent = new KDTreeFloat.KdNode [1];
        KdNode node = getNode(point, parent);
        LabellingType old = null;
        if (node != null)
        {
            old = node.getValue();
            d_hash_code -= node.hashCode();
            node.d_value = value;
        }
        else
        {
            if (parent[0] == null)
            {
                node = d_root_node = new KdNode(0, point, value);
            }
            else
            {
                final int discriminator = parent[0].d_discriminator;
                if (point[discriminator] >= parent[0].d_point[discriminator])
                {
                    node = parent[0].d_high = new KdNode((discriminator + 1) % d_dimension_count, point, value);
                }
                else
                {
                    node = parent[0].d_low = new KdNode((discriminator + 1) % d_dimension_count, point, value);
                }
            }
            ++d_size;
        }
        d_hash_code += node.hashCode();
        return old;
    }

    /**
     * Copies all of the point-value mappings from the given Map into the KDTree.
     * 
     * @param map
     *            The Map from which to copy the mappings.
     */
    @Override
    public void putAll(final Map<? extends float [], ? extends LabellingType> map)
    {
        for (final Map.Entry<? extends float [], ? extends LabellingType> pair : map.entrySet())
        {
            put(pair.getKey(), pair.getValue());
        }
    }

    /**
     * Removes the point-value mapping corresponding to the given point key.
     * 
     * @param key
     *            The point key of the mapping to remove.
     * @return The value part of the mapping, if a mapping existed and was removed. Null if not.
     * @exception ClassCastException
     *                If the key is not an instance of P.
     */
    @Override
    public LabellingType remove(final Object key) throws ClassCastException
    {
        final KdNode [] parent = new KDTreeFloat.KdNode [1];
        KdNode node = getNode((float []) key, parent);
        LabellingType old = null;

        if (node != null)
        {
            final KdNode child = node;

            node = recursiveRemoveNode(child);

            if (parent[0] == null)
            {
                d_root_node = node;
            }
            else
                if (child == parent[0].d_low)
                {
                    parent[0].d_low = node;
                }
                else
                    if (child == parent[0].d_high)
                    {
                        parent[0].d_high = node;
                    }

            --d_size;
            d_hash_code -= child.hashCode();
            old = child.getValue();
        }

        return old;
    }

    /**
     * Returns the number of point-value mappings in the KDTree.
     * 
     * @return The number of point-value mappings in the KDTree.
     */
    @Override
    public int size()
    {
        return d_size;
    }

    /**
     * Returns a Collection view of the values contained in the KDTree. Changes to the Collection are reflected in the
     * KDTree and vice versa. Note: the resulting Collection is very inefficient.
     * 
     * @return A Collection view of the values contained in the KDTree.
     */
    @Override
    public Collection<LabellingType> values()
    {
        return new ValueCollection();
    }

    // End Map interface methods

    @Override
    public Iterator<Map.Entry<float [], LabellingType>> iterator(final float [] lower, final float [] upper)
    {
        return new MapEntryIterator(lower, upper);
    }

    int fillArray(final KdNode [] a, int index, final KdNode node)
    {
        if (node == null)
        {
            return index;
        }
        a[index] = node;
        index = fillArray(a, index + 1, node.d_low);
        return fillArray(a, index, node.d_high);
    }

    KdNode optimize(final KdNode [] nodes, final int begin, final int end, final NodeComparator comp)
    {
        KdNode midpoint = null;
        final int size = end - begin;

        if (size > 1)
        {
            int nth = begin + (size >> 1);
            int nthprev = nth - 1;
            int d = comp.getDiscriminator();

            Arrays.sort(nodes, begin, end, comp);

            while ((nth > begin) && (nodes[nth].d_point[d] == nodes[nthprev].d_point[d]))
            {
                --nth;
                --nthprev;
            }

            midpoint = nodes[nth];
            midpoint.d_discriminator = d;

            if (++d >= d_dimension_count)
            {
                d = 0;
            }

            comp.setDiscriminator(d);

            midpoint.d_low = optimize(nodes, begin, nth, comp);

            comp.setDiscriminator(d);

            midpoint.d_high = optimize(nodes, nth + 1, end, comp);
        }
        else
            if (size == 1)
            {
                midpoint = nodes[begin];
                midpoint.d_discriminator = comp.getDiscriminator();
                midpoint.d_low = midpoint.d_high = null;
            }
        return midpoint;
    }

    /**
     * Optimizes the performance of future search operations by balancing the KDTree. The balancing operation is
     * relatively expensive, but can significantly improve the performance of searches. Usually, you don't have to
     * optimize a tree which contains random key values inserted in a random order.
     */
    public void optimize()
    {
        if (isEmpty())
        {
            return;
        }

        final KdNode [] nodes = (KdNode []) Array.newInstance(KdNode.class, size());
        fillArray(nodes, 0, d_root_node);
        d_root_node = optimize(nodes, 0, nodes.length, new NodeComparator());
    }

    final class NodeComparator implements Comparator<KdNode>, Serializable
    {
        private static final long serialVersionUID = 1L;

        int                       _discriminator   = 0;

        void setDiscriminator(final int val)
        {
            _discriminator = val;
        }

        int getDiscriminator()
        {
            return _discriminator;
        }

        @Override
        public int compare(final KdNode n1, final KdNode n2)
        {
            return Float.compare(n1.d_point[_discriminator], n2.d_point[_discriminator]);
        }
    }

    final class KdNode implements Map.Entry<float [], LabellingType>, Serializable
    {
        private static final long serialVersionUID = 1L;

        int                       d_discriminator;
        float []                  d_point;
        LabellingType             d_value;
        KdNode                    d_low;
        KdNode                    d_high;

        KdNode(final int discriminator, final float [] point, final LabellingType value)
        {
            d_point = point;
            d_value = value;
            d_low = null;
            d_high = null;
            d_discriminator = discriminator;
        }

        @Override
        public boolean equals(final Object object)
        {
            final KdNode node = (KdNode) object;

            if (node == this)
            {
                return true;
            }
            else
            {
                return ((getKey() == null ? node.getKey() == null : getKey().equals(node.getKey())) && (getValue() == null ? node
                    .getValue() == null : getValue().equals(node.getValue())));
            }
        }

        @Override
        public float [] getKey()
        {
            return d_point;
        }

        @Override
        public LabellingType getValue()
        {
            return d_value;
        }

        // Only call if the node is in the tree.
        @Override
        public LabellingType setValue(final LabellingType value)
        {
            final LabellingType old = d_value;
            d_hash_code -= hashCode();
            d_value = value;
            d_hash_code += hashCode();
            return old;
        }

        @Override
        public int hashCode()
        {
            return ((getKey() == null ? 0 : getKey().hashCode()) ^ (getValue() == null ? 0 : getValue().hashCode()));
        }
    }

    final class MapEntryIterator implements Iterator<Map.Entry<float [], LabellingType>>
    {
        LinkedList<KdNode> _stack;
        KdNode             _next;
        float []           _lower;
        float []           _upper;

        MapEntryIterator(final float [] lower, final float [] upper)
        {
            _stack = new LinkedList<KdNode>();
            _lower = lower;
            _upper = upper;
            _next = null;

            if (d_root_node != null)
            {
                _stack.addLast(d_root_node);
            }
            next();
        }

        MapEntryIterator()
        {
            this(null, null);
        }

        @Override
        public boolean hasNext()
        {
            return (_next != null);
        }

        @Override
        public Map.Entry<float [], LabellingType> next()
        {
            final KdNode old = _next;

            while (!_stack.isEmpty())
            {
                final KdNode node = _stack.removeLast();
                final int discriminator = node.d_discriminator;

                if (((_upper == null) || (node.d_point[discriminator] <= _upper[discriminator])) && (node.d_high != null))
                {
                    _stack.addLast(node.d_high);
                }

                if (((_lower == null) || (node.d_point[discriminator] > _lower[discriminator])) && (node.d_low != null))
                {
                    _stack.addLast(node.d_low);
                }

                if (isInRange(node.d_point, _lower, _upper))
                {
                    _next = node;
                    return old;
                }
            }

            _next = null;

            return old;
        }

        // This violates the contract for entrySet, but we can't support
        // in a reasonable fashion the removal of mappings through the iterator.
        // Java iterators require a hasNext() function, which forces the stack
        // to reflect a future search state, making impossible to adjust the
        // current
        // stack after a removal. Implementation alternatives are all too
        // expensive. Yet another reason to favor the C++ implementation...
        @Override
        public void remove() throws UnsupportedOperationException
        {
            throw new UnsupportedOperationException();
        }
    }

    final class KeyIterator implements Iterator<float []>
    {
        MapEntryIterator iterator;

        KeyIterator(final MapEntryIterator it)
        {
            iterator = it;
        }

        @Override
        public boolean hasNext()
        {
            return iterator.hasNext();
        }

        @Override
        public float [] next()
        {
            final Map.Entry<float [], LabellingType> next = iterator.next();
            return (next == null ? null : next.getKey());
        }

        @Override
        public void remove() throws UnsupportedOperationException
        {
            iterator.remove();
        }
    }

    final class ValueIterator implements Iterator<LabellingType>
    {
        MapEntryIterator iterator;

        ValueIterator(final MapEntryIterator it)
        {
            iterator = it;
        }

        @Override
        public boolean hasNext()
        {
            return iterator.hasNext();
        }

        @Override
        public LabellingType next()
        {
            final Map.Entry<float [], LabellingType> next = iterator.next();
            return (next == null ? null : next.getValue());
        }

        @Override
        public void remove() throws UnsupportedOperationException
        {
            iterator.remove();
        }
    }

    abstract class CollectionView<E> implements Collection<E>
    {

        @Override
        public boolean add(final E o) throws UnsupportedOperationException
        {
            throw new UnsupportedOperationException();
        }

        @Override
        public boolean addAll(final Collection<? extends E> c) throws UnsupportedOperationException
        {
            throw new UnsupportedOperationException();
        }

        @Override
        public void clear()
        {
            KDTreeFloat.this.clear();
        }

        @Override
        public boolean containsAll(final Collection<?> c)
        {
            for (final Object o : c)
            {
                if (!contains(o))
                {
                    return false;
                }
            }
            return true;
        }

        @Override
        public int hashCode()
        {
            return KDTreeFloat.this.hashCode();
        }

        @Override
        public boolean isEmpty()
        {
            return KDTreeFloat.this.isEmpty();
        }

        @Override
        public int size()
        {
            return KDTreeFloat.this.size();
        }

        @Override
        public Object [] toArray()
        {
            final Object [] obja = new Object [size()];
            int i = 0;

            for (final E e : this)
            {
                obja[i] = e;
                ++i;
            }

            return obja;
        }

        @Override
        public <T> T [] toArray(T [] a)
        {
            Object [] array = a;

            if (array.length < size())
            {
                array = a = (T []) Array.newInstance(a.getClass().getComponentType(), size());
            }

            if (array.length > size())
            {
                array[size()] = null;
            }

            int i = 0;
            for (final E e : this)
            {
                array[i] = e;
                ++i;
            }

            return a;
        }
    }

    abstract class SetView<E> extends CollectionView<E> implements Set<E>
    {
        @Override
        public boolean equals(final Object o)
        {
            if (!(o instanceof Set))
            {
                return false;
            }

            if (o == this)
            {
                return true;
            }

            final Set<?> set = (Set<?>) o;

            if (set.size() != size())
            {
                return false;
            }

            try
            {
                return containsAll(set);
            }
            catch (final ClassCastException cce)
            {
                return false;
            }
        }
    }

    final class MapEntrySet extends SetView<Map.Entry<float [], LabellingType>>
    {
        @Override
        public boolean contains(final Object o) throws ClassCastException, NullPointerException
        {
            final Map.Entry<float [], LabellingType> e = (Map.Entry<float [], LabellingType>) o;
            final KdNode node = getNode(e.getKey());

            if (node == null)
            {
                return false;
            }

            return e.getValue().equals(node.getValue());
        }

        @Override
        public Iterator<Map.Entry<float [], LabellingType>> iterator()
        {
            return new MapEntryIterator();
        }

        @Override
        public boolean remove(final Object o) throws ClassCastException
        {
            final int size = size();
            final Map.Entry<DoubleVector, LabellingType> e = (Map.Entry<DoubleVector, LabellingType>) o;

            KDTreeFloat.this.remove(e.getKey());

            return (size != size());
        }

        @Override
        public boolean removeAll(final Collection<?> c) throws ClassCastException
        {
            final int size = size();

            for (final Object o : c)
            {
                final Map.Entry<DoubleVector, LabellingType> e = (Map.Entry<DoubleVector, LabellingType>) o;
                KDTreeFloat.this.remove(e.getKey());
            }

            return (size != size());
        }

        @Override
        public boolean retainAll(final Collection<?> c) throws ClassCastException
        {
            for (final Object o : c)
            {
                if (contains(o))
                {
                    final Collection<Map.Entry<float [], LabellingType>> col = (Collection<Map.Entry<float [], LabellingType>>) c;
                    clear();
                    for (final Map.Entry<float [], LabellingType> e : col)
                    {
                        put(e.getKey(), e.getValue());
                    }
                    return true;
                }
            }
            return false;
        }
    }

    final class KeySet extends SetView<float []>
    {

        @Override
        public boolean contains(final Object o) throws ClassCastException, NullPointerException
        {
            return KDTreeFloat.this.containsKey(o);
        }

        @Override
        public Iterator<float []> iterator()
        {
            return new KeyIterator(new MapEntryIterator());
        }

        @Override
        public boolean remove(final Object o) throws ClassCastException
        {
            final int size = size();
            KDTreeFloat.this.remove(o);
            return (size != size());
        }

        @Override
        public boolean removeAll(final Collection<?> c) throws ClassCastException
        {
            final int size = size();

            for (final Object o : c)
            {
                KDTreeFloat.this.remove(o);
            }

            return (size != size());
        }

        @Override
        public boolean retainAll(final Collection<?> c) throws ClassCastException
        {
            final HashMap<float [], LabellingType> map = new HashMap<float [], LabellingType>();
            final int size = size();

            for (final Object o : c)
            {
                final LabellingType val = get(o);

                if ((val != null) || contains(o))
                {
                    map.put((float []) o, val);
                }
            }

            clear();
            putAll(map);

            return (size != size());
        }
    }

    final class ValueCollection extends CollectionView<LabellingType>
    {

        @Override
        public boolean contains(final Object o) throws ClassCastException, NullPointerException
        {
            return KDTreeFloat.this.containsValue(o);
        }

        @Override
        public Iterator<LabellingType> iterator()
        {
            return new ValueIterator(new MapEntryIterator());
        }

        @Override
        public boolean remove(final Object o) throws ClassCastException
        {
            final KdNode node = findValue(d_root_node, o);

            if (node != null)
            {
                KDTreeFloat.this.remove(node.getKey());
                return true;
            }

            return false;
        }

        @Override
        public boolean removeAll(final Collection<?> c) throws ClassCastException
        {
            final int size = size();

            for (final Object o : c)
            {
                KdNode node = findValue(d_root_node, o);

                while (node != null)
                {
                    KDTreeFloat.this.remove(o);
                    node = findValue(d_root_node, o);
                }
            }

            return (size != size());
        }

        @Override
        public boolean retainAll(final Collection<?> c) throws ClassCastException
        {
            final HashMap<float [], LabellingType> map = new HashMap<float [], LabellingType>();
            final int size = size();

            for (final Object o : c)
            {
                KdNode node = findValue(d_root_node, o);

                while (node != null)
                {
                    map.put(node.getKey(), node.getValue());
                    node = findValue(d_root_node, o);
                }
            }

            clear();
            putAll(map);

            return (size != size());
        }
    }

    KdNode getNode(final float [] point, final KdNode [] parent)
    {
        int discriminator;
        KdNode node = d_root_node, current, last = null;
        float c1;
        float c2;

        while (node != null)
        {
            discriminator = node.d_discriminator;
            c1 = point[discriminator];
            c2 = node.d_point[discriminator];
            current = node;

            if (c1 > c2)
            {
                node = node.d_high;
            }
            else
                if (c1 < c2)
                {
                    node = node.d_low;
                }
                else
                    if (node.d_point.equals(point))
                    {
                        if (parent != null)
                        {
                            parent[0] = last;
                        }
                        return node;
                    }
                    else
                    {
                        node = node.d_high;
                    }

            last = current;
        }

        if (parent != null)
        {
            parent[0] = last;
        }

        return null;
    }

    KdNode getNode(final float [] point)
    {
        return getNode(point, null);
    }

    KdNode getMinimumNode(final KdNode node, final KdNode p, final int discriminator, final KdNode [] parent)
    {
        KdNode result;

        if (discriminator == node.d_discriminator)
        {
            if (node.d_low != null)
            {
                return getMinimumNode(node.d_low, node, discriminator, parent);
            }
            else
            {
                result = node;
            }
        }
        else
        {
            KdNode nlow = null, nhigh = null;
            final KdNode [] plow = new KDTreeFloat.KdNode [1], phigh = new KDTreeFloat.KdNode [1];

            if (node.d_low != null)
            {
                nlow = getMinimumNode(node.d_low, node, discriminator, plow);
            }

            if (node.d_high != null)
            {
                nhigh = getMinimumNode(node.d_high, node, discriminator, phigh);
            }

            if ((nlow != null) && (nhigh != null))
            {
                if (nlow.d_point[discriminator] < nhigh.d_point[discriminator])
                {
                    result = nlow;
                    parent[0] = plow[0];
                }
                else
                {
                    result = nhigh;
                    parent[0] = phigh[0];
                }
            }
            else
                if (nlow != null)
                {
                    result = nlow;
                    parent[0] = plow[0];
                }
                else
                    if (nhigh != null)
                    {
                        result = nhigh;
                        parent[0] = phigh[0];
                    }
                    else
                    {
                        result = node;
                    }
        }

        if (result == node)
        {
            parent[0] = p;
        }
        else
            if (node.d_point[discriminator] < result.d_point[discriminator])
            {
                result = node;
                parent[0] = p;
            }

        return result;
    }

    KdNode recursiveRemoveNode(final KdNode node)
    {
        int discriminator;

        if ((node.d_low == null) && (node.d_high == null))
        {
            return null;
        }
        else
        {
            discriminator = node.d_discriminator;
        }

        if (node.d_high == null)
        {
            node.d_high = node.d_low;
            node.d_low = null;
        }

        final KdNode [] parent = new KDTreeFloat.KdNode [1];
        final KdNode newRoot = getMinimumNode(node.d_high, node, discriminator, parent);
        final KdNode child = recursiveRemoveNode(newRoot);

        if (parent[0].d_low == newRoot)
        {
            parent[0].d_low = child;
        }
        else
        {
            parent[0].d_high = child;
        }

        newRoot.d_low = node.d_low;
        newRoot.d_high = node.d_high;
        newRoot.d_discriminator = node.d_discriminator;

        return newRoot;
    }

    KdNode findValue(final KdNode node, final Object value)
    {
        if ((node == null) || (value == null ? node.getValue() == null : value.equals(node.getValue())))
        {
            return node;
        }

        KdNode result;

        if ((result = findValue(node.d_low, value)) == null)
        {
            result = findValue(node.d_high, value);
        }

        return result;
    }

    boolean isInRange(final float [] point, final float [] lower, final float [] upper)
    {
        Float coordinate1 = null;
        Float coordinate2 = null;
        Float coordinate3 = null;

        if ((lower != null) || (upper != null))
        {
            final int dimensions = point.length;

            for (int i = 0; i < dimensions; ++i)
            {
                coordinate1 = point[i];
                if (lower != null)
                {
                    coordinate2 = lower[i];
                }
                if (upper != null)
                {
                    coordinate3 = upper[i];
                }
                if (((coordinate2 != null) && (coordinate1.compareTo(coordinate2) < 0))
                    || ((coordinate3 != null) && (coordinate1.compareTo(coordinate3) > 0)))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public int get_dimension_count()
    {
        return d_dimension_count;
    }
}
