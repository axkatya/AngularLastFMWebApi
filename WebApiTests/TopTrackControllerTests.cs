using AngularLastFMWebApi.Controllers;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceAgent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace WebApiTests
{
	public class TopTrackControllerTests
	{
		private TopTrackController _controller;
		private readonly Mock<ILastFmServiceAgent> _lastFmServiceMock;
		private readonly IEnumerable<Track> _mockTracks;

		public TopTrackControllerTests()
		{
			_lastFmServiceMock = new Mock<ILastFmServiceAgent>();
			_mockTracks = new List<Track>
			{
				new Track { Name = "Love123", Listeners = 123 },
				new Track { Name = "Love456", Listeners = 567 }
			};

			_lastFmServiceMock.Setup(x => x.GetTopTracks("Cher")).Returns(() => Task.FromResult(_mockTracks));
		}

		[Fact]
		public async Task Get_WhenCalled_ReturnsAllItemsAsync()
		{
			_controller = new TopTrackController(_lastFmServiceMock.Object);

			// Act
			IActionResult actionResult = await _controller.Get("Cher").ConfigureAwait(false);

			// Assert
			Assert.NotNull(actionResult);
			OkObjectResult result = actionResult as OkObjectResult;
			Assert.NotNull(result);
			var items = new List<Track>(result.Value as IEnumerable<Track>);
			Assert.NotNull(items);
		}
	}
}
