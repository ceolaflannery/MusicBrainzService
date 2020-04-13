# MusicBrainz

MusicBrainz is a service which searches for music artists based on given artist name. It will return the releases associated with that artist if there is only one search result. It will return a list of artists if more that one search result is returned.

This is the format of the API Response in swagger https://app.swaggerhub.com/apis/ceola/artist-releases/1.0.0


### Installation

Requires [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) to run.

```sh
$ cd MusicBrainz
$ dotnet run
```

Hit the following endpoint through your browser, Fiddler or Postman to verify
```sh
https://localhost:5002/ArtistReleases/Search?ArtistName=Madonna
```


### Decisions and reasons
- Chose .Net Core as made an assumption that this should be extensible which was a big assumption. Azure Automation or a Lambda may have been sufficient depending on the use case
- Added paging as assumed use case would care about time it takes to return and returned results could be very large and difficult to consume in their entirety

### Assumptions
- Should be extensible which was a big assumption because I didn’t know the use case
- Assumed use case would care about time it takes to return

### If I was to put more time into it I would have...
- Confirm use case
- Improved paging (e.g. Hal) to improve usability for the calling code
- Ignore empty collection in response using serialisation
- Use Releases instead of ReleaseGroups by using API directly
