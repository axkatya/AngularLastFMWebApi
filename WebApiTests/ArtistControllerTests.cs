using AngularLastFMWebApi.Controllers;
using AngularLastFMWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceAgent;
using System.Threading.Tasks;
using Xunit;

namespace WebApiTests
{
	public class ArtistControllerTests
    {
		ArtistController _controller;
		Mock<ILastFmServiceAgent> _lastFmServiceMock;
		Artist _mockArtist;
		public ArtistControllerTests()
		{
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
			_controller = new ArtistController(_lastFmServiceMock.Object);

			// Act
			IActionResult actionResult = await _controller.Get("Cher");

			// Assert
			Assert.NotNull(actionResult);
			OkObjectResult result = actionResult as OkObjectResult;
			Assert.NotNull(result);
			var item = result.Value as Artist;
			Assert.Equal("Cher", item.Name);
		}
	}
}
