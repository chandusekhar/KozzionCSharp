
namespace KozzionMathematics.Datastructure.Matrix
{
    public class SVD<MatrixType>
    {
        // https://en.wikipedia.org/wiki/Singular_value_decomposition
        // VT AKA WT
        public AMatrix<MatrixType> U { get; private set; }
        public AMatrix<MatrixType> S { get; private set; }
        public AMatrix<MatrixType> VT { get; private set; }

        public SVD(AMatrix<MatrixType> u, AMatrix<MatrixType> s, AMatrix<MatrixType> vt)
        {
            this.U = u;
            this.S = s;
            this.VT = vt;
        }
     }
}