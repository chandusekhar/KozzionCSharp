package com.kozzion.library.core.datastructure.map;

import java.util.Collection;
import java.util.Set;

import com.kozzion.library.core.datastructure.Tuple2;

public interface IDualKeyMap<KeyType0, KeyType1, ValueType>
{

    public void clear();

    public boolean contains_key_pair(KeyType0 key_0, KeyType1 key_1);

    public boolean contains_value(ValueType value);

    public ValueType get(KeyType0 key_0, KeyType1 key_1);

    public ValueType put(KeyType0 key_0, KeyType1 key_1, ValueType value);

    public ValueType remove(KeyType0 key_0, KeyType1 key_1);

    public Collection<ValueType> values();

    public int size();

    public boolean is_empty();

    public Set<Tuple2<KeyType0, KeyType1>> key_pair_set();

    public Set<KeyType0> key_0_set();

    public Set<KeyType1> key_1_set();

}
