using System.Collections.Generic;

namespace KozzionPerfusionCL.Experiments
{
    public class StructuringElement3D
    {
        public List<int[]> RegularOffsets;
        public List<int[]> FlippedOffsets;
        public StructuringElement3D(List<int[]> element_offsets)
        {
            this.RegularOffsets = new List<int[]>(element_offsets); //TODO make indexing structs vector3int32 for instance
            this.FlippedOffsets = new List<int[]>();
            foreach (int[] offset in RegularOffsets)
            {
                FlippedOffsets.Add(new int[] { -offset[0], -offset[1], -offset[2] });
            }
        }
    }
}