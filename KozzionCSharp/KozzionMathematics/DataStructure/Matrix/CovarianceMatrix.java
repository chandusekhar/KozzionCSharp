package com.kozzion.library.math.matrix;

import cern.colt.function.DoubleDoubleFunction;
import cern.colt.function.DoubleFunction;
import cern.colt.matrix.DoubleFactory2D;
import cern.colt.matrix.DoubleMatrix1D;
import cern.colt.matrix.DoubleMatrix2D;
import cern.colt.matrix.linalg.Algebra;
import cern.colt.matrix.linalg.CholeskyDecomposition;
import cern.colt.matrix.linalg.EigenvalueDecomposition;
import cern.colt.matrix.linalg.SingularValueDecomposition;
import cern.jet.math.Functions;

import com.kozzion.library.core.utility.CollectionTools;
import com.kozzion.library.math.tools.MathToolsDoubleArray;
import com.kozzion.library.math.tools.MathToolsFloatArray;

public class CovarianceMatrix
    extends
        MatrixReal
{
    private static double TOLERANCE = 0.000000001;

    public CovarianceMatrix(
        float [][] data)
    {
        super(MathToolsFloatArray.covariance(data));
    }

    public MatrixReal compute_root()
    {
        CholeskyDecomposition decomposition = new CholeskyDecomposition(d_matrix);
        if (decomposition.isSymmetricPositiveDefinite())
        {
            return new MatrixReal(decomposition.getL());
        }
        else
        {
            // Check if we can rescue the matrix (if is is close to posdef)
            System.out.println("Old matrix");
            CollectionTools.print(d_matrix.toArray());
            EigenvalueDecomposition evd = new EigenvalueDecomposition(d_matrix);
            System.out.println("Old eigens");
            CollectionTools.print(evd.getRealEigenvalues().toArray());
            // change smalles eigen values so they are zero
            DoubleMatrix2D e_matrix = evd.getD();
            int size = e_matrix.rows();

            // boost the eigen values to make matrix posdef
            for (int index = 0; index < size; index++)
            {

                if (e_matrix.get(index, index) < 0)
                {
                    System.out.println(e_matrix.get(index, index) + " " + index);
                    e_matrix.set(index, index, TOLERANCE);
                }
            }

            Algebra algebra = new Algebra();
            DoubleMatrix2D reg = algebra.mult(algebra.mult(evd.getV(), e_matrix), algebra.transpose(evd.getV()));
            // make reg symetric
            for (int index_row = 0; index_row < size; index_row++)
            {
                for (int index_column = index_row + 1; index_column < size; index_column++)
                {
                    // Used the min to keep reg posdef
                    double min = Math.min(reg.get(index_row, index_column), reg.get(index_row, index_column));

                    reg.set(index_row, index_column, min);
                    reg.set(index_column, index_row, min);

                }
            }

            EigenvalueDecomposition evd2 = new EigenvalueDecomposition(reg);
            System.out.println("New matrix");
            CollectionTools.print(reg.toArray());
            System.out.println("New eigens");
            CollectionTools.print(evd2.getRealEigenvalues().toArray());

            CholeskyDecomposition regularized_decomposition = new CholeskyDecomposition(reg);
            if (regularized_decomposition.isSymmetricPositiveDefinite())
            {
                return new MatrixReal(regularized_decomposition.getL());
            }
            else
            {
                throw new RuntimeException("Could not save matrix");
            }

        }
    }

    protected IMatrixReal closest_spd(IMatrixReal matrix)
    {

        // % test for a square matrix A
        // [r,c] = size(A);

        if (matrix.is_square())
        {
            throw new RuntimeException("Must be a square matrix.");
        }

        if (matrix.size_rows() == 1)
        {
            if (matrix.get_element(0, 0) <= 0)
            {
                matrix = matrix.copy();
                matrix.set_element(0, 0, (float) TOLERANCE);
            }
            else
            {
                return matrix;
            }
        }
        int size = matrix.size_rows();
        DoubleMatrix2D A = matrix.get_double_matrix_2D();

        Algebra algebra = new Algebra();

        // symmetrize A into B
        // B = (A + A')/2;

        DoubleMatrix2D B = A.assign(algebra.transpose(A), Functions.plus).assign(Functions.div(2.0));

        // Compute the symmetric polar factor of B. Call it H.
        // Clearly H is itself SPD.

        // [U,Sigma,V] = svd(B);
        // H = V*Sigma*V';
        SingularValueDecomposition svd = new SingularValueDecomposition(A);
        DoubleMatrix2D V = svd.getV();
        DoubleMatrix2D S = svd.getS();
        DoubleMatrix2D Vt = algebra.transpose(V);
        DoubleMatrix2D H = algebra.mult(algebra.mult(V, S), Vt);

        // % get Ahat in the above formula
        // Ahat = (B+H)/2;
        DoubleMatrix2D Ahat = B.assign(H, Functions.plus).assign(Functions.div(2.0));

        // % ensure symmetry
        // Ahat = (Ahat + Ahat')/2;
        Ahat = Ahat.assign(algebra.transpose(Ahat), Functions.plus).assign(Functions.div(2.0));

        // % test that Ahat is in fact PD. if it is not so, then tweak it just a bit.
        int k = 1;
        CholeskyDecomposition decomposition = new CholeskyDecomposition(Ahat);
        while (!decomposition.isSymmetricPositiveDefinite())
        {

            // % Ahat failed the chol test. It must have been just a hair off,
            // % due to floating point trash, so it is simplest now just to
            // % tweak by adding a tiny multiple of an identity matrix.
            // mineig = min(eig(Ahat));
            // Ahat = Ahat + (-mineig*k.^2 + eps(mineig))*eye(size(A));
            DoubleMatrix1D eigens = new EigenvalueDecomposition(Ahat).getRealEigenvalues();
            double mineig = DoubleMatrixTools.min_element(eigens);

            DoubleMatrix2D eye = DoubleFactory2D.dense.identity(size);
            eye.assign(Functions.mult(mineig + TOLERANCE));
            eye.assign(Functions.plus(-mineig * k * k));
            Ahat = Ahat.assign(eye, Functions.plus);
            k++;
        }
        return new MatrixReal(Ahat);
    }
}
