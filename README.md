# MusicBrainz

MusicBrainz is a service which searches for music artists based on given artist name. It will return the releases associated with that artist if there is only one search result. It will return a list of artists if more that one search result is returned.

This is the format of the API Response in swagger https://app.swaggerhub.com/apis/ceola/artist-releases/1.0.0

### System Diagram
![System Diagram](/SystemDiagram.png)

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
- Chose .Net Core as made an assumption that this should be extensible. Azure Automation or a Lambda may have been sufficient depending on the use case.
- Added paging as assumed use case would care about time it takes to return and returned results could be very large and difficult to consume in their entirety.
- Used the available client library so that I didn't waste time shaving the Yak .
- Ran into an issue that there was a bug with the required endpoint in the client library so for the purposes of this code challenge, I went with another similar endpoint but in real life would have probably updated the client library myself as it is being kept fairly up to date with the MusicBrainz endpoints. 

### Assumptions
- Should be extensible which was a big assumption because I didn’t know the use case.
- Assumed calling would care about time it takes to return and not need all records at once.

### If I was to put more time into it I would have...
- Confirm use case.
- Improved paging (e.g. Hal) to improve usability for the calling code.
- Ignore empty collection in response using serialisation.
- Use Releases instead of ReleaseGroups by using API directly or updating client library.
