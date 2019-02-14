using AngularLastFMWebApi.Controllers;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceAgent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Xunit;

namespace WebApiTests
{
	public class AlbumControllerTests
	{
		AlbumController _controller;
		Mock<ILastFmServiceAgent> _lastFmServiceMock;
	    Mock<IFavoriteAlbumBusiness> _favoriteAlbumBusinessMock;
        IEnumerable<Album> _mockAlbums;

		public AlbumControllerTests()
		{
			_lastFmServiceMock = new Mock<ILastFmServiceAgent>();
		    _favoriteAlbumBusinessMock = new Mock<IFavoriteAlbumBusiness>();
            _mockAlbums = new List<Album>
			{
				new Album { Name = "Love123", Artist = "Cher" },
				new Album { Name = "Love456", Artist = "Tim" }
			};

			_lastFmServiceMock.Setup(x => x.GetAlbums("love")).Returns(() => Task.FromResult(_mockAlbums));
		}

		[Fact]
		public async Task Get_WhenCalled_ReturnsAllItemsAsync()
		{
			_controller = new AlbumController(_lastFmServiceMock.Object, _favoriteAlbumBusinessMock.Object);

			// Act
			IActionResult actionResult = await _controller.Get("love");

			// Assert
			Assert.NotNull(actionResult);
			OkObjectResult result = actionResult as OkObjectResult;
			Assert.NotNull(result);
			var items = new List<Album>(result.Value as IEnumerable<Album>);
			Assert.Equal(2, items.Count);
		}
	}
}

