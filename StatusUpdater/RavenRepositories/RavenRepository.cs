using System.Collections.Generic;
using Raven.Client;
using Raven.Client.Linq;
using StatusUpdater .LastFM;
using StructureMap;
using System.Linq;

namespace StatusUpdater.RavenRepositories
{

    public class RavenRepository<T> where T : IEntity
    {
        public void Save(T entity)
        {
            using (var session = ObjectFactory.GetInstance<IDocumentSession>())
            {
                session.Store(entity);
                session.SaveChanges();
                session.Dispose();
            }
        }

        public T Get(string id)
        {
            using (var session = ObjectFactory.GetInstance<IDocumentSession>())
            {
                var entity = session.Load<T>(id);
                return entity;
            }

        }

        public IEnumerable<T> QueryAll()
        {
            using (var session = ObjectFactory.GetInstance<IDocumentSession>())
            {
                return session.Query<T>().ToArray();
            }
        }

        public void Delete(string id)
        {
            using (var session = ObjectFactory.GetInstance<IDocumentSession>())
            {
                var entity = session.Load<T>(string.Format(id));
                session.Delete(entity);
                session.SaveChanges();
            }
        }

        public void Delete()
        {
            using (var session = ObjectFactory.GetInstance<IDocumentSession>())
            {
                foreach (var entity in QueryAll())
                {
                    session.Delete(entity);
                    session.SaveChanges();
                }
            }
        }
    }
}
