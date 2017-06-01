using System;
using System.Text.RegularExpressions;

namespace Carvajal.Turns.Domain.Entities
{
	[Serializable]
	public class Address
	{
		private const string MailAddressRegex = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
		private string addresss;
		public string Addresss
		{
			get
			{
				return this.addresss;
			}
			set
			{
				Address.ValidateAddress(value);
				this.addresss = value;
			}
		}
		public Address(string address)
		{
			Address.ValidateAddress(address);
			this.addresss = address;
		}
		private Address()
		{
		}
		private static void ValidateAddress(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			Regex regex = new Regex("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
			if (!regex.IsMatch(address))
			{
				throw new FormatException("The specified string is not in the form required for an e-mail address.");
			}
		}
	}
}
