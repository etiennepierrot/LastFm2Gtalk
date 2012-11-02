using RestSharp;

namespace StatusUpdater.LastFM
{
    public class LastFmClient
    {
        private readonly LastFmContentParserService _lastFmContentParserService;

        public LastFmClient(LastFmContentParserService lastFmContentParserService)
        {
            _lastFmContentParserService = lastFmContentParserService;
        }

        public virtual Track GetCurrentTrack(string user)
        {
            var client = new RestClient("http://ws.audioscrobbler.com");
            string pathRequest =
                string.Format("/2.0/?method=user.getrecenttracks&user={0}&api_key=49a0288b68ec3f08f94a216cdf5cf4f0",
                              user);
            var request = new RestRequest(pathRequest);
            var response = client.Execute(request);
            var content = response.Content;

            return _lastFmContentParserService.ParseStatusResponse(content) == "ok" ? _lastFmContentParserService.ParseCurrentTrack(content) : null;
            
        }
    }
}
