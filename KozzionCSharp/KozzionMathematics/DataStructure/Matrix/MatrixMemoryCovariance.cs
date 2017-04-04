using KozzionMathematics.Algebra;

namespace KozzionMathematics.Datastructure.Matrix
{
    public class MatrixMemoryCovariance : MatrixKozzion<float>
    {
        private float[,] data;

        public MatrixMemoryCovariance(float[,] data):
            base(null,null)
        {
            // TODO: Complete member initialization
            this.data = data;
        }

 
     
    }
}
