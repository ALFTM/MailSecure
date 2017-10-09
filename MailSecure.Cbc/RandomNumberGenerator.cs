using System;

namespace MailSecure.Cbc
{
	public class RandomNumberGenerator : Singleton<RandomNumberGenerator>
	{

		public Random random { get; }

		public RandomNumberGenerator() {
			random = new Random(DateTime.Now.Millisecond);
		}

		public RandomNumberGenerator(int seed) {
			random = new Random(seed);
		}

	}
}

