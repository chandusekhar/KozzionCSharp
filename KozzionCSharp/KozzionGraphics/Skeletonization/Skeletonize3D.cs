using KozzionGraphics.Image;
using KozzionGraphics.Image.Topology;
using KozzionGraphics.skeletonization;
using KozzionGraphics.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionGraphics.Skeletonization
{
    // Medial axis thinning of a 3D binary volume as described in:
    // Ta-Chih Lee, Rangasami L.Kashyap and Chong-Nam Chu "Building skeleton models via 3-D medial surface/axis thinning algorithms."
    // Computer Vision, Graphics, and Image Processing, 56(6):462–478, 1994.

    public class Skeletonize3D
    {
        public Skeletonize3D()
        { }


        public IImageRaster3D<bool> FindMedialAxes(IImageRaster3D<bool> image)
        {
            ImageRaster3D<bool> skeleton_image = ToolsImageRaster.Pad(image, false, 1, 1, 1);
            ITopologyElement topology = new TopologyElementRaster3D26Connectivity(skeleton_image.Raster);
            int[] euler_table = BuildEulerLookupTable();
            bool changes = true;
            while (changes)
            {
                changes = false;
                List<int> candidates = ToolsImageRaster.GetShell3D6C(skeleton_image).GetElementIndexesWithValue(true);


                if (candidates.Count != 0)
                {

                    // get 26 - neighbourhoods of candidates in volume
                    List<int[]> neigbourhoods = new List<int[]>();
                    List<int> end_point_indexes = new List<int>();
                    for (int candidate_index = 0; candidate_index < candidates.Count; candidate_index++)
                    {
                        int[] element_neigbour_array = new int[26];
                        bool[] element_neigbour_true_array = new bool[26];
                        topology.ElementNeighboursRBA(candidates[candidate_index], element_neigbour_array);
                        //int count = 0;
                        for (int neigbour_index = 0; neigbour_index < topology.MaximumConnectivity; neigbour_index++)
                        {
                            //TODO something here
                        }
                    }

                    // remove all endpoints (exactly one nb) from list              
                    neigbourhoods.RemoveAt(end_point_indexes);
                    candidates.RemoveAt(end_point_indexes);


                    // remove all non-Euler - invariant points from list
                    List<int> euler_variate_indexes = new List<int>();
                    for (int candidate_index = 0; candidate_index < candidates.Count; candidate_index++)
                    {
                        if (compute_euler_characteristic(neigbourhoods[candidate_index], euler_table))
                        {
                            euler_variate_indexes.Add(candidate_index);
                        }
                    }
                    neigbourhoods.RemoveAt(euler_variate_indexes);
                    candidates.RemoveAt(euler_variate_indexes);

                    List<int> is_simple_indexes = new List<int>();
                    for (int candidate_index = 0; candidate_index < candidates.Count; candidate_index++)
                    {
                        if (is_simple(neigbourhoods[candidate_index]))
                        {
                            is_simple_indexes.Add(candidate_index);
                        }
                    }
                    // remove all non-simple points from list
                    candidates.RemoveAt(is_simple_indexes);

                    //get subscript indices of remaining candidates
                    //TODO [x, y, z] = ind2sub(padded_image_size, candidates);

                    // if any candidates left: divide into 8 independent subvolumes TODO this is silly
                    if (candidates.Count != 0)
                    {
                        // do re-checking for all points in each subvolume
                        List<int> removal_indexes = new List<int>(); //sub2ind(padded_image_size, x(idx), y(idx), z(idx));
                        skeleton_image.SetElementValues(removal_indexes, false); // remove points
                        List<int[]> nh = GetNeigbourhoods(skeleton_image, removal_indexes);
                        List<int> di_rc = new List<int>();
                        for (int index = 0; index < nh.Count; index++)
                        {
                            if (!is_simple(nh[index]))
                            {
                                di_rc.Add(index);
                            }
                        }

                        if (di_rc.Count != 0)
                        {
                            changes = true; //% at least one voxel removed
                            skeleton_image.SetElementValues(di_rc, true); // TODO this seems weird
                        }
                    }
                }            
            }
            return skeleton_image;
        }

        private List<int[]> GetNeigbourhoods(ImageRaster3D<bool> skeleton_image, List<int> removal_indexes)
        {
            List<int[]> neigbourhoods = new List<int[]>();

            return neigbourhoods;
        }

        private static int[] BuildEulerLookupTable()
        {
            int[] euler_look_up_table = new int[256];
            euler_look_up_table[1] = 1;
            euler_look_up_table[3] = -1;
            euler_look_up_table[5] = -1;
            euler_look_up_table[7] = 1;
            euler_look_up_table[9] = -3;
            euler_look_up_table[11] = -1;
            euler_look_up_table[13] = -1;
            euler_look_up_table[15] = 1;
            euler_look_up_table[17] = -1;
            euler_look_up_table[19] = 1;
            euler_look_up_table[21] = 1;
            euler_look_up_table[23] = -1;
            euler_look_up_table[25] = 3;
            euler_look_up_table[27] = 1;
            euler_look_up_table[29] = 1;
            euler_look_up_table[31] = -1;
            euler_look_up_table[33] = -3;
            euler_look_up_table[35] = -1;
            euler_look_up_table[37] = 3;
            euler_look_up_table[39] = 1;
            euler_look_up_table[41] = 1;
            euler_look_up_table[43] = -1;
            euler_look_up_table[45] = 3;
            euler_look_up_table[47] = 1;
            euler_look_up_table[49] = -1;
            euler_look_up_table[51] = 1;

            euler_look_up_table[53] = 1;
            euler_look_up_table[55] = -1;
            euler_look_up_table[57] = 3;
            euler_look_up_table[59] = 1;
            euler_look_up_table[61] = 1;
            euler_look_up_table[63] = -1;
            euler_look_up_table[65] = -3;
            euler_look_up_table[67] = 3;
            euler_look_up_table[69] = -1;
            euler_look_up_table[71] = 1;
            euler_look_up_table[73] = 1;
            euler_look_up_table[75] = 3;
            euler_look_up_table[77] = -1;
            euler_look_up_table[79] = 1;
            euler_look_up_table[81] = -1;
            euler_look_up_table[83] = 1;
            euler_look_up_table[85] = 1;
            euler_look_up_table[87] = -1;
            euler_look_up_table[89] = 3;
            euler_look_up_table[91] = 1;
            euler_look_up_table[93] = 1;
            euler_look_up_table[95] = -1;
            euler_look_up_table[97] = 1;
            euler_look_up_table[99] = 3;
            euler_look_up_table[101] = 3;
            euler_look_up_table[103] = 1;

            euler_look_up_table[105] = 5;
            euler_look_up_table[107] = 3;
            euler_look_up_table[109] = 3;
            euler_look_up_table[111] = 1;
            euler_look_up_table[113] = -1;
            euler_look_up_table[115] = 1;
            euler_look_up_table[117] = 1;
            euler_look_up_table[119] = -1;
            euler_look_up_table[121] = 3;
            euler_look_up_table[123] = 1;
            euler_look_up_table[125] = 1;
            euler_look_up_table[127] = -1;
            euler_look_up_table[129] = -7;
            euler_look_up_table[131] = -1;
            euler_look_up_table[133] = -1;
            euler_look_up_table[135] = 1;
            euler_look_up_table[137] = -3;
            euler_look_up_table[139] = -1;
            euler_look_up_table[141] = -1;
            euler_look_up_table[143] = 1;
            euler_look_up_table[145] = -1;
            euler_look_up_table[147] = 1;
            euler_look_up_table[149] = 1;
            euler_look_up_table[151] = -1;
            euler_look_up_table[153] = 3;
            euler_look_up_table[155] = 1;

            euler_look_up_table[157] = 1;
            euler_look_up_table[159] = -1;
            euler_look_up_table[161] = -3;
            euler_look_up_table[163] = -1;
            euler_look_up_table[165] = 3;
            euler_look_up_table[167] = 1;
            euler_look_up_table[169] = 1;
            euler_look_up_table[171] = -1;
            euler_look_up_table[173] = 3;
            euler_look_up_table[175] = 1;
            euler_look_up_table[177] = -1;
            euler_look_up_table[179] = 1;
            euler_look_up_table[181] = 1;
            euler_look_up_table[183] = -1;
            euler_look_up_table[185] = 3;
            euler_look_up_table[187] = 1;
            euler_look_up_table[189] = 1;
            euler_look_up_table[191] = -1;
            euler_look_up_table[193] = -3;
            euler_look_up_table[195] = 3;
            euler_look_up_table[197] = -1;
            euler_look_up_table[199] = 1;
            euler_look_up_table[201] = 1;
            euler_look_up_table[203] = 3;
            euler_look_up_table[205] = -1;
            euler_look_up_table[207] = 1;

            euler_look_up_table[209] = -1;
            euler_look_up_table[211] = 1;
            euler_look_up_table[213] = 1;
            euler_look_up_table[215] = -1;
            euler_look_up_table[217] = 3;
            euler_look_up_table[219] = 1;
            euler_look_up_table[221] = 1;
            euler_look_up_table[223] = -1;
            euler_look_up_table[225] = 1;
            euler_look_up_table[227] = 3;
            euler_look_up_table[229] = 3;
            euler_look_up_table[231] = 1;
            euler_look_up_table[233] = 5;
            euler_look_up_table[235] = 3;
            euler_look_up_table[237] = 3;
            euler_look_up_table[239] = 1;
            euler_look_up_table[241] = -1;
            euler_look_up_table[243] = 1;
            euler_look_up_table[245] = 1;
            euler_look_up_table[247] = -1;
            euler_look_up_table[249] = 3;
            euler_look_up_table[251] = 1;
            euler_look_up_table[253] = 1;
            euler_look_up_table[255] = -1;
            return euler_look_up_table;
        }

        private  bool is_simple(int [] neigbourhood)
        { 

            // copy neighbors for labeling
            bool is_simple = true;

            // Cut out the middle part  not needed here
            int[] cube = neigbourhood;
            //cube[1:13)=neigbourhoods(:,1:13)';
            //cube[14:26)=neigbourhoods(:,15:27)';

            int label = 2;

            // for all points in the neighborhood
            for (int i = 0; i < 26; i++)
            {

                bool idx_1 = cube[i] != -1;
                bool idx_2 = is_simple;
                bool idx = idx_1 && idx_2;

                if (idx)
                {
                    // start recursion with any octant that contains the point i
                    switch (i)
                    {

                        case 0: case 1: case 3: case 4: case 9: case 10: case 12:
                            p_oct_label(1, label, cube);
                            break;
                        case 2: case 5: case 11:  case 13:
                            p_oct_label(2, label, cube);
                            break;
                        case 6: case 7: case 14: case 15:
                            p_oct_label(3, label, cube);
                            break;
                        case 8: case 16:
                            p_oct_label(4, label, cube);
                            break;
                        case 17: case 18: case 20: case 21:
                            p_oct_label(5, label, cube);
                            break;
                        case 19: case 22:
                            p_oct_label(6, label, cube);
                            break;
                        case 23: case 24:
                            p_oct_label(7, label, cube);
                            break;
                        case 25:
                            p_oct_label(8, label, cube);
                            break;
                        default:
                    throw new Exception("out of bounds");
                    }
                }
            }

            label = label + 1;
            return label >= 4;
        }

        public bool compute_euler_characteristic(int[] neigbourhoods, int[] euler_table)
        {
            // Calculate Euler characteristic for each octant and sum up
            int euler_characteristic = 0;
            // Octant SWU
            int n = 1;
            if (neigbourhoods[24] != -1) n = n | 128;
            if (neigbourhoods[25] != -1) n = n | 64;
            if (neigbourhoods[15] != -1) n = n | 32;
            if (neigbourhoods[16] != -1) n = n | 16;
            if (neigbourhoods[21] != -1) n = n | 8;
            if (neigbourhoods[22] != -1) n = n | 4;
            if (neigbourhoods[12] != -1) n = n | 2;
            euler_characteristic += euler_table[n];
            // Octant SEU
            n = 1;
            if (neigbourhoods[26] != -1) n = n | 128;
            if (neigbourhoods[23] != -1) n = n | 64;
            if (neigbourhoods[17] != -1) n = n | 32;
            if (neigbourhoods[14] != -1) n = n | 16;
            if (neigbourhoods[25] != -1) n = n | 8;
            if (neigbourhoods[22] != -1) n = n | 4;
            if (neigbourhoods[16] != -1) n = n | 2;
            euler_characteristic += euler_table[n];
            // Octant NWU
            n = 1;
            if (neigbourhoods[18] != -1) n = n | 128;
            if (neigbourhoods[21] != -1) n = n | 64;
            if (neigbourhoods[9] != -1) n = n | 32;
            if (neigbourhoods[12] != -1) n = n | 16;
            if (neigbourhoods[19] != -1) n = n | 8;
            if (neigbourhoods[22] != -1) n = n | 4;
            if (neigbourhoods[10] != -1) n = n | 2;
            euler_characteristic += euler_table[n];
            // Octant NEU
            n = 1;
            if (neigbourhoods[20] != -1) n = n | 128;
            if (neigbourhoods[23] != -1) n = n | 64;
            if (neigbourhoods[19] != -1) n = n | 32;
            if (neigbourhoods[22] != -1) n = n | 16;
            if (neigbourhoods[11] != -1) n = n | 8;
            if (neigbourhoods[14] != -1) n = n | 4;
            if (neigbourhoods[10] != -1) n = n | 2;
            euler_characteristic += euler_table[n];
            // Octant SWB
            n = 1;
            if (neigbourhoods[6] != -1) n = n | 128;
            if (neigbourhoods[15] != -1) n = n | 64;
            if (neigbourhoods[7] != -1) n = n | 32;
            if (neigbourhoods[16] != -1) n = n | 16;
            if (neigbourhoods[3] != -1) n = n | 8;
            if (neigbourhoods[12] != -1) n = n | 4;
            if (neigbourhoods[4] != -1) n = n | 2;
            euler_characteristic += euler_table[n];
            // Octant SEB
            n = 1;
            if (neigbourhoods[8] != -1) n = n | 128;
            if (neigbourhoods[7] != -1) n = n | 64;
            if (neigbourhoods[17] != -1) n = n | 32;
            if (neigbourhoods[16] != -1) n = n | 16;
            if (neigbourhoods[5] != -1) n = n | 8;
            if (neigbourhoods[4] != -1) n = n | 4;
            if (neigbourhoods[14] != -1) n = n | 2;
            euler_characteristic += euler_table[n];
            // Octant NWB
            n = 1;
            if (neigbourhoods[0] != -1) n = n | 128;
            if (neigbourhoods[9] != -1) n = n | 64;
            if (neigbourhoods[3] != -1) n = n | 32;
            if (neigbourhoods[12] != -1) n = n | 16;
            if (neigbourhoods[1] != -1) n = n | 8;
            if (neigbourhoods[10] != -1) n = n | 4;
            if (neigbourhoods[4] != -1) n = n | 2;
            euler_characteristic += euler_table[n];
            //Octant NEB
            n = 1;
            if (neigbourhoods[2] != -1) n = n | 128;
            if (neigbourhoods[1] != -1) n = n | 64;
            if (neigbourhoods[11] != -1) n = n | 32;
            if (neigbourhoods[10] != -1) n = n | 16;
            if (neigbourhoods[5] != -1) n = n | 8;
            if (neigbourhoods[4] != -1) n = n | 4;
            if (neigbourhoods[14] != -1) n = n | 2;
            euler_characteristic += euler_table[n];

            return euler_characteristic == 0;
        }

        public void CheckCube(int[] cube, int label, int index, int[] octants)
        {
            if (cube[index] == 1)
            {
                cube[index] = label;
                for (int octant_index = 0; octant_index < octants.Length; octant_index++)
                {
                    p_oct_label(octants[octant_index], label, cube);
                }
            }
        }

        public void p_oct_label(int octant, int label, int[] cube)
        {
            //% check if there are points in the octant with value 1
            if (octant == 1)
            {
                // set points in this octant to current label
                // and recurseive labeling of adjacent octants    
                CheckCube(cube, label, 1, new int[] {});
                CheckCube(cube, label, 2, new int[] {2});
                CheckCube(cube, label, 4, new int[] {3});
                CheckCube(cube, label, 5, new int[] {2, 3,4 });
                CheckCube(cube, label, 10, new int[] { 5});
                CheckCube(cube, label, 11, new int[] { 2, 5,6 });
                CheckCube(cube, label, 13, new int[] { 3, 5, 7 });
            }

            if (octant == 2)
            {
                CheckCube(cube, label, 2, new int[] { 1 });
                CheckCube(cube, label, 5, new int[] { 1, 3, 4});
                CheckCube(cube, label, 11, new int[] { 1, 5, 6 });
                CheckCube(cube, label, 3, new int[] { });
                CheckCube(cube, label, 6, new int[] {4 });
                CheckCube(cube, label, 12, new int[] { 6 });
                CheckCube(cube, label, 14, new int[] { 4,6,8 });
            }

            if (octant == 3)
            {
                CheckCube(cube, label, 4, new int[] {1});
                CheckCube(cube, label, 5, new int[] { 1 ,2,4});
                CheckCube(cube, label, 13, new int[] { 1, 5, 7 });
                CheckCube(cube, label, 7, new int[] { });
                CheckCube(cube, label, 8, new int[] {4 });
                CheckCube(cube, label, 15, new int[] { 7 });
                CheckCube(cube, label, 16, new int[] { 4,7,8 });
            }

            if (octant == 4)
            {
                CheckCube(cube, label, 5, new int[] { 1, 2, 3 });
                CheckCube(cube, label, 6, new int[] { 2 });
                CheckCube(cube, label, 14, new int[] { 2, 6, 8 });
                CheckCube(cube, label, 8, new int[] {3 });
                CheckCube(cube, label, 16, new int[] { 3,7,8 });
                CheckCube(cube, label, 9, new int[] { });
                CheckCube(cube, label, 17, new int[] {8 });
            }
    
            if( octant==5 )
            {
                CheckCube(cube, label, 10, new int[] {1 });
                CheckCube(cube, label, 11, new int[] { 1,2,6 });
                CheckCube(cube, label, 13, new int[] { 1, 3, 7 });
                CheckCube(cube, label, 18, new int[] { });
                CheckCube(cube, label, 19, new int[] {6 });
                CheckCube(cube, label, 21, new int[] { 7 });
                CheckCube(cube, label, 22, new int[] { 6,7,8 });
            }

            if( octant==6 )
            {
                CheckCube(cube, label, 11, new int[] { 1,2,5 });
                CheckCube(cube, label, 12, new int[] { 2 });
                CheckCube(cube, label, 14, new int[] { 2,4,8 });
                CheckCube(cube, label, 19, new int[] {5 });
                CheckCube(cube, label, 22, new int[] { 5,7,8 });
                CheckCube(cube, label, 20, new int[] { });
                CheckCube(cube, label, 23, new int[] { 8});
            }

            if (octant == 7)
            {

                CheckCube(cube, label, 13, new int[] { 1, 3, 5 });
                CheckCube(cube, label, 15, new int[] { 3 });
                CheckCube(cube, label, 16, new int[] { 3, 4, 8 });
                CheckCube(cube, label, 21, new int[] { 5 });
                CheckCube(cube, label, 22, new int[] { 5, 6, 8 });
                CheckCube(cube, label, 24, new int[] { });
                CheckCube(cube, label, 25, new int[] { 8 });
            }

            if (octant == 8)
            {
                CheckCube(cube, label, 14, new int[] { 2, 4, 6 });
                CheckCube(cube, label, 16, new int[] { 3, 4, 7 });
                CheckCube(cube, label, 17, new int[] { 4 });
                CheckCube(cube, label, 22, new int[] { 5, 6, 7 });
                CheckCube(cube, label, 17, new int[] { 4 });
                CheckCube(cube, label, 23, new int[] { 6 });
                CheckCube(cube, label, 25, new int[] { 7 });
                CheckCube(cube, label, 26, new int[] { 7 });
            }
        }
    }
}
