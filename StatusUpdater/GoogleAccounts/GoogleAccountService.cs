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
                var googleAccounts = session.Query<GoogleAccount>();
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
                    session.Store(googleAccount);
                }
                session.SaveChanges();
            }
        }

        public void RegisterAccount(string email, string password)
        {
            using (var session = RavenRepository.GetInstance)
            {
                var existingEmail = session.Load<GoogleAccount>().FirstOrDefault(x => x.Email == email);

                if (existingEmail != null) return;
                var entity = new GoogleAccount(email)
                                 {
                                     Password =   Crypto.Encrypt(password, Crypto.Password)
                                 };

                if (!entity.Connect()) return;

                session.Store(entity);
                session.SaveChanges();
                entity.CloseConnection();
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

        public IEnumerable<GoogleAccount> GetAccounts()
        {
            using (var session = RavenRepository.GetInstance)
            {
                var accounts = session.Query<GoogleAccount>();
                return accounts.ToArray();
            }
        }
    }
}
