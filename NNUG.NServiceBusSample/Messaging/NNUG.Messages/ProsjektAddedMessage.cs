namespace NNUG.Messages
{
    using NServiceBus;

    public class ProsjektAddedMessage : IMessage
    {
        public string Id { get; set; }
    }
}
