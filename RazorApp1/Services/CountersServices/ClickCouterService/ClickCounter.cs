using System.Collections.Concurrent;

namespace EmailSenderWebApi.Services.CountersServices.ClickCouterService
{
    public class ClickCounter : IClickCounter
    {
        private ConcurrentDictionary<string, int>? _clickCount;

        public ConcurrentDictionary<string, int> Count
        {
            get
            {
                if (_clickCount is null)
                {
                    _clickCount=new ( );
                    return _clickCount;
                }
                return _clickCount;
            }
            set
            {
                if (_clickCount is null)
                {
                    _clickCount=new ( );
                    _clickCount=value;
                }
                _clickCount=value;
            }
        }
    }
}
