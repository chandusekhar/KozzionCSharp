using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using KozzionAudio.VolumeControl;
using KozzionAudio.Tools;
using System.Diagnostics;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using System.Threading;

namespace KozzionAudioCL
{
    public class Program
    {
        static WaveFileWriter wri;
  
        static void Main(string[] args)
        {
            // Start recording from loopback
            IWaveIn waveIn = new WasapiLoopbackCapture();
            waveIn.DataAvailable += waveIn_DataAvailable;
            // Setup MP3 writer to output at 32kbit/sec (~2 minutes per MB)
            wri = new WaveFileWriter("test.mp3", waveIn.WaveFormat);
            waveIn.StartRecording();

            // Keep recording until Escape key pressed
            Console.Read();
            waveIn.StopRecording();
            // flush output to finish MP3 file correctly
            wri.Flush();
            // Dispose of objects
            waveIn.Dispose();
            wri.Dispose();
        }


        static void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            // write recorded data to MP3 writer
            if (wri != null)
            {
                wri.Write(e.Buffer, 0, e.BytesRecorded);
            }
            byte[] bytes = new byte[e.BytesRecorded];
            Array.Copy(e.Buffer, 0, bytes, 0, e.BytesRecorded);
        }



      

 
    //Console.WriteLine("done");

    //ToolsAudioGrab grab = new ToolsAudioGrab();
    //foreach (MMDevice item in grab.GetAllDevices())
    //{

    //    if (item.FriendlyName.Contains("Speakers"))
    //    {
    //        Console.WriteLine("Recording: " + item.FriendlyName);
    //        grab.StartRecording(item);
    //        Console.Read();
    //        grab.StopRecording();
    //    }
    //}

    //Console.WriteLine("done");




    //int waveInDevices = WaveOut.DeviceCount;
    //for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
    //{
    //    WaveOutCapabilities deviceInfo = WaveOut.GetCapabilities(waveInDevice);
    //    Console.WriteLine("Device {0}: {1}, {2} channels", waveInDevice, deviceInfo.ProductName, deviceInfo.Channels);
    //}


    //int spotify_pid = GetSpotifyPID();


    //int[] audio_second = GrabAudioSecond(spotify_pid);



    //if (spotify_pid == -1)
    //{
    //    Console.WriteLine("Spotify not found:");
    //}
    //else
    //{
    //    Console.WriteLine("Spotify pid: " + spotify_pid);
    //    ToolsVolumeMixer.SetApplicationVolume(spotify_pid, 25f);
    //}



   


        private static int[] GrabAudioSecond(int spotify_pid)
        {
            throw new NotImplementedException();
        }

        public static int GetSpotifyPID()
        {
            KozzionAudio.VolumeControl.AudioSessionControl[] controls = ToolsVolumeMixer.GetAudioSessionControls();
            int spotify_pid = -1;
            foreach (KozzionAudio.VolumeControl.AudioSessionControl control in controls)
            {
                if (control.SessionIdentifier.Contains("Spotify"))
                {
                    spotify_pid = control.ProcessId;
                }
            }
            return spotify_pid;
        }
    }
}