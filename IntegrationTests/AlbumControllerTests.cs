using Xunit;

namespace IntegrationTests
{
	public class AlbumControllerTests : IClassFixture<TestServerFixture>
	{
		private readonly TestServerFixture _fixture;

		public AlbumControllerTests(TestServerFixture fixture)
		{
			_fixture = fixture;
		}

		[Fact]
		public async System.Threading.Tasks.Task Get_WhenCalled_ReturnsAllItemsAsync()
		{
			// Act
			var response = await _fixture.Client.GetAsync("/api/album/love");

			// Assert
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();
			Assert.Contains("Love", responseString);
		}
	}
}
