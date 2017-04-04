using KozzionAudioUI;
using KozzionGraphics.ColorFunction;
using KozzionGraphics.Image;
using KozzionGraphics.Rendering;
using KozzionGraphics.Tools;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace KozzionAudioUI
{
    public class SpectralAnaliser
    {
        private readonly ModelApplication application;
        private readonly Dispatcher Dispatcher;
        private Blocker blocker;
        private bool is_running;

        private int channel;

        private ImageRaster2D<float> image;
        private int f_rate;
        private int x_index;


        public SpectralAnaliser(ModelApplication application, int channel, int block_size, int block_stride, int size_x, int size_y)
        {
            this.application = application;
            this.Dispatcher = Dispatcher.CurrentDispatcher;
            this.blocker = new Blocker(block_size, 8);
            this.blocker.ExecuteStart();
            this.is_running = false;

            this.channel = channel;

            this.image = new ImageRaster2D<float>(size_x, size_y);
            this.x_index = 0;
            this.f_rate = block_size / (size_y * 2 );
         
            Render();

        }

        private void Render()
        {
            //WriteableBitmap x;

            //x.WritePixels()

            //Set image
            if (this.channel == 0)
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                { 
                    RendererBitmapSourceDefault < float > renderer = new RendererBitmapSourceDefault<float>(new FunctionFloat32ToColorParula(0, 2));
                    BitmapSource result = renderer.Render(image);
                    //application.Channel0Image = result;
                }  ));
            }
            else
            {
                //this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                //  { RendererBitmapSourceDefault<float> renderer = new RendererBitmapSourceDefault<float>(new FunctionFloat32ToColorASIST(0, 2));
                //      BitmapSource result = renderer.Render(image);
                //      application.Channel1Image = result;
                //  }));
            }
        }

        public void AddData(float[] data)
        {
            blocker.InputQueue.Enqueue(data);
        }

        private void Start()
        {
            Complex[] block;
            while (is_running)
            {

                if (blocker.OutputQueue.TryDequeue(out block))
                {

                    // do fft
                    MathNet.Numerics.IntegralTransforms.Fourier.Forward(block);
                    //TODO Shift image? 

                    float sum = 0;
                    for (int y_index = 0; y_index < image.Raster.Size1; y_index++)
                    {
                        //image.SetElementValue(x_index, y_index, (float)random.NextDouble());
                        float value = 0;
                        for (int f_index = 0; f_index < f_rate; f_index++)
                        {
                            if ((y_index * f_rate) + f_index < (block.Length))
                            {
                                value += (float)(block[(y_index * f_rate) + f_index].Magnitude * ((float)((y_index * f_rate) + f_index)));
                            }
                        }
                        value /= f_rate;
                        sum += value;
                        image.SetElementValue(x_index, y_index, value);
                    }

                    sum /= image.Raster.Size1 * 0.5f;

                    for (int y_index = 0; y_index < image.Raster.Size1; y_index++)
                    {
                        image.SetElementValue(x_index, y_index, image.GetElementValue(x_index, y_index) / sum);
                    }
                    x_index = (x_index + 1) % image.Raster.Size0;
                }
                //Render image
                Render();                     
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
