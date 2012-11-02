using System.Collections.Generic;
using System.Linq;
using StatusUpdater.LastFM;

namespace StatusUpdater.GoogleAccounts
{
    public class Facade
    {
        private readonly GoogleAccountService _googleAccountService;
        private readonly LastFmClient _lastFmClient;

        public Facade(GoogleAccountService googleAccountService, LastFmClient lastFmClient)
        {
            _googleAccountService = googleAccountService;
            _lastFmClient = lastFmClient;
        }

        public void UpdateStatusOnGoogleAccount(IEnumerable<GoogleAccount> googleAccounts, string status)
        {
            foreach (var xmppClient in googleAccounts.Where(x => x.IsAccountValid.HasValue && x.IsAccountValid.Value))
            {
                xmppClient.SetStatus(status);
            }
        }

        public void Update(string lastFMUser, IEnumerable<GoogleAccount> googleAccounts)
        {
            var track = _lastFmClient.GetCurrentTrack(lastFMUser);

            if (track != null)
            {
                UpdateStatusOnGoogleAccount(googleAccounts, track.ToString());
            }
        }


        public void RegisterAccount(string email, string password)
        {
            _googleAccountService.RegisterAccount(email, password);
        }

        public IEnumerable<GoogleAccount> GetGoogleAccountToUpdate()
        {
            return _googleAccountService.GetValidAccount();
        }
    }
}
