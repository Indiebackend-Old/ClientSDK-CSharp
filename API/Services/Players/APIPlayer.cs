using System;

namespace Indiebackend.API.Services
{
	public class ApiPlayer
	{
		public string Id { get; set; }
		public string AppId { get; set; }
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public string Email { get; set; }
		public DateTime? BirthDate { get; set; }
		public string AvatarUrl { get; set; }
		public string IsVerified { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string Token { get; set; }
	}
}