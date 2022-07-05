using System.Collections.Concurrent;

namespace AmmoraiteCollections
{
    public class ConcurrentQueueTasks
    {
        private ConcurrentQueue<Task> _tasks { get; set; } = new ( );
        private Task CurrentTask;
        public int Size { get => _tasks.Count; }
        public bool StopWork { get; set; } = false;
        private object _lock = new object ( );
        public void Clear ( )
        {
            StopWork=true;
            _tasks.Clear ( );
            StopWork=false;
        }
        public void AddTask ( Task task )
        {
            if (!StopWork)
            {
                lock (task)
                {
                    _tasks.Enqueue (task);
                    while (_tasks.Count>=1)
                    {
                        _tasks.TryDequeue (out CurrentTask);
                        CurrentTask.Start ( );
                    }
                }
            }
        }
    }
}



