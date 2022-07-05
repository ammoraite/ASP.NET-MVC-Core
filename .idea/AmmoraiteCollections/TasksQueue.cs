namespace AmmoraiteCollections
{
    public class TasksQueue
    {
        private Queue<Task> _tasks { get; set; }
        public int Size { get => _tasks.Count; }
        public bool StopWork { get; set; } = false;
        private object _lock = new object ( );

        public AutoResetEvent _WorkingEvent = new (true);
        public TasksQueue ( int size=100 )
        {
            _tasks=new Queue<Task> (size);
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
        public void AddTask ( Task task )
        {
            if (!StopWork)
            {
                lock (_lock)
                {
                    _tasks.Enqueue (task);
                    _WorkingEvent.Set ( );
                    while (Size>=1)
                    {
                        if (Size>=1&&_tasks.Peek ( )!=null)
                        {
                            _WorkingEvent.WaitOne ( );
                            _tasks.Dequeue ( ).RunSynchronously ( );
                            _WorkingEvent.Set ( );
                        }
                    }
                }
            }
        }
    }
}



