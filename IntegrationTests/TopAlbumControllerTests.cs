﻿using Xunit;

namespace IntegrationTests
{
   public class TopAlbumControllerTests : IClassFixture<TestServerFixture>
	{
		private readonly TestServerFixture _fixture;

		public TopAlbumControllerTests(TestServerFixture fixture)
		{
			_fixture = fixture;
		}

		[Fact]
		public async System.Threading.Tasks.Task Get_WhenCalled_ReturnsAllItemsAsync()
		{
			// Act
			var response = await _fixture.Client.GetAsync("/api/topalbum/cher").ConfigureAwait(false);

			// Assert
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			Assert.Contains("Believe", responseString);
		}
	}
}
