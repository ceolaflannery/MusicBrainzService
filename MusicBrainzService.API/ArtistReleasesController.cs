using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicBrainzService.API.Search;

namespace MusicBrainzService.API
{
    [ApiController]
    [Route("ArtistReleases")]
    public class ArtistReleasesController : ControllerBase
    {
        private readonly ILogger<ArtistReleasesController> _logger;
        private readonly IArtistReleasesSearcher _searcher;

        public ArtistReleasesController(ILogger<ArtistReleasesController> logger, IArtistReleasesSearcher searcher)
        {
            _logger = logger;
            _searcher = searcher;
        }

        [HttpGet("Search")]
        public ActionResult<ArtistReleasesResponse> LookupReleasesForArtistOrListOfPossibleArtistMatches([FromQuery]ArtistReleasesRequest request)
        {
            string errorMessage;
            if (!request.IsValid(out errorMessage))
                return BadRequest(errorMessage);
            
            try
            {
                return _searcher.GetReleasesForArtistOrListOfPossibleArtistMatches(request);

            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }
}
