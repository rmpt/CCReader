
using System;
using System.IO;
using eidpt;
using LeitorCC;
using NUnit.Framework;
using System.Drawing;
using System.Drawing.Imaging;

namespace LeitorCCTestes
{
	[TestFixture]
	public class CCReaderTests
	{
		[Test]
		public void TestMethod1()
		{
			var ccApi = new CCReaderAPI();

			Citizen citizen;
			try
			{
				citizen = ccApi.Read();
			}
			catch (PteidException e)
			{
				HandlePteidException(e);
				return;
			}

			Console.WriteLine(string.Format("First Name: {0}", citizen.FirstName));
            Console.WriteLine(string.Format("Last Name: {0}", citizen.LastName));
            Console.WriteLine(string.Format("Card Expiration Date: {0}", citizen.CardExpirationDate));
            Console.WriteLine(string.Format("Card Number: {0}", citizen.CardNumber));
            Console.WriteLine(string.Format("Genre: {0}", citizen.Genre));
            Console.WriteLine(string.Format("Health System Number: {0}", citizen.HealthSystemNumber));
            Console.WriteLine(string.Format("Nationality: {0}", citizen.Nationality));
            Console.WriteLine(string.Format("Country: {0}", citizen.Country));
            Console.WriteLine(string.Format("Number: {0}", citizen.Number));
            Console.WriteLine(string.Format("Tax Number: {0}", citizen.TaxNumber));
            Console.WriteLine(string.Format("Birth Date: {0}", citizen.BirthDate));
			Console.WriteLine(string.Format("Card Expiration Date: {0}", citizen.CardExpirationDate));
			Console.WriteLine(string.Format("Number of certificates: {0}", citizen.Certificates != null ? citizen.Certificates.Count : 0));

			Console.WriteLine("Address:");
			Console.WriteLine(string.Format("   Street: {0}", citizen.Address.Street));
			Console.WriteLine(string.Format("   District: {0}", citizen.Address.District));
			Console.WriteLine(string.Format("   Building: {0}", citizen.Address.Building));
			Console.WriteLine(string.Format("   Country: {0}", citizen.Address.Country));
			Console.WriteLine(string.Format("   Type: {0}", citizen.Address.Type));
			Console.WriteLine(string.Format("   Door: {0}", citizen.Address.Door));
			Console.WriteLine(string.Format("   Parish: {0}", citizen.Address.Parish));
			Console.WriteLine(string.Format("   Locality: {0}", citizen.Address.Locality));
			Console.WriteLine(string.Format("   Floor: {0}", citizen.Address.Floor));
			Console.WriteLine(string.Format("   ZipCode1: {0}", citizen.Address.ZipCode1));
			Console.WriteLine(string.Format("   ZipCode2: {0}", citizen.Address.ZipCode2));

			if(citizen.Picture != null)
            {
				Bitmap bitmap;
				using (var ms = new MemoryStream(Convert.FromBase64String(citizen.Picture)))
				{
					bitmap = new Bitmap(ms);
				}
				var ccPictureFilePath = @"C:\Temp\cc_picture.jpg";
				bitmap.Save(ccPictureFilePath, ImageFormat.Jpeg);
				Console.WriteLine(string.Format("Picture saved at: {0}", ccPictureFilePath));
            }

			Console.ReadLine(); // just to see the output
		}


		
		private static void HandlePteidException(PteidException ex)
        {
            var errorNumber = int.Parse(ex.Message.Split(new string[] { "Error Code : " }, StringSplitOptions.RemoveEmptyEntries)[0]);
            switch (errorNumber)
            {
                case 1101: // unknown
		            Console.WriteLine("unknown error");
                    break;
                case 1104: // could not read card
                    Console.WriteLine("Could not read card");
                    break;
                case 1109: // canceled by user
                    Console.WriteLine("canceled by user");
                    break;
                case 1210: // invalid card
					Console.WriteLine("invalid card");
                    break;
                default: // unexpected
                    Console.WriteLine("unexpected error");
                    break;
            }
        }
	}
}
