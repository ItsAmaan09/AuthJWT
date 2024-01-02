using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleJWT.Models;

namespace SimpleJWT
{
	public class UserManager
	{
		private UserRepository userRepository = null;
		public UserManager()
		{
			this.userRepository = new UserRepository();
		}
		public IEnumerable<User> GetUsers()
		{
			IEnumerable<User> userList = new List<User>();
			try
			{
				userList = this.userRepository.GetUsers();



			}
			catch (System.Exception)
			{

				throw;
			}
			return userList;
		}
		public bool AddUser(User user)
		{
			bool result = false;

			try
			{
				user.UserID = 0;
				result = this.userRepository.AddUser(user);
			}
			catch (System.Exception)
			{

				throw;
			}

			return result;
		}

		public AuthenticationInfo Authenticate(AuthenticationRequest authenticationRequest)
		{
			string userName = authenticationRequest.UserIdentifier;
			var user1 = new User();
			user1.UserName = authenticationRequest.UserIdentifier;
			var username = authenticationRequest.UserIdentifier;
			IEnumerable<User> users = this.IsUserExists(user1);

			if (users == null || users.Count() == 0)
			{
				throw new Exception("User is invalid");
			}

			var user = users.First();
			if (user.IsActive.HasValue == false || user.IsActive.Value == false)
			{
				throw new Exception("User is not active");
			}

			authenticationRequest.UserIdentifier = user.UserID.ToString();

			return new AuthenticationInfo()
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				UserIdentifier = user.UserID.ToString(),
			};
		}
		public IEnumerable<User> IsUserExists(User user)
		{
			IEnumerable<User> userList = new List<User>();
			try
			{
				userList = this.userRepository.IsEntityAlreadyExists(user);
			}
			catch (System.Exception)
			{

				throw;
			}
			return userList;
		}
	}
}