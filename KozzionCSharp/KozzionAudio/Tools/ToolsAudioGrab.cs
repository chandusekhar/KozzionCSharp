using System.ComponentModel;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using KozzionAudio.VolumeControl;

namespace KozzionAudio.Tools
{
    public class ToolsAudioGrab
    {
        public NAudio.CoreAudioApi.MMDeviceEnumerator DeviceEnumerator { get; set; }
        public SoundCardRecorder SoundCardRecorder { get; set;  }

        public ToolsAudioGrab()
        {
            DeviceEnumerator = new NAudio.CoreAudioApi.MMDeviceEnumerator();
            var devices = DeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();

            // instantiate the sound recorder once in an attempt to reduce lag the first time used
            try
            {
                SoundCardRecorder = new SoundCardRecorder((MMDevice)devices[0]);
                SoundCardRecorder.Dispose();
                SoundCardRecorder = null;
            }
            catch (Exception)
            {
            }


        }

        public List<MMDevice> GetDevices()
        {
            return DeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();
        }

        public List<MMDevice> GetAllDevices()
        {
            return DeviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToList();
        }

        public void StartRecording(MMDevice device)
        {
            if (device == null)
            {
                throw new Exception("");
            }
            if (SoundCardRecorder != null)
            {
                throw new Exception(""); //TODO needs mutexes prolly
            }

            SoundCardRecorder = new SoundCardRecorder(device);
            SoundCardRecorder.Start();

        }

        public void StopRecording()
        {
            if (SoundCardRecorder == null)
            {
                throw new Exception(""); //TODO needs mutexes prolly
            }
            SoundCardRecorder.Stop();      
            SoundCardRecorder.Dispose();
            SoundCardRecorder = null;        
        }
    }
}
