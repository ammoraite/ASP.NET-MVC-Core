using System.Collections;
using System.Runtime.CompilerServices;

namespace AmmoraiteCollections
{
    public class ConcurrentList<T> : IEnumerable<T>
    {
        private object updatelocker = new();
        private T[] Items { get; set; }
        public int Count { get; private set; }
        public int Capacity { get; private set; }
        public T this[int index]
        {
            get
            {
                CheckOutOfRangeValueIndex (index);
                return Items[index];
            }
            set
            {
                CheckOutOfRangeValueIndex (index);
                Items[index]=value;
            }
        }

        #region Constructor

        public ConcurrentList ( ConcurrentList<T>  items ) => AddSeveralFromConcurrentList (items);
        public ConcurrentList ( ) => Items=new T[Capacity];

        #endregion

        private void CheckOutOfRangeValueIndex ( int index )
        {
            if (index > Count - 1 || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }
        }

        /// <summary>
        /// Увеличивает размер массива T Элементов
        /// </summary>
        private void UpSizeItemsArray ( int newSize )
        {
            lock (updatelocker)
            {
                T[] temporaryItemsArray = new T[newSize];
                for (int i = 0; i<Items.Length; i++)
                {
                    temporaryItemsArray[i]=Items[i];
                }

                Items=temporaryItemsArray;
            }
            
        }

        #region AddMetods

        /// <summary>
        /// Добавляет элемент в текуущую коллекцию 
        /// </summary>
        /// <param name="item"></param>
        public void Add ( T item )
        {
            lock (updatelocker)
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

        /// <summary>
        /// Добавляет в текуущую коллекцию элементы из массива "itemsToAdd" начиная с "index"(по умолчанию index=0)
        /// </summary>
        /// <param name="itemsToAdd">Массив элементы которого будут добавлены начиная с index</param>
        /// <param name="index">индекс с которого будут добавлены элементы (по умолчанию 0)</param>
        public void AddSeveralFromConcurrentList ( ConcurrentList<T> itemsToAdd, int index = 0 )
        {
            lock (updatelocker)
            {
                for (int i = index; i<itemsToAdd.Count; i++)
                {
                    Add (itemsToAdd[i]);
                }
            }
        }

        #endregion

        #region RemoveMetods

        /// <summary>
        /// Удаляет все входящие в колекцию элементы эквивалентные item
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
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
            lock (updatelocker)
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

        /// <summary>
        /// удаляет элемент по индексу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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

        #endregion

        #region Sort/FindMetod
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
            }
        }
        #endregion

        #region IEnumerable<T>

        public IEnumerator<T> GetEnumerator ( )
        {
            return Items.Take (Count).Where(x=>x!=null).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator ( )
        {
            return Items.GetEnumerator ( );
        }



        #endregion
    }
}