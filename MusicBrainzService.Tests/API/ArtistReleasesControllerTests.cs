using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MusicBrainzService.API;
using MusicBrainzService.API.Search;
using NUnit.Framework;

namespace MusicBrainzService.Tests.API
{
    public class ArtistReleasesControllerTests
    {
        private ArtistReleasesController _sut;
        private Mock<ILogger<ArtistReleasesController>> _logger;
        private Mock<IArtistReleasesSearcher> _artistReleasesesSearcher;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<ArtistReleasesController>>();
            _artistReleasesesSearcher = new Mock<IArtistReleasesSearcher>();

            _sut = new ArtistReleasesController(_logger.Object, _artistReleasesesSearcher.Object);
        }

        [Test]
        public void LookupReleasesForArtistOrListOfPossibleArtistMatches_WhenRequestInvalid_ShouldReturnBadRequest()
        {
            var request = InvalidRequest;

            var result = _sut.LookupReleasesForArtistOrListOfPossibleArtistMatches(request);

            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void LookupReleasesForArtistOrListOfPossibleArtistMatches_WhenRequestValid_ShouldCallLookup()
        {
            var request = ValidRequest;
            _artistReleasesesSearcher.Setup(
                x => x.GetReleasesForArtistOrListOfPossibleArtistMatches(request))
                .Returns(ValidResponse);

            var result = _sut.LookupReleasesForArtistOrListOfPossibleArtistMatches(request);

            _artistReleasesesSearcher.Verify(
                x => x.GetReleasesForArtistOrListOfPossibleArtistMatches(request));
            Assert.AreEqual(ValidResponse.Artists.First().Name, result.Value.Artists.First().Name);
        }

        private static ArtistReleasesResponse ValidResponse =
            new ArtistReleasesResponse { Artists = new List<Artist> { new Artist(Guid.NewGuid(), "FunkyTown", "FunkyTown", "FunkyTown") } };
     

        private static ArtistReleasesRequest InvalidRequest => new ArtistReleasesRequest();
        private static ArtistReleasesRequest ValidRequest => new ArtistReleasesRequest { ArtistName = "Test" };

    }
}
