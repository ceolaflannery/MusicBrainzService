using MusicBrainzService.API;
using NUnit.Framework;

namespace MusicBrainzService.Tests
{
    public class ArtistReleasesRequestTests
    {
        private ArtistReleasesRequest _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ArtistReleasesRequest();
        }

        [TestCase(100)]
        [TestCase(99)]
        [TestCase(5)]
        [TestCase(1)]
        [Test]
        public void IsValid_WhenValidArtistNameAndLimitGreaterLessThanOrEqualTo100_ShouldReturnTrue(int limit)
        {
            _sut.ArtistName = "Test Artist";
            _sut.Limit = limit;

            var isValid = _sut.IsValid(out string actualMessage);

            Assert.True(isValid);
        }

        [Test]
        public void IsValid_WhenNullArtistAndLimitUnder100_ShouldReturnFalse()
        {
            var expectedMessage = "Artist name is a required field. Add '?artistName=Example' to your request;";
            _sut.ArtistName = null;
            _sut.Limit = 50;

            var isValid = _sut.IsValid(out string actualMessage);

            Assert.False(isValid);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestCase(101)]
        [TestCase(200)]
        [TestCase(1000)]
        [Test]
        public void IsValid_WhenNullArtistAndLimitIs100OrOver_ShouldReturnFalse(int limit)
        {
            var expectedMessage = "Artist name is a required field. Add '?artistName=Example' to your request;Cannot retrieve more that 100 results at once;";
            _sut.ArtistName = null;
            _sut.Limit = limit;

            var isValid = _sut.IsValid(out string actualMessage);

            Assert.False(isValid);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void IsValid_WhenNullArtistAndLimitIsZero_ShouldReturnFalse()
        {
            var expectedMessage = "Artist name is a required field. Add '?artistName=Example' to your request;It is invalid to request 0 records;";
            _sut.ArtistName = null;
            _sut.Limit = 0;

            var isValid = _sut.IsValid(out string actualMessage);

            Assert.False(isValid);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestCase(101)]
        [TestCase(200)]
        [TestCase(1000)]
        [Test]
        public void IsValid_WhenValidArtistNameAndLimitGreaterThan100_ShouldReturnFalse(int limit)
        {
            var expectedMessage = "Cannot retrieve more that 100 results at once;";
            _sut.ArtistName = "Test Artist";
            _sut.Limit = limit;

            var isValid = _sut.IsValid(out string actualMessage);

            Assert.False(isValid);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void IsValid_WhenValidArtistNameAndZeroLimit_ShouldReturnFalse()
        {
            var expectedMessage = "It is invalid to request 0 records;";
            _sut.ArtistName = "Test Artist";
            _sut.Limit = 0;

            var isValid = _sut.IsValid(out string actualMessage);

            Assert.False(isValid);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

    }
}