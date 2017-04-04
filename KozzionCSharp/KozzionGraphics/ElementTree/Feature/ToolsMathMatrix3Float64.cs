
using System;
using System.Text;
namespace KozzionGraphics.ElementTree.Feature
{
    public class ToolsMathMatrix3Float64
    {
        // Thesis Bennink....
        public static double[] eigenvalues_symetric(
            double[] m)
        {
            double[] eigenvalues = new double[3];
            eigenvalues_symetric_fill(m, eigenvalues);
            return eigenvalues;
        }

        // public static double [] eigenvalues_symetric_d(
        // double [] m)
        // {
        // double a = -m[0] - m[4] - m[8];
        // double b = m[0] * m[4] + m[4] * m[8] + m[8] * m[0] - m[1] * m[1] - m[2] * m[2] - m[5] * m[5];
        // double c = m[0] * m[5] * m[5] + m[4] * m[2] * m[2] + m[8] * m[1] * m[1] - m[0] * m[4] * m[8] - 2 * m[1] * m[2] *
        // m[5];
        // double p = a * a - 3 * b;
        // double p_sr = Math.sqrt(p);
        // double sr3 = Math.sqrt(3.0);
        // double q = (-27.0f / 2.0f) * c - a * a * a + (9.0f / 2.0f) * a * b;
        // System.out.println((((1.0f / 4.0f) * b * b * (p - b)) + c * (q + ((27.0f / 4.0f) * c))));
        // System.out.println((((1.0f / 4.0f) * b * b * (p - b)) + c * (q + ((27.0f / 4.0f) * c))));
        // System.out.println((((1.0f / 4.0f) * b * b * (p - b)) + c * (q + ((27.0f / 4.0f) * c))));
        // System.out.println("");
        // System.out.println(27 * (((1.0f / 4.0f) * b * b * (p - b)) + c * (q + ((27.0f / 4.0f) * c))));
        // System.out.println(Math.sqrt(27 * (((1.0f / 4.0f) * b * b * (p - b)) + c * (q + (27.0f / 4.0f) * c))));
        // double phi = Math.atan2(Math.sqrt(27 * (((1.0f / 4.0f) * b * b * (p - b)) + c * (q + (27.0f / 4.0f) * c))), q);
        //
        // System.out.println("a " + a);
        // System.out.println("b " + b);
        // System.out.println("c " + c);
        // System.out.println("p " + p);
        // System.out.println("q " + q);
        // System.out.println(phi);
        //
        // double [] eigenvalues = new double [3];
        // eigenvalues[0] = ((2 * p_sr * (Math.cos(phi)) - a) / 3.0f);
        // eigenvalues[1] = ((p_sr * (-Math.cos(phi) + sr3 * Math.sin(phi)) - a) / 3.0f);
        // eigenvalues[2] = ((p_sr * (-Math.cos(phi) - sr3 * Math.sin(phi)) - a) / 3.0f);
        // return eigenvalues;
        // }

        public static void eigenvalues_symetric_fill(
            double[] matrix,
            double[] result)
        {
            double a = -matrix[0] - matrix[4] - matrix[8];
            double b = matrix[0] * matrix[4] + matrix[4] * matrix[8] + matrix[8] * matrix[0] - matrix[1] * matrix[1] - matrix[2] * matrix[2] - matrix[5] * matrix[5];
            double c = matrix[0] * matrix[5] * matrix[5] + matrix[4] * matrix[2] * matrix[2] + matrix[8] * matrix[1] * matrix[1] - matrix[0] * matrix[4] * matrix[8] - matrix[1] * matrix[2] * matrix[5] - matrix[1] * matrix[2]
                * matrix[5];
            double p = Math.Max((a * a) - (3 * b), 0.0f);

            double q = ((-27.0f * c) - (2 * a * a * a) + (9.0f * a * b)) / 2.0f;
            double rt = Math.Max((27 / 4.0f) * ((b * b * (p - b)) + c * (4 * q + (27 * c))), 0.0f);

            double phi = (double)Math.Atan2(Math.Sqrt(rt), q) / 3.0f;

            double p_sr = (double)Math.Sqrt(p);
            double sr3 = (double)Math.Sqrt(3.0);

            double cos_phi = Math.Cos(phi);
            double sin_phi = Math.Sin(phi);

            result[0] = (double)((p_sr * (-cos_phi - (sr3 * sin_phi)) - a) / 3.0f);
            result[1] = (double)((p_sr * (-cos_phi + (sr3 * sin_phi)) - a) / 3.0f);
            result[2] = (double)((2 * p_sr * cos_phi - a) / 3.0f);
        }

        public static string ToString(
            double[] matrix)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("{{" + matrix[0] + " " + matrix[1] + " " + matrix[2] + "},");
            builder.AppendLine(" {" + matrix[3] + " " + matrix[4] + " " + matrix[5] + "},");
            builder.AppendLine(" {" + matrix[6] + " " + matrix[7] + " " + matrix[8] + "}}");
            return builder.ToString();
        }
    }
}