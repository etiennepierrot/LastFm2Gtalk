using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using StatusUpdater.GoogleAccounts;
using StatusUpdater.LastFM;
using StructureMap;
using log4net;

namespace StatusUpdater
{
    public class Facade
    {
       
        private readonly GoogleAccountService _googleAccountService;
        private readonly LastFmClient _lastFmClient;
        private readonly TrackService _trackService;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(Facade));

        public Facade(GoogleAccountService googleAccountService, LastFmClient lastFmClient, TrackService trackService)
        {
            _googleAccountService = googleAccountService;
            _lastFmClient = lastFmClient;
            _trackService = trackService;
        }

        public void UpdateStatusOnGoogleAccount(IEnumerable<GoogleAccount> googleAccounts, string status)
        {
            
            foreach (var xmppClient in googleAccounts.Where(x => x.IsAccountValid))
            {
                xmppClient.SetStatus(status);
            }
        }

        public bool Update(string lastFMUser, IEnumerable<GoogleAccount> googleAccounts)
        {
            Logger.Info("Begin update");
            var track = _lastFmClient.GetCurrentTrackOnAir(lastFMUser);

            if (track == null) return false;

            var currentTrack = _trackService.GetCurrentTrack();

            if(currentTrack != null)
            {
                if (currentTrack.ToString() == track.ToString()) return false;
            }
            

            _trackService.SetCurrentTrack(track);
            UpdateStatusOnGoogleAccount(googleAccounts, track.ToString());
            return true;
        }


        public void RegisterAccount(string email, string password)
        {
            
            _googleAccountService.RegisterAccount(email, password);
        }

        public IEnumerable<GoogleAccount> GetGoogleAccountToUpdate()
        {
            return _googleAccountService.GetValidAccount();
        }

        public void Dispose()
        {
            _googleAccountService.CloseConnection();
            ObjectFactory.GetInstance<IDocumentStore>().Dispose();
        }

        public Track GetCurrentTrack()
        {
            return _trackService.GetCurrentTrack();
        }
    }
}
