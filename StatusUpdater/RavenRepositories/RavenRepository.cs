using System.Collections.Generic;
using Raven.Client;
using Raven.Client.Linq;
using StructureMap;
using System.Linq;

namespace StatusUpdater.RavenRepositories
{

    public class RavenRepository<T> where T : IEntity
    {
        public T Save(T entity)
        {
            using (var session = ObjectFactory.GetInstance<IDocumentSession>())
            {
                session.Store(entity);
                session.SaveChanges();
                return entity; 
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
    }
}
