using System;

namespace MailSecure.Cbc
{
	public class CbcCypher
	{
		public uint blockSize { get; set; }

		public CbcCypher (uint blockSize = 32)
		{
			this.blockSize = blockSize;
		}

		public byte[] encrypt(byte[] data, byte[] key) {
			byte[] iv = BytesGenerator.getInstance().generateRandom(blockSize);
			byte[] result = new byte[data.Length + blockSize];

			for (uint i = 0; i < blockSize; i++) {
				result [i] = iv [i];
			}

			for (uint i = 0; i < data.Length; i+=blockSize) {
				for (uint j = 0; j < blockSize && i + j < data.Length; j++) {
					result [blockSize + i + j] = (byte) (data [i + j] ^ iv [j] ^ key [j]);
					iv[j] = result [blockSize + i + j];
				}
			}
			return result;
		}

		public byte[] decrypt(byte[] data, byte[] key) {
			byte[] iv = new byte[blockSize];
			byte[] ivTemp = new byte[blockSize];
			byte[] result = new byte[data.Length - blockSize];

			for (uint i = 0; i < blockSize; i++) {
				iv [i] = data [i];
			}

			for (uint i = 0; i < result.Length; i+=blockSize) {
				for (uint j = 0; j < blockSize && i + j < result.Length ; j++) {
					ivTemp[j] = data [blockSize + i + j];
					result [i + j] = (byte) (data [blockSize + i + j] ^ iv [j] ^ key [j]);
					iv [j] = ivTemp [j];
				}
			}
			return result;
		}
			
	}
}

