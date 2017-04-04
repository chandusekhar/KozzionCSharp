using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionAudio.VolumeControl
{
    public class AudioSessionControl
    {
        //from AudioSessionControl
        public string DisplayName { get; }
        //public string GroupingParam { get; }//TODO
        public string IconPath { get; }
        //public string GetState { get; }//TODO

        public int ProcessId { get; }
        public string SessionIdentifier { get; }
        public string SessionInstanceIdentifier { get; }
        //public string IsSystemSoundsSession { get; }//TODO
        

        public AudioSessionControl(string display_name, string icon_path, int process_id, string session_identifier, string session_instance_identifier)
        {
            this.DisplayName = display_name;
            this.IconPath = icon_path;
            this.ProcessId = process_id;
            this.SessionIdentifier = session_identifier;
            this.SessionInstanceIdentifier = session_instance_identifier;
        }
    }
}
