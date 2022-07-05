using System.Collections;
<<<<<<< HEAD
using System.Runtime.CompilerServices;
=======
>>>>>>> Update_Collection

namespace AmmoraiteCollections
{
    public class ConcurrentList<T> : IEnumerable<T>
    {
<<<<<<< HEAD
        private object updatelocker = new();
=======
        private object updatelocker = new ( );
        private TasksQueue _ConcurrentListTasksQueue { get; set; } = new (100000);
>>>>>>> Update_Collection
        private T[] Items { get; set; }
        public int Count { get; private set; }
        public int Capacity { get; private set; }
        public T this[int index]
        {
            get
            {
<<<<<<< HEAD
                CheckOutOfRangeValueIndex (index);
                return Items[index];
            }
            set
            {
                CheckOutOfRangeValueIndex (index);
                Items[index]=value;
=======
                lock (updatelocker)
                {
                    lock (Items[index])
                    {
                        CheckOutOfRangeValueIndex (index);
                        return Items[index];
                    }
                }
            }
            set
            {
                lock (updatelocker)
                {
                    lock (Items[index])
                    {
                        CheckOutOfRangeValueIndex (index);
                        Items[index]=value;
                    }

                }
>>>>>>> Update_Collection
            }
        }

        #region Constructor

<<<<<<< HEAD
        public ConcurrentList ( ConcurrentList<T>  items ) => AddSeveralFromConcurrentList (items);
        public ConcurrentList ( ) => Items=new T[Capacity];

        #endregion

        private void CheckOutOfRangeValueIndex ( int index )
        {
            if (index > Count - 1 || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }
=======
        public ConcurrentList ( ConcurrentList<T> items ) => AddSeveralFromConcurrentList (items);
        public ConcurrentList ( )
        {
            Items=new T[Capacity];
        }

        #endregion

        #region PrivateMetods

        private void CheckOutOfRangeValueIndex ( int index )
        {
            if (index>Count-1||index<0)
            {
                throw new IndexOutOfRangeException ( );
            }
>>>>>>> Update_Collection
        }

        /// <summary>
        /// Увеличивает размер массива T Элементов
        /// </summary>
        private void UpSizeItemsArray ( int newSize )
        {
            lock (updatelocker)
            {
                T[] temporaryItemsArray = new T[newSize];
<<<<<<< HEAD
                for (int i = 0; i<Items.Length; i++)
                {
                    temporaryItemsArray[i]=Items[i];
                }

                Items=temporaryItemsArray;
            }
            
        }

        #region AddMetods

=======
                Parallel.For (0, Items.Length, ( int i ) =>
                {
                    temporaryItemsArray[i]=Items[i];
                });
                Items=temporaryItemsArray;
            }

        }

        #endregion

        #region AddMetods

        #region Additem
>>>>>>> Update_Collection
        /// <summary>
        /// Добавляет элемент в текуущую коллекцию 
        /// </summary>
        /// <param name="item"></param>
<<<<<<< HEAD
        public void Add ( T item )
=======
        /// 
        public void Add ( T item ) => _ConcurrentListTasksQueue.AddTask (new Task (( ) => AddItem (item)));
        private void AddItem ( T item )
>>>>>>> Update_Collection
        {
            lock (updatelocker)
            {
                if (Capacity==0)
                {
                    Capacity=4;
                    UpSizeItemsArray (Capacity);
<<<<<<< HEAD
=======
                    _ConcurrentListTasksQueue._WorkingEvent.Set ( );
>>>>>>> Update_Collection
                    Add (item);
                }
                else if (Count<Capacity)
                {
                    Items[Count]=item;
                    Count++;
                }
                else
                {
                    Capacity*=2;
                    UpSizeItemsArray (Capacity);
<<<<<<< HEAD
                    Add (item);
                }
            }
        }

=======
                    _ConcurrentListTasksQueue._WorkingEvent.Set ( );
                    Add (item);
                }
            }

        }
        #endregion

        #region AddSeveralItems
>>>>>>> Update_Collection
        /// <summary>
        /// Добавляет в текуущую коллекцию элементы из массива "itemsToAdd" начиная с "index"(по умолчанию index=0)
        /// </summary>
        /// <param name="itemsToAdd">Массив элементы которого будут добавлены начиная с index</param>
        /// <param name="index">индекс с которого будут добавлены элементы (по умолчанию 0)</param>
<<<<<<< HEAD
        public void AddSeveralFromConcurrentList ( ConcurrentList<T> itemsToAdd, int index = 0 )
=======
        public void AddSeveralFromConcurrentList ( ConcurrentList<T> itemsToAdd, int index = 0 ) =>
         _ConcurrentListTasksQueue.AddTask (new Task (( ) => AddSeveralItemFromConcurrentList (itemsToAdd, index)));
        private void AddSeveralItemFromConcurrentList ( ConcurrentList<T> itemsToAdd, int index = 0 )
>>>>>>> Update_Collection
        {
            lock (updatelocker)
            {
                for (int i = index; i<itemsToAdd.Count; i++)
                {
                    Add (itemsToAdd[i]);
                }
            }
        }
<<<<<<< HEAD
=======
        #endregion
>>>>>>> Update_Collection

        #endregion

        #region RemoveMetods

<<<<<<< HEAD
=======
        #region RemoveAllEquals

>>>>>>> Update_Collection
        /// <summary>
        /// Удаляет все входящие в колекцию элементы эквивалентные item
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
<<<<<<< HEAD
        public bool RemoveAllEquals ( params T[] items )
        {
            var a = false;
            lock (updatelocker)
            {
                
                for (int i = 0; i<items.Length; i++)
                {
                    for (int j = 0; j<Count; j++)
                    {
                        if (Items[j].Equals (items[i]))
                        {
                            a=RemoveOnIndex (j);
                        }
                    }
                }
            }
            return a;
        }

        /// <summary>
        /// Удаляет все элементы из колекции
        /// </summary>
        public void Clear()
        {
=======
        public void RemoveAllEquals ( params T[] items ) =>
            _ConcurrentListTasksQueue.AddTask (new Task (( ) => RemoveAllItemsEquals (items)));
        private void RemoveAllItemsEquals ( params T[] items )
        {
            _ConcurrentListTasksQueue._WorkingEvent.Set ( );
            _ConcurrentListTasksQueue._WorkingEvent.WaitOne ( );
            lock (updatelocker)
            {
                Parallel.For (0, items.Length, ( int i ) =>
                {
                    for (int j = 0; j<Count; j++)
                    {
                        if (Items!=null&&Items[j].Equals (items[i]))
                        {
                            RemoveOnIndex (j);
                        }
                    }
                });
            }
            _ConcurrentListTasksQueue._WorkingEvent.Set ( );

        }
        #endregion

        #region Clear
        /// <summary>
        /// Удаляет все элементы из колекции
        /// </summary>
        public void Clear ( ) =>
            _ConcurrentListTasksQueue.AddTask (new Task (( ) => ClearCollectionns ( )));
        private void ClearCollectionns ( )
        {
            _ConcurrentListTasksQueue._WorkingEvent.Set ( );
            _ConcurrentListTasksQueue._WorkingEvent.WaitOne ( );
>>>>>>> Update_Collection
            lock (updatelocker)
            {
                try
                {
<<<<<<< HEAD
=======
                    _ConcurrentListTasksQueue.Clear ( );
>>>>>>> Update_Collection
                    Items=new T[Capacity];
                }
                catch (Exception e)
                {
                    Console.WriteLine (e);
                    throw;
                }
            }
<<<<<<< HEAD
        }

=======
            _ConcurrentListTasksQueue._WorkingEvent.Set ( );
        }
        #endregion

        #region RemoveOnIndex
>>>>>>> Update_Collection
        /// <summary>
        /// удаляет элемент по индексу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
<<<<<<< HEAD
        public bool RemoveOnIndex ( int index )
        {
            lock (updatelocker)
            {
                if (index<0||index>Count)
                {
                    return false;
                }
                for (int j = index; j<Count; j++)
                {
                    if (Items[j]!=null&&j+1<=Count)
                    {
                        Items[j]=Items[j+1];
                    }
                    else
                    {
                        Items[j]=default;
                        break;
                    }
                }
                Count--;
                return true;
            }
        }
=======
        public void RemoveOnIndex ( int index ) =>
            _ConcurrentListTasksQueue.AddTask (new Task (( ) => RemoveItemOnIndex (index)));
        private void RemoveItemOnIndex ( int index )
        {
            _ConcurrentListTasksQueue._WorkingEvent.Set ( );
            _ConcurrentListTasksQueue._WorkingEvent.WaitOne ( );
            lock (updatelocker)
            {
                lock (Items[index])
                {
                    CheckOutOfRangeValueIndex (index);


                    for (int j = index; j<Count; j++)
                    {
                        if (Items[j]!=null&&j+1<=Count)
                        {
                            Items[j]=Items[j+1];
                        }
                        else
                        {
                            Items[j]=default;
                            break;
                        }
                    }
                    Count--;
                }
            }
            _ConcurrentListTasksQueue._WorkingEvent.Set ( );
        }
        #endregion

>>>>>>> Update_Collection

        #endregion

        #region Sort/FindMetod
<<<<<<< HEAD
        public IEnumerable<int> FindIndex ( Predicate<T> predicate )
        {
            for (int i = 0; i<Count; i++)
            {
                if (predicate.Invoke (this[i]))
                {
                    yield return i;
                }
            }
        }

        public void Sort ( )
        {
            lock (updatelocker)
            {
                Array.Sort (Items);
=======

        #region FindItemIndex
        public IEnumerable<int> FindItemIndex ( Predicate<T> predicate )
        {
            _ConcurrentListTasksQueue._WorkingEvent.Set ( );
            _ConcurrentListTasksQueue._WorkingEvent.WaitOne ( );
            lock (updatelocker)
            {
                for (int i = 0; i<Count; i++)
                {
                    if (predicate.Invoke (this[i]))
                    {
                        yield return i;
                    }
                }
            }
            _ConcurrentListTasksQueue._WorkingEvent.Set ( );
        }
        #endregion

        #region Contains
        public bool Contains ( Predicate<T> predicate )
        {
            lock (updatelocker)
            {
                _ConcurrentListTasksQueue._WorkingEvent.Set ( );
                _ConcurrentListTasksQueue._WorkingEvent.WaitOne ( );
                foreach (var item in Items)
                {
                    if (predicate.Invoke (item))
                    {
                        return true;
                    }
                }
                _ConcurrentListTasksQueue._WorkingEvent.Set ( );
            }
            return false;
        }
        #endregion

        #region Sort
        public void Sort ( ) =>
            _ConcurrentListTasksQueue.AddTask (new Task (( ) => SortItems ( )));
        private void SortItems ( )
        {
            lock (updatelocker)
            {
                _ConcurrentListTasksQueue._WorkingEvent.Set ( );
                _ConcurrentListTasksQueue._WorkingEvent.WaitOne ( );
                Array.Sort (Items);
                _ConcurrentListTasksQueue._WorkingEvent.Set ( );
>>>>>>> Update_Collection
            }
        }
        #endregion

<<<<<<< HEAD
        #region IEnumerable<T>

        public IEnumerator<T> GetEnumerator ( )
        {
            return Items.Take (Count).Where(x=>x!=null).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator ( )
        {
            return Items.GetEnumerator ( );
        }



=======
        #endregion

        #region IEnumerable<T>
        public IEnumerator<T> GetEnumerator ( )
        {
            lock (updatelocker)
            {
                _ConcurrentListTasksQueue._WorkingEvent.Set ( );
                _ConcurrentListTasksQueue._WorkingEvent.WaitOne ( );
                var a = Items.Take (Count).Where (x => x!=null).GetEnumerator ( );
                _ConcurrentListTasksQueue._WorkingEvent.Set ( );
                return a;
            }
        }
        IEnumerator IEnumerable.GetEnumerator ( )
        {
            lock (updatelocker)
            {
                _ConcurrentListTasksQueue._WorkingEvent.Set ( );
                _ConcurrentListTasksQueue._WorkingEvent.WaitOne ( );
                var a = Items.Take (Count).Where (x => x!=null).GetEnumerator ( );
                _ConcurrentListTasksQueue._WorkingEvent.Set ( );
                return a;
            }
        }
>>>>>>> Update_Collection
        #endregion
    }
}