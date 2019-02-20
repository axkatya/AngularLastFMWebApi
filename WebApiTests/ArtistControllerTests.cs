using AngularLastFMWebApi.Controllers;
using Business.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceAgent;
using System.Threading.Tasks;
using Xunit;

namespace WebApiTests
{
	public class ArtistControllerTests
	{
		private ArtistController _controller;
		private readonly Mock<ILastFmServiceAgent> _lastFmServiceMock;
		private readonly Mock<IFavoriteArtistBusiness> _favoriteArtistBusinessMock;
		private readonly Artist _mockArtist;

		public ArtistControllerTests(Mock<IFavoriteArtistBusiness> favoriteArtistBusinessMock)
		{
			_favoriteArtistBusinessMock = favoriteArtistBusinessMock;
			_lastFmServiceMock = new Mock<ILastFmServiceAgent>();
			_mockArtist = new Artist
			{
				 Name = "Cher"
			};

			_lastFmServiceMock.Setup(x => x.GetArtist("Cher")).Returns(() => Task.FromResult(_mockArtist));
		}

		[Fact]
		public async Task Get_WhenCalled_ReturnsAllItemsAsync()
		{
			_controller = new ArtistController(_lastFmServiceMock.Object, _favoriteArtistBusinessMock.Object);

			// Act
			IActionResult actionResult = await _controller.Get("Cher").ConfigureAwait(false);

			// Assert
			Assert.NotNull(actionResult);
			OkObjectResult result = actionResult as OkObjectResult;
			Assert.NotNull(result);
			var item = result.Value as Artist;
			Assert.Equal("Cher", item.Name);
		}
	}
}
