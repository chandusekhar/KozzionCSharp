using KozzionGraphics.ColorFunction;
using KozzionGraphics.Image;
using KozzionGraphics.Rendering;
using NAudio.Wave;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace KozzionAudioUI
{
    public class ModelApplication : ReactiveObject
    {
        //private WriteableBitmap channel0_image;
        //public WriteableBitmap Channel0Image
        //{
        //    get { return this.channel0_image; }
        //    set { this.RaiseAndSetIfChanged(ref this.channel0_image, value); }
        //}

        private BitmapSource channel0_image;
        public BitmapSource Channel0Image
        {
            get { return this.channel0_image; }
            set { this.RaiseAndSetIfChanged(ref this.channel0_image, value); }
        }

        public ReactiveCommand CommandStart { get; private set; }
        public ReactiveCommand CommandStop { get; private set; }



        SpectralAnaliser SpectralAnaliser0;
        //SpectralAnaliser SpectralAnaliser1;

        public ModelApplication()
        {
            this.CommandStart = ReactiveCommand.Create(ExecuteStart);
            this.CommandStop = ReactiveCommand.Create(ExecuteStop);

            RendererBitmapSourceDefault<float> renderer = new RendererBitmapSourceDefault<float>(new FunctionFloat32ToColorJet(0, 1));
            int block_size   = 4096; //4096 * 8;
            int block_stride = 4096; //4096 * 8;
            int size_x = 256;
            int size_y = 256;

            Channel0Image = renderer.Render(new ImageRaster2D<float>(256, 256));
            //Channel1Image = renderer.Render(new ImageRaster2D<float>(256, 256));
   
            TaskScheduler ui_context = TaskScheduler.FromCurrentSynchronizationContext();
            this.SpectralAnaliser0 = new SpectralAnaliser(this, 0, block_size, block_stride, size_x, size_y);
            this.SpectralAnaliser0.ExecuteStart();

        }

        private IWaveIn wave_in;

        private void ExecuteStart()
        {
            // Start recording from loopback
            wave_in = new WasapiLoopbackCapture();
            //wave_in = new WaveIn();

            wave_in.DataAvailable += DataAvailable;
            wave_in.StartRecording();
                 

        }

        private void ExecuteStop()
        {
            //SpectralAnaliser0.Stop();
            //SpectralAnaliser1.Stop();
            wave_in.StopRecording();
            wave_in.Dispose();
        }


        private void DataAvailable(object sender, WaveInEventArgs e)
        {
            // write recorded data to MP3 writer
            //byte[] bytes = new byte[e.BytesRecorded];

            //Array.Copy(e.Buffer, 0, bytes, 0, e.BytesRecorded);
            if (e.Buffer.Length == 0)
            {
                return;
            }

            float[] stream0 = new float[e.Buffer.Length / 8];
            float[] stream1 = new float[e.Buffer.Length / 8];
            BinaryReader reader = new BinaryReader(new MemoryStream(e.Buffer), Encoding.BigEndianUnicode);
            for (int index = 0; index < e.Buffer.Length / 8; index++)
            {
                stream0[index] = reader.ReadSingle();
                stream1[index] = reader.ReadSingle();
                // stream0[index] = bytes[index* 2]
                // stream1[index] = bytes[(index * 2) + 1];
            }
            SpectralAnaliser0.AddData(stream0);
            //SpectralAnaliser1.AddData(stream1);
        }




    }
}
