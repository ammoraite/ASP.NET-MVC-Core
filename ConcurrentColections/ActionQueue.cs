namespace AmmoraiteCollections
{
    public class ActionQueue
    {
        private Queue<Action> _tasks { get; set; }
        public int Size { get => _tasks.Count; }
        public bool StopWork { get; set; } = false;

        private object _lock = new object ( );
        public ActionQueue ( int size )
        {
            _tasks=new Queue<Action> (size);
        }
        public void Clear ( )
        {
            lock (_lock)
            {
                StopWork=true;
                _tasks.Clear ( );
                StopWork=false;
            }
        }
        public void AddAction ( Action action )
        {
            if (!StopWork)
            {
                lock (_lock)
                {
                    _tasks.Enqueue (action);
                    while (Size>=1)
                    {
                        if (Size>=1&&_tasks.Peek ( )!=null)
                        {
                            _tasks.Dequeue ( ).Invoke ( );
                        }
                    }
                }
            }
        }
    }
}



