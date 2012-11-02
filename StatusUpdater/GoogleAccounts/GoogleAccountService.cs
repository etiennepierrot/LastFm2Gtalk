using System.Collections.Generic;
using System.Linq;
using StatusUpdater.RavenRepositories;

namespace StatusUpdater.GoogleAccounts
{
    public class GoogleAccountService
    {
        private readonly RavenRepository<GoogleAccount> _ravenRepositoryGoogleAccount;

        public GoogleAccountService(RavenRepository<GoogleAccount> ravenRepositoryGoogleAccount)
        {
            _ravenRepositoryGoogleAccount = ravenRepositoryGoogleAccount;
        }

        public IEnumerable<GoogleAccount> GetValidAccount()
        {
            return
                _ravenRepositoryGoogleAccount.QueryAll().Where(x => x.IsAccountValid.HasValue && x.IsAccountValid.Value)
                    .ToArray();
        }

        public GoogleAccount Get(string id)
        {
            return _ravenRepositoryGoogleAccount.Get(id);
        }

        public string RegisterAccount(string login, string password)
        {
            var entity = new GoogleAccount(login, password);
             entity =_ravenRepositoryGoogleAccount.Save(entity);
            return entity.Id;
        }
    }
}
