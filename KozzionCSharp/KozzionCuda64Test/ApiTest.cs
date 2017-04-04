
using kozzioncore.tools;
using ManagedCuda;
using ManagedCuda.CudaBlas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCuda64Test
{
    [TestClass]
    public class ApiTest
    {

        [TestMethod]
        public void TestAddCUDA()
        {
            //Alloc device memory
            int count_rows_a = 2;
            int count_shared = 2;
            int count_cols_b = 2;
            double alfa = 1.0;
            double beta = 1.0;
            CudaDeviceVariable<double> A = new double[] { 0, 0, 0, 0 };
            CudaDeviceVariable<double> B = new double[] { 0, 0, 0, 0 };
            CudaDeviceVariable<double> C = new double[] { 0, 0, 0, 0 };

            //Clean up
            CudaBlas blas = new CudaBlas();
            blas.Gemm(Operation.NonTranspose, Operation.NonTranspose,
                count_rows_a, count_shared, count_cols_b,
                alfa,
                A, count_rows_a,
                B, count_shared,
                beta,
                C, count_rows_a);


            //Copy data back to host
            double[] result = C;

            ToolsArray.print(result);
            Assert.AreEqual(0, result[0], 0.001);
            // Um right this should be moved
            blas.Dispose();
        }
    }
}
