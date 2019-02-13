using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.Data
{
    public class Heap<T> where T : Comparable
    {
        private T[] Array { get; set; } // array that represents the tree.
        private int InitialCapacity { get; } // Initial capacity set to the heap
        private int Count { get; set; } // Heap elements count.
        private bool Ascendent { get; } // Orders ascendent/descendent the heap

        public Heap(int n = 100, bool ascendent = true)
        {
            if (n < 0) n = 100;
            Array = new T[n];
            InitialCapacity = n;
            Ascendent = ascendent;
        }

        /// <summary>
        /// Inserts new object to the heap
        /// </summary>
        /// <param name="data">object to insert.</param>
        public void Add(T data)
        {
            if (!IsHomogeneus(data)) { return; }

            int n = Array.Length;
            if (Count == n) { AdjustCapacity(2 * n); }

            int s = Count;
            Array[s] = data;
            while (s != 0)
            {
                int f = (s - 1) / 2;

                if (!ValidChange(s, f)) { break; }

                T aux = Array[s];
                Array[s] = Array[f];
                Array[f] = aux;
                s = f;
            }

            Count++;
        }

        /// <summary>
        /// Removes all elements of the heap
        /// </summary>
        public void Clear()
        {
            Array = new T[InitialCapacity];
            Count = 0;
        }

        /// <summary>
        /// Returns the element that is in the highest position of the heap without removing it
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            if (IsEmpty()) { return default(T); }
            return Array[0];
        }

        /// <summary>
        /// Checks if the heap is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return (Count == 0);
        }

        /// <summary>
        /// Gets and returns the element that is in the highest position of the heap and removes it
        /// </summary>
        /// <returns></returns>
        public T Remove()
        {
            if (IsEmpty()) { return default(T); }

            T x = Array[0];

            Array[0] = Array[Count - 1];
            Count--;

            // if there's too much space left, reduce the array...
            int n = Array.Length;
            int ic = InitialCapacity;
            if (n / 2 >= ic && Count > ic && Count < (n / 2) * 0.9) { AdjustCapacity(n / 2); }

            int f = 0;
            while (f < Count)
            {
                // calculate left son's (sl) and right son's (sr) directions...
                int sl = f * 2 + 1;
                int sr = sl + 1;

                // if there are no sons break...
                if (sl >= Count) { break; }

                // keep son's index in s as he's the one to move down
                int s = sl;
                if (sr < Count && !OptimalLeft(sl, sr)) { s = sr; }

                // if that son should not change places with the father, break...
                if (!ValidChange(s, f)) { break; }

                // otherwise, exchange father (Array(f)) with the correct son (Array[s])...
                T aux = Array[f];
                Array[f] = Array[s];
                Array[s] = aux;

                // continue from the son...
                f = s;
            }

            // ... y devolver el elemento que estaba originalmente en la cima...
            return x;
        }

        /// <summary>
        /// Count of elements of the heap
        /// </summary>
        /// <returns>Heap size</returns>
        public int Size()
        {
            return Count;
        }

        /// <summary>
        /// Returns if the heap is ascendent
        /// </summary>
        /// <returns></returns>
        public bool IsAscendent()
        {
            return Ascendent;
        }

        private void AdjustCapacity(int nc)
        {
            int n = Array.Length;

            // How many elements must be moved
            int q = n;
            if (nc < n) { q = Count; }

            // Moving elements
            var newHeap = new T[nc];
            System.Array.Copy(Array, 0, newHeap, 0, q);
            Array = newHeap;
        }

        /// <summary>
        /// Checks two siblings elements and returns true if the optimal is the left one, or false if
        /// the optimal is the right one
        /// </summary>
        /// <param name="sl">Left son</param>
        /// <param name="sr">Right son</param>
        /// <returns></returns>
        private bool OptimalLeft(int sl, int sr)
        {
            int r = Array[sl].CompareTo(Array[sr]);

            // if the heap is ascendent and the left one is less than the right one, return true 
            if (Ascendent && r < 0) { return true; }

            // if the heap is descendent and the left one is bigger than the right one, return true 
            if (!Ascendent && r > 0) { return true; }

            // the optimal is not the left one
            return false;
        }

        /// <summary>
        /// Return true if the son in s position should be exchanged with the father in f position
        /// </summary>
        /// <param name="s">Position of son element</param>
        /// <param name="f">Position of father element</param>
        /// <returns></returns>
        private bool ValidChange(int s, int f)
        {
            // Compare the son (Array[s]) with the father (Array[f])
            int r = Array[s].CompareTo(Array[f]);

            // if the minor should be up, but it's down, return true    
            if (Ascendent && r < 0) { return true; }

            // if the major should be up, but it's down, return true    
            if (!Ascendent && r > 0) { return true; }

            // else it's ok
            return false;
        }

        private bool IsHomogeneus(object x)
        {
            if (x == null) { return false; }
            if (Array[0] != null && x.GetType() != Array[0].GetType()) { return false; }
            return true;
        }

        public T[] GetHeap()
        {
            return Array;
        }
    }
}
