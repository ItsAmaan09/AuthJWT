using System;
using System.Data;
using Microsoft.Data.SqlClient;
using SimpleJWT.Models;

namespace SimpleJWT
{
	public class UserRepository
	{
		private string connectionString = string.Empty;

		public UserRepository()
		{
			this.connectionString = ConfigurationHelper.Instance.GetConnectionString();
		}

		public IEnumerable<User> GetUsers()
		{
			IList<User> users = new List<User>();
			try
			{

				using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
				{
					SqlCommand sqlCommand = new SqlCommand
					{
						CommandType = CommandType.Text,
						CommandText = "SELECT * FROM Users",
						Connection = sqlConnection
					};

					sqlConnection.Open();
					SqlDataReader reader = sqlCommand.ExecuteReader();
					while (reader.Read())
					{
						User user = new User
						{
							UserID = (short)reader["UserID"],
							UserName = (string)reader["UserName"],
							Password = (string)reader["Password"],
							FirstName = (string)reader["FirstName"],
							LastName = (string)reader["LastName"],
							Gender = (string)reader["Gender"],
							EmailAddress = (string)reader["EmailAddress"],
							MobileNumber = (string)reader["MobileNumber"],
							IsActive = (bool)reader["IsActive"],
							IsDeleted = (bool)reader["IsDeleted"]
						};
						users.Add(user);
					}
					reader.Close();
					sqlConnection.Close();
				}
			}
			catch (System.Exception)
			{
				throw;
			}

			return users;
		}
		public bool AddUser(User user)
		{
			using (SqlConnection con = new SqlConnection(this.connectionString))
			{
				con.Open();
				var query = "INSERT INTO [USERS] (UserName,FirstName,LastName,EmailAddress,Password,MobileNumber,Gender,IsActive, CreatedBy, CreatedOn) VALUES (@UserName,@FirstName,@LastName,@EmailAddress,@Password,@MobileNumber,@Gender,@IsActive, @CreatedBy, @CreatedOn)";

				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.UserName;
					cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = user.FirstName;
					cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = user.LastName;
					cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = user.EmailAddress;
					cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = user.Password;
					cmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar).Value = user.MobileNumber;
					cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = user.Gender;
					cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;
					cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = "MA";
					cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = DateTime.Now.ToUniversalTime();

					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}

		public IEnumerable<User> IsEntityAlreadyExists(User user)
		{
			IList<User> users = new List<User>();

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				SqlCommand sqlCommand = new SqlCommand
				{
					CommandType = CommandType.Text,
					CommandText = "SELECT * FROM [Users] WHERE UserName = @UserName OR FirstName = @FirstName",
					Connection = sqlConnection
				};

				sqlCommand.Parameters.AddWithValue("@UserName", user.UserName);
				sqlCommand.Parameters.AddWithValue("@FirstName", !string.IsNullOrEmpty(user.FirstName) ? user.FirstName : (object)DBNull.Value);

				sqlConnection.Open();
				SqlDataReader reader = sqlCommand.ExecuteReader();

				while (reader.Read())
				{
					User user1 = new User
					{
						UserID = (short)reader["UserID"],
						UserName = (string)reader["UserName"],
						Password = (string)reader["Password"],
						FirstName = (string)reader["FirstName"],
						LastName = (string)reader["LastName"],
						Gender = (string)reader["Gender"],
						EmailAddress = (string)reader["EmailAddress"],
						MobileNumber = (string)reader["MobileNumber"],
						IsActive = (bool)reader["IsActive"],
						IsDeleted = (bool)reader["IsDeleted"]
					};
					users.Add(user1);
				}
				reader.Close();

				sqlConnection.Close();

				return users;
			}
		}
	}
}