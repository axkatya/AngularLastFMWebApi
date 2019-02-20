namespace Entities
{
	/// <summary>
	/// The account entity.
	/// </summary>
	public class Account
	{
		public int UserId { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public string Token { get; set; }
	}
}
