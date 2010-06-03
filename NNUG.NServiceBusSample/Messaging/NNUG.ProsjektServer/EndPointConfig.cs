namespace NNUG.ProsjektServer
{
    using NServiceBus;
    using NServiceBus.ObjectBuilder;

    public class EndPointConfig : AsA_Server, IConfigureThisEndpoint, IWantCustomInitialization
    {
        public void Init()
        {
            var config = Configure.With()
                .DefaultBuilder()
                .XmlSerializer();

            config.Configurer.ConfigureComponent<Session>(ComponentCallModelEnum.Singleton);
        }
    }
}
