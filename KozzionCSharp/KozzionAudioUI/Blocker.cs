using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KozzionAudioUI
{
    public class Blocker
    {
        public ConcurrentQueue<float[]> InputQueue;
        public ConcurrentQueue<Complex[]> OutputQueue;
        public int BlockSize { get; }
        public int BlockStride { get; }
        private bool is_running;

        public Blocker(int block_size, int block_stride)
        {
    
            this.BlockSize = block_size;
            this.BlockStride = block_stride;
            this.InputQueue = new ConcurrentQueue<float[]>();
            this.OutputQueue = new ConcurrentQueue<Complex[]>();
        }
 
        public void AddData(float[] data)
        {
            InputQueue.Enqueue(data);
        }


        private void Start()
        {
            float[] current_input;
            Complex [] current_block = new Complex[this.BlockSize];
            int current_block_offset = 0;   
            while (is_running)
            {

        
                if (InputQueue.TryDequeue(out current_input))
                {
                    int current_input_offset = 0;
                    while ((current_block.Length - current_block_offset) <= (current_input.Length - current_input_offset))
                    {
                        //Fill current block
                        int count = current_block.Length - current_block_offset;
                        for (int index = 0; index < count; index++)
                        {
                            current_block[index + current_block_offset] = current_input[index + current_input_offset];
                        }
                        current_input_offset += count;

                        //Addlocate new block
                        Complex[] new_current_block = new Complex[this.BlockSize];

                        //Move data into new block accoring to block stride           
                        Array.Copy(current_block, this.BlockStride, new_current_block, 0, this.BlockSize - this.BlockStride);
                    
                        //Queue current block
                        this.OutputQueue.Enqueue(current_block);
                        current_block = new_current_block;
                        current_block_offset = this.BlockStride;
                    }

                    // Move remainder of current_input into current_block
                    int remainder = current_input.Length - current_input_offset;
                    for (int index = 0; index < remainder; index++)
                    {
                        current_block[index + current_block_offset] = current_input[index + current_input_offset];
                    }
                    current_block_offset += current_input.Length - current_input_offset;
                }
            }
        }

        public async void ExecuteStart()
        {
            is_running = true;
            await Task.Run(() => Start());
        }

        public void Stop()
        {
            is_running = false;
        }

    }
}
