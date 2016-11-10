using System;
using ZXing;
using ZXing.Common;

namespace SJCoinWallet
{
	public class QRCode
	{
		public QRCode()
		{
		}
/*
		public byte[] GenerateQR(String data)
		{

		}
*/			
		public byte[] GenerateQR(String data)
		{
			var writer = new BarcodeWriter
			{					
				Format = BarcodeFormat.QR_CODE,
				Options = new EncodingOptions
				{
					Height = 160,						
					Width = 160
				}
			};
			var bytes = writer.Write(data);

			System.Diagnostics.Debug.WriteLine("bytes=" + bytes.Length);
			return bytes;
		}
	}
}

