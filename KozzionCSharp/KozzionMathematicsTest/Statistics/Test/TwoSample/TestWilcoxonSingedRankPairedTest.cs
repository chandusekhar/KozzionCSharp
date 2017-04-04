

using KozzionMathematics.Statistics.Test.TwoSample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KozzionMathematicsTest.Statistics.Test
{
    [TestClass]
    public class TestWilcoxonSingedRankPairedTest
    {
        [TestMethod]
        public void TestTestWilcoxonSingedRankPairedP0()
        {
            // Larsen Marc 4Th editiopn P821
            double[] sample_0 = new double[] { 4.67, 3.50, 3.50, 3.88, 3.94, 4.88, 4.00, 4.40, 4.41, 4.11, 3.45, 4.29, 4.25, 4.18, 4.65};
            double[] sample_1 = new double[] { 4.36, 3.64, 4.00, 3.26, 4.06, 4.58, 3.52, 3.66, 4.43, 4.28, 4.25, 4.00, 5.00, 3.85, 4.18};
            double p_value = TestWilcoxonSingedRankPaired.TestStatic(sample_0, sample_1);
            Assert.IsTrue(p_value < 0.29);
            Assert.IsTrue(0.28 < p_value);
        }


        [TestMethod]
        public void TestTestWilcoxonSingedRankPairedP1()
        {
            // Data from correct 2
            double[] margin_95_manu = new double[] { 1.67, 1.43, 2.61, 0.80, 1.23, 2.76, 3.01 };
            double[] margin_95_auto = new double[] { 3.75, 1.88, 5.19, 2.6, 4.06, 2.08, 2.63 };

            double[] margin_100_manu = new double[] { 3.77, 4.02, 4.44, 2.49, 3.06, 8.25, 6.32 };
            double[] margin_100_auto = new double[] { 7.54, 3.51, 8.24, 6.43, 8.85, 7.17, 5.64 };

            double[] volume_gtv_manu = new double[] { 25.53, 10.86, 5.84, 13.43, 16.30, 60.25, 22.32 };
            double[] volume_gtv_auto = new double[] { 27.15, 7.20, 6.87, 10.55, 13.71, 72.94, 28.55 };

            double[] volume_ctv_manu = new double[] { 68.16, 41.04, 31.98, 42.72, 52.50, 119.90, 63.40 };//none are off
            double[] volume_ctv_auto = new double[] { 110.22, 46.90, 52.59, 57.82, 82.69, 209.33, 134.46 };

            double[] sensitivity_gtv_manu = new double[] { 0.88, 0.82, 0.70, 0.95, 0.91, 0.91, 0.88 }; //none are off
            double[] sensitivity_gtv_auto = new double[] { 0.6168, 0.6711, 0.2713, 0.7338, 0.7102, 0.6924, 0.7624 };

            double[] ppv_gtv_manu = new double[] { 0.65, 0.37, 0.70, 0.44, 0.63, 0.62, 0.68 }; //one is off
            double[] ppv_gtv_auto = new double[] { 0.43, 0.46, 0.23, 0.42, 0.57, 0.41, 0.45 };

            double margin_95_p_value = TestWilcoxonSingedRankPaired.TestStatic(margin_95_manu, margin_95_auto);
            double margin_100_p_value = TestWilcoxonSingedRankPaired.TestStatic(margin_100_manu, margin_100_auto);
            double volume_gtv_p_value = TestWilcoxonSingedRankPaired.TestStatic(volume_gtv_manu, volume_gtv_auto);
            double volume_ctv_value = TestWilcoxonSingedRankPaired.TestStatic(volume_ctv_manu, volume_ctv_auto);
            double sensitivity_gtv_p_value = TestWilcoxonSingedRankPaired.TestStatic(sensitivity_gtv_manu, sensitivity_gtv_auto);
            double ppv_gtv_p_value = TestWilcoxonSingedRankPaired.TestStatic(ppv_gtv_manu, ppv_gtv_auto);

            Assert.IsTrue(0.004 < margin_95_p_value);
        }


        [TestMethod]
        public void TestTestWilcoxonSingedRankPairedP2()
        {

            double[] sensitivity_gtv_manu = new double[] { 0.88, 0.82, 0.70, 0.95, 0.91, 0.91, 0.88 };
            double[] ppv_gtv_manu         = new double[] { 0.65, 0.37, 0.70, 0.44, 0.63, 0.62, 0.68 };


            double[] sensitivity_gtv_auto = new double[] { 0.75, 0.77, 0.43, 0.79, 0.75, 0.72, 0.82 };
            double[] ppv_gtv_auto         = new double[] { 0.65, 0.44, 0.77, 0.49, 0.62, 0.42, 0.53 };

            double sensitivity_gtv_p_value = TestWilcoxonSingedRankPaired.TestStatic(sensitivity_gtv_manu, sensitivity_gtv_auto);
            double ppv_gtv_p_value = TestWilcoxonSingedRankPaired.TestStatic(ppv_gtv_manu, ppv_gtv_auto);

            Assert.IsTrue(0.004 < ppv_gtv_p_value);
        }

    }
}
