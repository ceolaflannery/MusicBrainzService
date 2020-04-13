using System.Text;

namespace MusicBrainzService.API
{
    public class ArtistReleasesRequest
    {
        public string ArtistName { get; set; }
        public int Limit { get; set; } = 25;
        public int Offset { get; set; } = 0;

        public ArtistReleasesRequest() { }

        public ArtistReleasesRequest(string artistName, int limit, int offset)
        {
            ArtistName = artistName;
            Limit = limit;
            Offset = offset;
        }
        
        internal bool IsValid(out string errorMessage)
        {
            var concatenatedMessage = new StringBuilder();

            if (string.IsNullOrWhiteSpace(ArtistName))
                concatenatedMessage.Append("Artist name is a required field. Add '?artistName=Example' to your request;");

            if (Limit == 0)
                concatenatedMessage.Append("It is invalid to request 0 records;");

            if (Limit > 100)
                concatenatedMessage.Append("Cannot retrieve more that 100 results at once;");

            errorMessage = concatenatedMessage.ToString();
            return string.IsNullOrWhiteSpace(errorMessage);
        }

    }
}
