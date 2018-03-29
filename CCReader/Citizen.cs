using System.Collections.Generic;

namespace LeitorCC
{
	public class Citizen
	{
		public string Number { get; set; }
		public string CardNumber { get; set; }
		public string Nationality { get; set; }
		public string CardExpirationDate { get; set; }
		public string Genre { get; set; }
		public string Country { get; set; }
		public string HealthSystemNumber { get; set; }
		public string TaxNumber { get; set; }
		public string BirthDate { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }

		/// <summary>
		/// base 64 picture
		/// </summary>
		public string Picture { get; set; }
		public List<Certificate> Certificates { get; set; }
		public Address Address { get; set; }

		public Citizen()
		{
			Address = new Address();
			Certificates = new List<Certificate>();
		}

	}
}