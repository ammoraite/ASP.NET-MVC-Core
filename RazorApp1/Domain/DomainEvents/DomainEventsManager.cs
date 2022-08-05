namespace EmailSenderWebApi.DomainEvents
{
    public static class DomainEventsManager
    {
        private static readonly Dictionary<Type, List<Delegate>> _handlers = new ( );


        public static void Register<IDomainEvent> ( Action<IDomainEvent> eventHandler )
        {
            try
            {
                _handlers[typeof (IDomainEvent)].Add (eventHandler);
            }
            catch (Exception)
            {

                _handlers[typeof (IDomainEvent)]=new ( );
                _handlers[typeof (IDomainEvent)].Add (eventHandler);
            }
        }

        public static Task Raise<IDomainEvent> ( IDomainEvent domainEvent )
        {
            var task = new Task (( ) =>
            {
                foreach (Delegate handler in _handlers[typeof (IDomainEvent)])
                {
                    var action = (Action<IDomainEvent>) handler;
                    action (domainEvent);
                }

            });
            task.Start ( );
            return task;
        }
    }
}
