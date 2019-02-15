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
	public class TopAlbumControllerTests
	{
		TopAlbumController _controller;
	    readonly Mock<ILastFmServiceAgent> _lastFmServiceMock;
	    readonly Mock<IFavoriteAlbumBusiness> _favoriteAlbumBusinessMock;
	    readonly IEnumerable<TopAlbum> _mockAlbums;

		public TopAlbumControllerTests(Mock<IFavoriteAlbumBusiness> favoriteAlbumBusinessMock)
		{
		    _favoriteAlbumBusinessMock = favoriteAlbumBusinessMock;
		    _lastFmServiceMock = new Mock<ILastFmServiceAgent>();
			_mockAlbums = new List<TopAlbum>
			{
				new TopAlbum { Name = "Love123", PlayCount = 123 },
				new TopAlbum { Name = "Love456", PlayCount = 567 }
			};

			_lastFmServiceMock.Setup(x => x.GetTopAlbums("Cher")).Returns(() => Task.FromResult(_mockAlbums));
		}

		[Fact]
		public async Task Get_WhenCalled_ReturnsAllItemsAsync()
		{
			_controller = new TopAlbumController(_lastFmServiceMock.Object, _favoriteAlbumBusinessMock.Object);

			// Act
			IActionResult actionResult = await _controller.Get("Cher");

			// Assert
			Assert.NotNull(actionResult);
			OkObjectResult result = actionResult as OkObjectResult;
			Assert.NotNull(result);
			var items = new List<TopAlbum>(result.Value as IEnumerable<TopAlbum>);
			Assert.NotNull(items);
			Assert.Equal(2, items.Count);
		}
	}
}
