using System.Collections.Concurrent;

namespace EmailSenderWebApi.DomainEvents
{
    public static class DomainEventsManager
    {
        private static ConcurrentDictionary<Type,List<Delegate>> _handlers = new ( );
        public static void Register<IDomainEvent> (Action<IDomainEvent> eventHandler )
        {
            try
            {              
                _handlers[typeof (IDomainEvent)].Add (eventHandler);              
            }
            catch (Exception e)
            {
                _handlers[typeof (IDomainEvent)]=new ( );
                _handlers[typeof (IDomainEvent)].Add (eventHandler);
            }
        }

        public static void Raise<IDomainEvent> ( IDomainEvent domainEvent )
        {
            foreach (Delegate handler in _handlers[typeof (IDomainEvent)])
            {
                var action = (Action<IDomainEvent>) handler;
                action (domainEvent);
            }
        }
    }
}
