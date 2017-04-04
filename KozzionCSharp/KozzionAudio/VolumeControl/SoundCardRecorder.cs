using System;
using System.Diagnostics;
using NAudio.CoreAudioApi;
using NAudio.Wave;


namespace KozzionAudio.VolumeControl
{

    public class SoundCardRecorder : IDisposable
    {
        private MMDevice Device { get; set; }
        private IWaveIn wave_in;
        private WaveFileWriter writer;
        private Stopwatch _stopwatch = new Stopwatch();
        public TimeSpan Duration { get { return _stopwatch.Elapsed; } }



        public SoundCardRecorder(MMDevice device)
        {
            Device = device;

            wave_in = new WasapiCapture(Device);
            writer = new WaveFileWriter("test3.wav", wave_in.WaveFormat);
            wave_in.DataAvailable += OnDataAvailable;

        }

        public void Dispose()
        {
            if (wave_in != null)
            {
                wave_in.StopRecording();
                wave_in.Dispose();
                wave_in = null;
            }
            if (writer != null)
            {
                writer.Close();
                writer = null;
            }
        }



        void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            //            if (InvokeRequired)
            //            {
            //                BeginInvoke(new EventHandler<WaveInEventArgs>(OnDataAvailable), sender, e);
            //            }
            //            else
            //            {
            if (writer != null)
            {
                writer.Write(e.Buffer, 0, e.BytesRecorded);
            }
            //            }
        }



        public void Start()
        {
            wave_in.StartRecording();
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        public void Stop()
        {
            if (wave_in != null)
            {
                wave_in.StopRecording();
                wave_in.Dispose();
                wave_in = null;
            }
            if (writer != null)
            {
                writer.Close();
                writer = null;
            }

        }
    }
}
