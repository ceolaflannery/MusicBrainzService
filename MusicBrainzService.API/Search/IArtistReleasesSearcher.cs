namespace MusicBrainzService.API.Search
{
    public interface IArtistReleasesSearcher
    {
        ArtistReleasesResponse GetReleasesForArtistOrListOfPossibleArtistMatches(ArtistReleasesRequest request);
    }
}