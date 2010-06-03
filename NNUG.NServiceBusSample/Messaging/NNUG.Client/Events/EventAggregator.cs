namespace NNUG.Prosjekt.Client.Events
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, List<object>> listeners = new Dictionary<Type, List<object>>();

        private SynchronizationContext context;

        public EventAggregator()
            : this(SynchronizationContext.Current ?? new SynchronizationContext())
        {
        }

        public EventAggregator(SynchronizationContext context)
        {
            this.context = context;
        }

        public void SendMessage<T>(T message)
        {
            if (listeners.ContainsKey(typeof(T)))
            {
                var listenersList = listeners[typeof(T)];

                if (listenersList.Count > 0)
                {
                    listenersList.ForEach(x => SendAction(() => Send(x as IListener<T>, message)));
                }
            }
        }

        public void AddListener<T>(IListener<T> listener)
        {
            Type type = typeof(T);
            AddListener(type, listener);
        }

        public void AddListener(object listener)
        {
            Type[] interfaces = listener.GetType().GetInterfaces();

            foreach (Type type in interfaces)
            {
                if (type.ImplementedByGenericType(typeof(IListener<>)))
                {
                    Type[] arguments = type.GetGenericArguments();

                    AddListener(arguments[0], listener);
                }
            }
        }

        public void RemoveListener<T>(IListener<T> listener)
        {
            if (listeners.ContainsKey(typeof(T)))
            {
                var listenerList = listeners[typeof(T)];

                if (listenerList.Contains(listener))
                {
                    listenerList.Remove(listener);
                }

            }
        }

        public void SetContext(SynchronizationContext current)
        {
            context = current;
        }

        protected virtual void SendAction(Action action)
        {
            GetContext().Send(state => action(), null);
        }

        private static void Send<T>(IListener<T> listener, T message)
        {
            if (listener != null)
            {
                listener.HandleMessage(message);
            }
        }

        private SynchronizationContext GetContext()
        {
            if (context == null)
            {
                context = SynchronizationContext.Current;
            }
            return context;
        }
        
        private void AddListener(Type type, object listener)
        {
            var listenerList = new List<object>();

            if (listeners.ContainsKey(type) == false)
            {
                listeners.Add(type, listenerList);
            }
            else
            {
                listenerList = listeners[type];
            }

            listenerList.Add(listener);
        }
    }
}