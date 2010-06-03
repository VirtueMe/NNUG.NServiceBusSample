namespace NNUG.Prosjekt.Client.Events
{
    public interface IListener<T>
    {
        void HandleMessage(T message);
    }
}
