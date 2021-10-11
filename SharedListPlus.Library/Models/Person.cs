using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedListPlus.Library.Models
{
	public class Person
	{
		public long PersonId { get; set; }
		public Group YourGroup { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public long? GroupId { get; set; }

		public static bool IsValidEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;

			try
			{
				// Normalize the domain
				email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
									  RegexOptions.None, TimeSpan.FromMilliseconds(200));

				// Examines the domain part of the email and normalizes it.
				string DomainMapper(Match match)
				{
					// Use IdnMapping class to convert Unicode domain names.
					var idn = new IdnMapping();

					// Pull out and process domain name (throws ArgumentException on invalid)
					string domainName = idn.GetAscii(match.Groups[2].Value);

					return match.Groups[1].Value + domainName;
				}
			}
			catch (RegexMatchTimeoutException e)
			{
				return false;
			}
			catch (ArgumentException e)
			{
				return false;
			}

			try
			{
				return Regex.IsMatch(email,
					@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
					RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
		}
		public static bool IsValidPhone(string phone, bool IsRequired)
		{
			if (string.IsNullOrEmpty(phone) & !IsRequired)
				return true;

			if (string.IsNullOrEmpty(phone) & IsRequired)
				return false;

			var cleaned = RemoveNonNumeric(phone);
			if (IsRequired)
			{
				if (cleaned.Length == 10)
					return true;
				else
					return false;
			}
			else
			{
				if (cleaned.Length == 0)
					return true;
				else if (cleaned.Length > 0 & cleaned.Length < 10)
					return false;
				else if (cleaned.Length == 10)
					return true;
				else
					return false; // should never get here
			}
		}
		public static string RemoveNonNumeric(string phone)
		{
			return Regex.Replace(phone, @"[^0-9]+", "");
		}
	}
}
