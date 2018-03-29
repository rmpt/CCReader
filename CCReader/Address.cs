namespace LeitorCC
{
	public class Address
	{
		public string Street { get; set; }
		public string District { get; set; }
		public string Building { get; set; }
		public string Country { get; set; }
		public string Type { get; set; }
		public string Door { get; set; }
		/// <summary>
		/// Freguesia
		/// </summary>
		public string Parish { get; set; }
		public string Locality { get; set; }
		public string Floor { get; set; }
		public string ZipCode1 { get; set; }
		public string ZipCode2 { get; set; }
	}
}