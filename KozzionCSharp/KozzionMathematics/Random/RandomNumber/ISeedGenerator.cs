namespace KozzionMathematics.random.random_number
{

    public interface ISeedGenerator
    {

        byte[] generate_seed(int seedSizeBytes);

    }
}