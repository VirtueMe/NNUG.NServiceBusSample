namespace NNUG.Prosjekt.Client.Events
{
    using System.Threading;

    public interface IEventAggregator
    {
        void SendMessage<T>(T message);

        void AddListener<T>(IListener<T> listener);
        void AddListener(object listener);

        void RemoveListener<T>(IListener<T> listener);
        void SetContext(SynchronizationContext current);
    }
}