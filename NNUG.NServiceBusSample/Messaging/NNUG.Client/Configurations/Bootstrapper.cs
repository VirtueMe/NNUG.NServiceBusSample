using System.Threading;
using NNUG.Prosjekt.Client.Events;
using StructureMap;

namespace NNUG.Prosjekt.Client.Configurations
{
    public class Bootstrapper : IBootstrapper
    {
        private static bool hasStarted;

        public static void Restart()
        {

            if (hasStarted)
            {
                ObjectFactory.ResetDefaults();
            }
            else
            {
                Bootstrap();

                hasStarted = true;
            }
        }

        public static void Bootstrap()
        {
            new Bootstrapper().BootstrapStructureMap();
        }

        public void BootstrapStructureMap()
        {
            ObjectFactory.Initialize(x =>
            {
                x.ForRequestedType<IEventAggregator>().AsSingletons().Use<EventAggregator>();
                x.RegisterInterceptor(new EventAggregatorInterceptor());
                x.UseDefaultStructureMapConfigFile = false;
            });
        }
    }
}