﻿using System.Collections;


namespace AmmoraiteCollections
{
    public class ConcurrentList<T> : IEnumerable<T>
    {
        private object _updatelocker = new ( );

        private ActionQueue _oncurrentListActionQueue { get; set; } = new (100000);
        private T[] Items { get; set; }
        public int Count { get; private set; }
        public int Capacity { get; private set; }
        public T this[int index]
        {
            get
            {
                lock (_updatelocker)
                {
                    CheckOutOfRangeValueIndex (index);
                    return Items[index];
                }
            }

            set
            {
                lock (_updatelocker)
                {
                    lock (Items[index])
                    {
                        CheckOutOfRangeValueIndex (index);
                        Items[index]=value;
                    }

                }

            }
        }

        #region Constructor

        public ConcurrentList ( ConcurrentList<T> items ) => AddSeveralFromConcurrentList (items);
        public ConcurrentList ( ) => Items=new T[Capacity];

        #endregion

        #region PrivateMetods
        private void CheckOutOfRangeValueIndex ( int index )
        {
            if (index>Count-1||index<0)
            {
                throw new IndexOutOfRangeException ( );
            }

        }

        /// <summary>
        /// Увеличивает размер массива T Элементов
        /// </summary>
        private void UpSizeItemsArray ( int newSize )
        {
            lock (_updatelocker)
            {
                T[] temporaryItemsArray = new T[newSize];

                for (int i = 0; i<Items.Length; i++)
                {
                    temporaryItemsArray[i]=Items[i];
                }

                Items=temporaryItemsArray;
            }

        }
        #endregion

        #region AddMetods

        #region Additem

        /// <summary>
        /// Добавляет элемент в текуущую коллекцию 
        /// </summary>
        /// <param name="item"></param>
        public void Add ( T item ) =>
            _oncurrentListActionQueue.AddAction (( ) => AddItem (item));
        private void AddItem ( T item )

        {
            lock (_updatelocker)
            {
                if (Capacity==0)
                {
                    Capacity=4;
                    UpSizeItemsArray (Capacity);
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
                    Add (item);
                }
            }
        }
        #endregion

        #region AddSeveralItems

        /// <summary>
        /// Добавляет в текуущую коллекцию элементы из массива "itemsToAdd" начиная с "index"(по умолчанию index=0)
        /// </summary>
        /// <param name="itemsToAdd">Массив элементы которого будут добавлены начиная с index</param>
        /// <param name="index">индекс с которого будут добавлены элементы (по умолчанию 0)</param>
        public void AddSeveralFromConcurrentList ( ConcurrentList<T> itemsToAdd, int index = 0 ) =>
         _oncurrentListActionQueue.AddAction (( ) => AddSeveralItemFromConcurrentList (itemsToAdd, index));
        private void AddSeveralItemFromConcurrentList ( ConcurrentList<T> itemsToAdd, int index = 0 )

        {
            lock (_updatelocker)
            {
                for (int i = index; i<itemsToAdd.Count; i++)
                {
                    Add (itemsToAdd[i]);
                }
            }
        }


        #endregion

        #endregion

        #region RemoveMetods

        #region RemoveAllEquals

        /// <summary>
        /// Удаляет все входящие в колекцию элементы эквивалентные item
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public void RemoveAllEquals ( params T[] items ) => _oncurrentListActionQueue.AddAction (( ) => RemoveAllEqualsItems (items));
        private void RemoveAllEqualsItems ( params T[] items )
        {
            lock (_updatelocker)
            {

                for (int i = 0; i<items.Length; i++)
                {
                    for (int j = 0; j<Count; j++)
                    {
                        if (Items[j].Equals (items[i]))
                        {
                            RemoveOnIndex (j);
                        }
                    }
                }
            }
        }

        #endregion

        #region Clear
        /// <summary>
        /// Удаляет все элементы из колекции
        /// </summary>
        public void Clear ( ) =>
            _oncurrentListActionQueue.AddAction (( ) => ClearCollectionns ( ));
        private void ClearCollectionns ( )
        {
            lock (_updatelocker)
            {
                try
                {
                    Items=new T[Capacity];
                }
                catch (Exception e)
                {
                    Console.WriteLine (e);
                    throw;
                }
            }
        }

        #endregion

        #region RemoveOnIndex
        public void RemoveOnIndex ( int index ) =>
            _oncurrentListActionQueue.AddAction (( ) => RemoveItemOnIndex (index));
        private void RemoveItemOnIndex ( int index )
        {
            lock (_updatelocker)
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
        }
        #endregion

        #endregion

        #region Sort/FindMetod

        #region GetIndex
        public IEnumerable<int> GetIndex ( Predicate<T> predicate )
        {
            lock (_updatelocker)
            {
                for (int i = 0; i<Count; i++)
                {
                    if (predicate.Invoke (this[i]))
                    {
                        yield return i;
                    }
                }
            }

        }
        #endregion

        #region Contains
        public bool Contains ( Predicate<T> predicate )
        {
            lock (_updatelocker)
            {
                foreach (var item in Items)
                {
                    if (predicate.Invoke (item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region Sort
        public void Sort ( ) =>
            _oncurrentListActionQueue.AddAction (( ) => SortItems ( ));
        private void SortItems ( )
        {
            lock (_updatelocker)
            {
                Array.Sort (Items);
            }
        }
        #endregion

        #endregion

        #region IEnumerable<T>
        public IEnumerator<T> GetEnumerator ( )
        {
            lock (_updatelocker)
            {
                var a = Items.Take (Count).Where (x => x!=null).GetEnumerator ( );
                return a;
            }
        }
        IEnumerator IEnumerable.GetEnumerator ( )
        {
            lock (_updatelocker)
            {
                var a = Items.Take (Count).Where (x => x!=null).GetEnumerator ( );
                return a;
            }
        }

        #endregion
    }
}