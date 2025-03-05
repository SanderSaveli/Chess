using IUP.Toolkit;

namespace OFG.ChessPeak
{
    public static class EventBusProvider
    {
        public static IEventBus EventBus
        {
            get
            {
                _eventBus ??= new EventBus();
                return _eventBus;
            }
        }

        private static EventBus _eventBus;
    }
}
