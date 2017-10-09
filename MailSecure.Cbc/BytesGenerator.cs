using System;

namespace MailSecure.Cbc
{
	public class BytesGenerator : Singleton<BytesGenerator>
	{
		RandomNumberGenerator generator { get; }

		public BytesGenerator ()
		{
			this.generator = RandomNumberGenerator.getInstance();
		}

		public BytesGenerator (RandomNumberGenerator generator)
		{
			this.generator = generator;
		}

		public byte[] generateRandom(uint size) {
			byte[] bytes = new byte[size];

			generator.random.NextBytes(bytes);

			return bytes;
		}
	}
}

