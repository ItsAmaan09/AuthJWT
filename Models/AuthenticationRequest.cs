using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJWT.Models
{
	public class AuthenticationRequest
	{
		private string userIdentifier = string.Empty;
		private string password = string.Empty;
		private string senderIPAddress = string.Empty;
		public string UserIdentifier { get => userIdentifier; set => userIdentifier = value; }
		public string Password { get => password; set => password = value; }
		public string SenderIPAddress { get => senderIPAddress; set => senderIPAddress = value; }
		public string RefreshToken { get; set; }
		public bool IsEmailAsUserIdentifier { get; set; }
	}
	public class AuthenticationInfo
	{
		public string UserIdentifier { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Token { get; set; }
		public string RefreshToken { get; set; }

	}
}