namespace NNUG.Prosjekt.Client.Handlers
{
    using Events;
    
    using Messages;
    
    using NServiceBus;
    
    public class ProsjektSavedMessageHandler : IHandleMessages<ProsjektAddedMessage>
    {
        public IEventAggregator EventAggregator { get; set; }
        
        public void Handle(ProsjektAddedMessage message)
        {
            EventAggregator.SendMessage(new ProsjektAdded { Id = message.Id });    
        }
    }
}
