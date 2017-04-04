using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionAudio.Music
{
    public class ToneSystemWestern : ToneSystem
    {
        //A = 440 Hz
        public static string[] WESTERN_TONE_NAMES = new string[]
        {"A","Bes", "B", "C","Cies","D","Dies","E","F" ,"Fies","G", "Gies"};

        public ToneSystemWestern(double base_frequency, int[] selected_tone_indexes)
            : base(base_frequency, 12, WESTERN_TONE_NAMES, selected_tone_indexes)
        {
            if (selected_tone_indexes.Length != 7)
            {
                throw new Exception("Western tone systems choos 7 out of 12");
            }
        }
        
        public ToneSystemWestern( int [] selected_tones)
            : this(440, selected_tones)
        {
        }
    }
}
