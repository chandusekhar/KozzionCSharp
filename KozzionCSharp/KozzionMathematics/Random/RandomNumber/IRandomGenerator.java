package com.kozzion.library.math.random.numbergenerator;

public interface IRandomGenerator
{
    byte next_byte();
    
    byte [] next_bytes(int bytes);

    void next_bytes(byte [] array);

    int next_int();
}
