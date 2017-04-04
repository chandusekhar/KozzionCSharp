using System;
namespace KozzionMathematics.random.random_number
{
	public class SeedGeneratorDefault : ISeedGenerator
	{
		Random d_random;
    
		public SeedGeneratorDefault()
		{
			d_random = new Random();
		}

		public byte [] generate_seed(int seed_size)
		{
			byte [] bytes = new byte [seed_size];
			d_random.NextBytes(bytes);
			return bytes;
		}

	}
}