using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using CSJ2K.Util;
using eidpt;

namespace LeitorCC
{
	public class CCReaderAPI
	{

		#region private methods

		private string Convert2UTF8(string input)
        {
            return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(input));
        }

		private void ReadPicture(Citizen citizen)
        {
            var pictureData = Pteid.GetPic();
            if (pictureData == null || pictureData.picture == null) return;

	        var j2kImg = CSJ2K.J2kImage.FromBytes(pictureData.picture);
	        var bmap = j2kImg.As<Bitmap>();

			var imgConverter = new ImageConverter();
			var bitmapBytes = (byte[]) imgConverter.ConvertTo(bmap, typeof(byte[]));

			citizen.Picture = Convert.ToBase64String(bitmapBytes);
        }

		private void ReadCertificates(Citizen citizen)
        {
            PteidCertif[] pCerts = Pteid.GetCertificates();
            if (pCerts == null) return;

            var certificates = new List<Certificate>();
            foreach (PteidCertif pCert in pCerts)
            {
                var cert = new X509Certificate2(pCert.certif);
                certificates.Add(new Certificate()
                {
                    Label                   = pCert.certifLabel,
                    ExpirationDateString    = cert.GetExpirationDateString(),
                    KeyAlgorithm            = cert.GetKeyAlgorithm(),
                    PublicKey               = cert.GetPublicKeyString()
                });
            }
			citizen.Certificates = certificates;
        }


		private void ReadIDField(Citizen citizen)
        {
            var id = Pteid.GetID();
            citizen.Number = Convert2UTF8(id.cardNumber);
            citizen.CardNumber = Convert2UTF8(id.numBI);
            citizen.Nationality = Convert2UTF8(id.nationality);
            citizen.Genre = Convert2UTF8(id.sex);
            citizen.Country = Convert2UTF8(id.country);
            citizen.HealthSystemNumber = Convert2UTF8(id.numSNS);
            citizen.TaxNumber = Convert2UTF8(id.numNIF);
            citizen.LastName = Convert2UTF8(id.name);
            citizen.FirstName = Convert2UTF8(id.firstname);
			citizen.CardExpirationDate = Convert2UTF8(id.validityDate);
            citizen.BirthDate = Convert2UTF8(id.birthDate);
        }

		private void ReadAddress(Citizen citizen)
	    {
		    var addr = Pteid.GetAddr();
		    citizen.Address.Street		= Convert2UTF8(addr.street);
		    citizen.Address.District	= Convert2UTF8(addr.districtDesc);
			citizen.Address.Building	= Convert2UTF8(addr.building);
		    citizen.Address.Country		= Convert2UTF8(addr.country);
		    citizen.Address.Type		= Convert2UTF8(addr.addrType);
			citizen.Address.Parish		= Convert2UTF8(addr.freguesiaDesc);
			citizen.Address.Locality	= Convert2UTF8(addr.locality);
			citizen.Address.Door		= Convert2UTF8(addr.door);
		    citizen.Address.Floor		= Convert2UTF8(addr.floor);
			citizen.Address.ZipCode1	= Convert2UTF8(addr.cp4);
			citizen.Address.ZipCode2	= Convert2UTF8(addr.cp3);
	    }

		#endregion

		#region public methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="readAddress">Reading address will promp for password, to avoid it don't read the address.</param>
		/// <returns></returns>
		public Citizen Read(bool readAddress = true)
		{
			// enable bitmap convertion for CSJ2K
			BitmapImageCreator.Register();

			// connect to card
			Pteid.Init(null);
            Pteid.SetSODChecking(0);

			// read data
			var citizen = new Citizen();
			ReadIDField(citizen);
			ReadCertificates(citizen);
			ReadPicture(citizen);

			if (readAddress)
			{
				ReadAddress(citizen); // will request address password
			}

			return citizen;
		}

		#endregion
	}
}
