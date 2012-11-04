using System.Collections.Generic;
using System.Linq;
using StatusUpdater.RavenRepositories;
using StatusUpdater.Tools;

namespace StatusUpdater.GoogleAccounts
{
    public class GoogleAccountService
    {
        public IEnumerable<GoogleAccount> GetValidAccount()
        {
            using (var session = RavenRepository.GetInstance)
            {
                var googleAccounts = session.Query<GoogleAccount>().Where(x =>  x.IsAccountValid);
                var validAccount = googleAccounts.ToArray();
                return validAccount;
            }
            
        }

        public void MigratePassword()
        {
            using(var session = RavenRepository.GetInstance)
            {
                var accounts = session.Query<GoogleAccount>();
                foreach (var googleAccount in accounts)
                {
                    googleAccount.Password = Crypto.Encrypt(googleAccount.Password, Crypto.Password);
                }
            }
        }

        public string RegisterAccount(string login, string password)
        {
            using (var session = RavenRepository.GetInstance)
            {
                var entity = new GoogleAccount(login, password);
                session.Store(entity);
                session.SaveChanges();
                return entity.Id;
            }
        }

        public void CloseConnection()
        {
            using (var session = RavenRepository.GetInstance)
            {
                var accounts = session.Query<GoogleAccount>().Where(x => x.IsConnected);
                foreach (var googleAccount in accounts)
                {
                    googleAccount.CloseConnection();
                }
            }
        }
    }
}
