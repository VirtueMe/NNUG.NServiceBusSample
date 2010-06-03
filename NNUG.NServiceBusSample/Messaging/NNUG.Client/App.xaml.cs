namespace NNUG.Prosjekt.Client
{
    using System.Threading;
           
    using Caliburn.PresentationFramework.ApplicationModel;
    using Caliburn.StructureMap;
    
    using Configurations;
    using Events;
    
    using Microsoft.Practices.ServiceLocation;

    using NServiceBus;
    using NServiceBus.ObjectBuilder;
    
    using StructureMap;

    using ViewModels.Interfaces;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override IServiceLocator CreateContainer()
        {
            Bootstrapper.Bootstrap();

            var adapter = new StructureMapAdapter(ObjectFactory.Container);

            InitNServiceBus();

            return adapter;
        }

        protected override object CreateRootModel()
        {
            var binder = (DefaultBinder) Container.GetInstance<IBinder>();

            binder.EnableMessageConventions();
            binder.EnableBindingConventions();

            SetContext();

            return Container.GetInstance<IMainWindowViewModel>();
        }
        
        private static void InitNServiceBus()
        {
            var config = Configure.With()
                   .Log4Net()
                   .StructureMapBuilder(ObjectFactory.Container)
                   .XmlSerializer()
                   .MsmqTransport()
                   .UnicastBus()
                       .LoadMessageHandlers();

            config.Configurer.ConfigureComponent<EventAggregator>(ComponentCallModelEnum.Singleton);

            config
                   .CreateBus()
                   .Start();            
        }

        /// <summary>
        /// PGA autoregistrering må vi sette contexten i synk med WPF
        /// </summary>
        private void SetContext()
        {
            var eventA = Container.GetInstance<IEventAggregator>();
            
            eventA.SetContext(SynchronizationContext.Current);
        }
    }
}
