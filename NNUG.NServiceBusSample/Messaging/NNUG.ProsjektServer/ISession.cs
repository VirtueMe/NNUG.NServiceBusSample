namespace NNUG.ProsjektServer
{
    public interface ISession
    {
        void Save(params object[] entity);
    }
}