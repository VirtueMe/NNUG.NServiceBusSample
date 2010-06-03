using log4net;

namespace NNUG.ProsjektServer
{
    using Messages;
    
    using NServiceBus;
    
    using Prosjekt.Domain;

    public class ProsjektMessageHandler : IHandleMessages<ProsjektMessage>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProsjektMessageHandler));

        public ISession Session { get; set; }
        public IBus Bus { get; set; }

        public void Handle(ProsjektMessage message)
        {
            var prosjektTimer = new ProsjektTimer { Dato = message.Dato, Prosjekt = message.Navn, Timer = message.Antall };
            
            Session.Save(prosjektTimer);

            Bus.Reply(new ProsjektAddedMessage { Id = prosjektTimer.Id });
            
            Logger.Info(string.Format("Prosjektet informasjonen ble lagret med id {0}", prosjektTimer.Id));
            
        }
    }
}
