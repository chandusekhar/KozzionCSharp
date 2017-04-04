using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMathematics.DataStructure.BigDecimal;
using KozzionMathematics.Function;

namespace KozzionMathematics.Tools
{

    public class MathToolsMatrixRealFloat
    {
        public static void hessian(IFunction<float[], float> function_to_minimize,
            float[][] v, int vs, float[] dx, float[,] matrix)
        {
            int dimension_count = matrix.GetLength(0);
            float[] d = new float[dimension_count]; /* Finite differeces. */
            float[][] w = new float[4][]; /* v[vs] + d. */
            for (int i = 0; i < 4; i++)
            {
                w[i] = new float[dimension_count];
            }

            for (int i = 0; i < dimension_count; i++)
            {
                /*
                 * Use the span of the simplex in each dimension as finite difference for calculating the derivatives.
                 */
                /*
                 * int j; float mn, mx; mn = v[vs][i]; mx = v[vs][i]; for (j = 0; j < n+1; j++) { if (act[j] && v[j][i] <
                 * mn) mn = v[j][i]; else if (act[j] && v[j][i] > mx) mx = v[j][i]; } d[i] = 10 * (mx - mn);
                 */

                /* this is a different thing */
                d[i] = 0.01f * dx[i];
            }


            /*
             * Calculate the Hessian at v[vs]. This requires 2 N^2 + N function evaluations.
             */
            for (int p = 0; p < dimension_count; p++)
            {
                for (int q = p; q < dimension_count; q++)
                {
                    /*
                     * Initialize the vectors that are used for calculating the finite difference.
                     */
                    for (int i = 0; i < dimension_count; i++)
                    {
                        w[3][i] = w[2][i] = w[1][i] = w[0][i] = v[vs][i];
                    }

                    if (q == p)
                    {
                        /* Diagonal value; 2nd order derivative to p. */
                        w[0][p] += d[p];
                        w[2][p] -= d[p];
                        matrix[p, p] = ((function_to_minimize.Compute(w[0]) - (2.0f * function_to_minimize.Compute(w[1]))) + function_to_minimize
                            .Compute(w[2])) / (float)Math.Sqrt(Math.Abs(d[p]));
                        /* if (h[p][p] < 1e-12f) h[p][p] = 1e-12f; */
                    }
                    else
                    {
                        /* 2nd order partial derivative to p and q. */
                        w[0][p] += d[p];
                        w[0][q] += d[q];
                        w[1][p] += d[p];
                        w[1][q] -= d[q];
                        w[2][p] -= d[p];
                        w[2][q] += d[q];
                        w[3][p] -= d[p];
                        w[3][q] -= d[q];
                        matrix[p, q] = (0.25f * ((function_to_minimize.Compute(w[0]) - function_to_minimize.Compute(w[1]) - function_to_minimize
                            .Compute(w[2])) + function_to_minimize.Compute(w[3]))) / (d[q] * d[p]);
                        matrix[q, p] = matrix[p, q];
                    }
                }
            }
        }
        /**
         * Decompose an n*n symetric positive-definite matrix. Note that this function overwrites the input! Apply a
         * Cholesky decomposition as described in Numerical Recipes: http://www.nrbook.com/
         * 
         * @param matrix
         */
        public static void cholesky(float[,] matrix)
        {
            int n = matrix.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    float sum = matrix[i, j];
                    for (int k = i - 1; k >= 0; k--)
                    {
                        sum -= matrix[i, k] * matrix[j, k];
                    }
                    if (i == j)
                    {
                        if (sum < 0.0)
                        {
                            /* printf("Matrix is not positive-definite!\n"); */
                            matrix[i, i] = 0;
                        }
                        else
                        {
                            matrix[i, i] = (float)Math.Sqrt(sum);
                        }
                    }
                    else
                        if (matrix[i, i] > 0)
                    {
                        matrix[j, i] = sum / matrix[i, i];
                    }
                    else
                    {
                        matrix[j, i] = 0;
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    matrix[j, i] = 0;
                }
            }
        }


        public static void Invert(float[,] mat, float[,] inv)
        {
            int n = mat.GetLength(0);
            float sum = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (i == j)
                    {
                        sum = 1;
                    }
                    else
                    {
                        sum = 0;
                    }

                    for (int k = i - 1; k >= j; k--)
                    {
                        sum -= mat[i, k] * inv[j, k];
                    }
                    if (mat[i, i] > 0)
                    {
                        inv[j, i] = sum / mat[i, i];
                    }
                    else
                    {
                        inv[j, i] = 0;
                    }
                }
            }

            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (i < j)
                    {
                        sum = 0;
                    }
                    else
                    {
                        sum = inv[j, i];
                    }
                    for (int k = i + 1; k < n; k++)
                    {
                        sum -= mat[k, i] * inv[j, k];
                    }
                    if (mat[i, i] > 0)
                    {
                        inv[i, j] = inv[j, i] = sum / mat[i, i];
                    }
                    else
                    {
                        inv[i, j] = inv[j, i] = 0;
                    }
                }
            }
        }

        public static float MaxElementValue(float[,] matrix)
        {
            float max_value = float.MinValue;
            for (int row_index = 0; row_index < matrix.GetLength(0); row_index++)
            {
                for (int col_index = 0; col_index < matrix.GetLength(1); col_index++)
                {
                    float value = matrix[row_index, col_index];
                    if (max_value < value)
                    {
                        max_value = value;
                    }
                }
            }
            return max_value;
        }
    } 
}