using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using MusicBrainzService.API;
using MusicBrainzService.API.Search;
using MusicBrainzService.DAL;
using NUnit.Framework;

namespace MusicBrainzService.Tests.API.Search
{
    public class ArtistReleasesSearcherTests
    {
        private ArtistReleasesSearcher _sut;
        private Mock<IArtistReleasesRepository> _artistReleasesRepository;

        [SetUp]
        public void Setup()
        {
            _artistReleasesRepository = new Mock<IArtistReleasesRepository>();
            
            _sut = new ArtistReleasesSearcher(_artistReleasesRepository.Object);
        }

        [Test]
        public void GetReleasesForArtistOrListOfPossibleArtistMatches_WhenMoreThanOneArtistReturned_ShouldReturnArtistListAndNoReleases()
        {
            var request = ValidRequest;
            _artistReleasesRepository.Setup(x => x.RetrieveArtistBySearchTerm(request.ArtistName, request.Limit, request.Offset))
                .Returns(ThreeArtists);

            var result = _sut.GetReleasesForArtistOrListOfPossibleArtistMatches(request);

            _artistReleasesRepository.Verify(x => x.RetrieveArtistBySearchTerm(request.ArtistName, request.Limit, request.Offset), Times.Once());
            _artistReleasesRepository.Verify(x => x.RetrieveReleasesByArtistId(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never());
            Assert.AreEqual(3, result.Artists.Count);
            Assert.IsNull(result.Releases);
        }

        [Test]
        public void GetReleasesForArtistOrListOfPossibleArtistMatches_WhenExactlyOneArtistReturned_ShouldReturnReleasesListAndNoArtists()
        {
            var request = ValidRequest;
            _artistReleasesRepository.Setup(x => x.RetrieveArtistBySearchTerm(request.ArtistName, request.Limit, request.Offset))
                .Returns(OneArtist);
            _artistReleasesRepository.Setup(x => x.RetrieveReleasesByArtistId(OneArtist.First().Id, request.Limit, request.Offset))
                .Returns(FiveReleases);

            var result = _sut.GetReleasesForArtistOrListOfPossibleArtistMatches(request);

            _artistReleasesRepository.Verify(x => x.RetrieveArtistBySearchTerm(request.ArtistName, request.Limit, request.Offset), Times.Once());
            _artistReleasesRepository.Verify(x => x.RetrieveReleasesByArtistId(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            Assert.AreEqual(5, result.Releases.Count);
            Assert.IsNull(result.Artists);
        }

        [Test]
        public void GetReleasesForArtistOrListOfPossibleArtistMatches_WhenNoArtistReturned_ShouldReturnEmptyResponse()
        {
            var request = ValidRequest;
            _artistReleasesRepository.Setup(x => x.RetrieveArtistBySearchTerm(request.ArtistName, request.Limit, request.Offset))
                .Returns(new List<Artist>());

            var result = _sut.GetReleasesForArtistOrListOfPossibleArtistMatches(request);

            _artistReleasesRepository.Verify(x => x.RetrieveArtistBySearchTerm(request.ArtistName, request.Limit, request.Offset), Times.Once());
            _artistReleasesRepository.Verify(x => x.RetrieveReleasesByArtistId(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never());
            Assert.IsNull(result.Releases);
            Assert.IsNull(result.Artists);
        }

        private static ArtistReleasesRequest ValidRequest => new ArtistReleasesRequest { ArtistName = "Test" };

        private List<Artist> ThreeArtists => new List<Artist>
        {
            new Artist(new Guid("13211522-c20c-4e38-b599-137faa76ddf1"), "FunkyTown1", "FunkyTown1", "FunkyTown1"),
            new Artist(new Guid("14a6f47c-9a21-48e4-8d35-cc5b96c7cc93"), "FunkyTown2", "FunkyTown2", "FunkyTown2"),
            new Artist(new Guid("233394a3-9c79-4153-868e-bfa3cd8cb604"), "FunkyTown3", "FunkyTown3", "FunkyTown3")
        };
        private List<Artist> OneArtist => new List<Artist>
        {
            new Artist(new Guid("7988d4aa-9d5f-4dd9-9483-72ed7291a631"), "FunkyTown", "FunkyTown", "FunkyTown")
        };
        private List<Release> FiveReleases => new List<Release>
        {
            new Release("FunkyRelease0", Guid.NewGuid(), null),
            new Release("FunkyRelease2", Guid.NewGuid(), null),
            new Release("FunkyRelease3", Guid.NewGuid(), null),
            new Release("FunkyRelease4", Guid.NewGuid(), null),
            new Release("FunkyRelease5", Guid.NewGuid(), null),
        };
    }
}
