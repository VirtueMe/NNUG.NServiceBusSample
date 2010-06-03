using System;

namespace NNUG.ProsjektServer
{
    using Raven.Client.Document;

    public class Session : ISession
    {
        public void Save(params object[] entities)
        {
            using (var documentStore = new DocumentStore { Url = "http://localhost:8080" })
            {
                documentStore.Initialize();

                using (var documentSession = documentStore.OpenSession())
                {
                    Array.ForEach(entities, documentSession.Store);
                    documentSession.SaveChanges();
                }
            }
        }
    }
}