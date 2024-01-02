using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleJWT.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleJWT.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		IConfiguration configuration;

		UserManager userManager;
		public AuthController(IConfiguration configuration)
		{
			this.configuration = configuration;
			this.userManager = new UserManager();
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Auth([FromBody] AuthenticationRequest authModel)
		{
			// IActionResult response = Unauthorized();
			try
			{
				// if (authModel != null)
				// {
					var user = new User();
					user.UserName = authModel.UserIdentifier;
					var authInfo = this.userManager.Authenticate(authModel);
					if (authInfo != null)
					{
						var issuer = configuration["Jwt:Issuer"];
						var audience = configuration["Jwt:Audience"];
						var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
						var signingCredentials = new SigningCredentials(
							new SymmetricSecurityKey(key),
							SecurityAlgorithms.HmacSha512Signature
							);
						var subject = new ClaimsIdentity(new[]
						{
						new Claim(JwtRegisteredClaimNames.Name, authInfo.UserIdentifier),
						// new Claim(JwtRegisteredClaimNames.Email, user.UserName),
					});

						var expires = DateTime.UtcNow.AddMinutes(10);
						var tokenDescriptor = new SecurityTokenDescriptor
						{
							Subject = subject,
							Expires = expires,
							Issuer = issuer,
							Audience = audience,
							SigningCredentials = signingCredentials
						};

						var tokenHandler = new JwtSecurityTokenHandler();
						var token = tokenHandler.CreateToken(tokenDescriptor);
						authInfo.Token = tokenHandler.WriteToken(token);
					}
					return Ok(authInfo);
				// }
			}
			catch (System.Exception)
			{

				throw;
			}

			// return response;
		}

		// public IActionResult Auth([FromBody] AuthenticationRequest authModel)
		// {
		// 	IActionResult response = Unauthorized();
		// 	if (authModel != null)
		// 	{
		// 		var user = new User();
		// 		user.UserName = authModel.UserIdentifier;
		// 		var result = this.userManager.IsUserExists(user);
		// 		if (result.Count() > 0)
		// 		{
		// 			var issuer = configuration["Jwt:Issuer"];
		// 			var audience = configuration["Jwt:Audience"];
		// 			var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
		// 			var signingCredentials = new SigningCredentials(
		// 				new SymmetricSecurityKey(key),
		// 				SecurityAlgorithms.HmacSha512Signature
		// 				);
		// 			var subject = new ClaimsIdentity(new[]
		// 			{
		// 				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
		// 				new Claim(JwtRegisteredClaimNames.Email, user.UserName),
		// 			});

		// 			var expires = DateTime.UtcNow.AddMinutes(10);
		// 			var tokenDescriptor = new SecurityTokenDescriptor
		// 			{
		// 				Subject = subject,
		// 				Expires = expires,
		// 				Issuer = issuer,
		// 				Audience = audience,
		// 				SigningCredentials = signingCredentials
		// 			};

		// 			var tokenHandler = new JwtSecurityTokenHandler();
		// 			var token = tokenHandler.CreateToken(tokenDescriptor);
		// 			var jwtToken = tokenHandler.WriteToken(token);
		// 			return Ok(jwtToken);
		// 		}
		// 	}
		// 	return response;
		// }
	}
}
