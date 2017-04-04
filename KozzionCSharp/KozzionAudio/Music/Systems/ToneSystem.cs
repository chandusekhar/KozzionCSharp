using KozzionCore.Tools;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KozzionAudio.Music
{
    public class ToneSystem
    {
        public double BaseFrequency { get; private set; }
        public int ToneCount { get; private set; }
        public int ScaleToneCount { get; private set; }

        private double[] frequency_multiplyers;
        private int[] notes_to_tones;
        private string[] tone_names;    

        public ToneSystem(double base_frequency, int tone_count, string [] all_tone_names, int[] selected_tone_indexes)
        {
            this.BaseFrequency = base_frequency;
            this.ToneCount = tone_count;
            this.ScaleToneCount = selected_tone_indexes.Length;

            this.frequency_multiplyers = new double[tone_count];
            this.notes_to_tones = ToolsMathSeries.RangeInt32(tone_count).Select(selected_tone_indexes);
            this.tone_names = ToolsCollection.Copy(all_tone_names);

            for (int tone_index = 0; tone_index < tone_count; tone_index++)
            {
                frequency_multiplyers[tone_index] = Math.Pow(2, tone_index / (double)tone_count);
            }
          
        }

        public double GetFrequencyTone(int harmonic, int tone_index)
        {
            harmonic += tone_index / ToneCount;
            tone_index %= ToneCount;
            return BaseFrequency * Math.Pow(2, harmonic) * frequency_multiplyers[tone_index];
        }

        public double GetFrequencyNote(int harmonic, int note_index)
        {
            return GetFrequencyTone( harmonic, notes_to_tones[note_index]);
        }
    }
}
