using System;
using NNUG.Prosjekt.Client.Events;
using StructureMap;
using StructureMap.Interceptors;

namespace NNUG.Prosjekt.Client.Configurations
{
    public class EventAggregatorInterceptor : TypeInterceptor
    {

        public object Process(object target, IContext context)
        {
            context.GetInstance<IEventAggregator>().AddListener(target);

            return target;
        }

        public bool MatchesType(Type type)
        {
            Console.WriteLine(type.ToString());
            return type.ImplementsInterfaceTemplate(typeof(IListener<>));
        }
    }
}