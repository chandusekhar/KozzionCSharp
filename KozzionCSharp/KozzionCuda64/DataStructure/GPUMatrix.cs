using ManagedCuda;
using ManagedCuda.BasicTypes;
using ManagedCuda.VectorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kozzioncuda
{
    public class MatrixCuda
    {
        public static void blaa()
        {
            int num  = 10;
            //NewContext creation
            CudaContext cntxt = new CudaContext();

            //Module loading from precompiled .ptx in a project output folder
            CUmodule cumodule = cntxt.LoadModule("kernel.ptx");

            //_Z9addKernelPf - function name, can be found in *.ptx file
            CudaKernel addWithCuda = new CudaKernel("_Z9addKernelPf", cumodule, cntxt);

            //Create device array for data
            CudaDeviceVariable<float> vec1_device = new CudaDeviceVariable<float>(num);

            //Create arrays with data
            float[] vec1 = new float[num];

            //Copy data to device
            vec1_device.CopyToDevice(vec1);

            //Set grid and block dimensions                       
            addWithCuda.GridDimensions = new dim3(8, 1, 1);
            addWithCuda.BlockDimensions = new dim3(512, 1, 1);

            //Run the kernel
            addWithCuda.Run(
                vec1_device.DevicePointer);

            //Copy data from device
            vec1_device.CopyToHost(vec1);
        }
    }
}
