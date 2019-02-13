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
        /// Inserta un nuevo objeto en el heap. Se admiten repeticiones. La insercion se realiza
        /// con tiempo de ejecucion de O(log(n)), siendo n la cantidad de elementos del heap.
        /// </summary>
        /// <param name="data">el objeto a insertar.</param>
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
        /// Remueve todos los elementos del heap y lo deja vacio. La capacidad vuelve
        /// al valor con el que inicialmente se creo el heap. El tipo de heap (ascendente
        /// o descendente) se mantiene igual al que se tenia antes de invocar a clear.
        /// </summary>
        public void Clear()
        {
            Array = new T[InitialCapacity];
            Count = 0;
        }

        /// <summary>
        /// Retorna el elemento de la cima del Heap, sin removerlo. Si el Heap
        /// es de tipo ascendente, el objeto retornado sera el menor del heap,
        /// y si el heap es descendente, el objeto retornado sera el mayor. Si
        /// el heap esta vacio, retorna null.
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            if (IsEmpty()) { return default(T); }
            return Array[0];
        }

        /// <summary>
        /// Permite determinar si el Heap esta vacio. Retorna true si el Heap esta vacio.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return (Count == 0);
        }

        /// <summary>
        /// Obtiene y retorna el elemento de la cima del heap. Si el heap es de 
        /// tipo ascendente, ese elemento sera el menor del heap. En caso contrario 
        /// sera el mayor. Rearma el heap con los elementos restantes, de forma que
        /// luego de terminada la operacion, la cima vuelve a contener al menor (o al
        /// mayor) de los elementos que quedaban, y la cantidad de elementos se reduce
        /// en uno. Si el heap esta vacio, retorna null. Tiempo de ejecucion esperado
        /// (en el peor caso): O(log(n)).
        /// </summary>
        /// <returns>el elemento menor (o el mayor) del heap.</returns>
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
        /// Retorna la cantidad de elementos contenidos en el Heap.
        /// </summary>
        /// <returns>el tamaño del Heap.</returns>
        public int Size()
        {
            return Count;
        }

        /// <summary>
        /// Retorna el tipo de Heap: true si es ascendente, false si es descendente.
        /// </summary>
        /// <returns>true: el heap es ascendente - false: es descendente.</returns>
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
        /// Chequea dos elementos hermanos y retorna true si el optimo es el izquierdo, o false si es
        /// el derecho. Entendemos por "optimo" al menor de los dos si el heap es ascendente, o al mayor
        /// si el heap es descendente.
        /// </summary>
        /// <param name="sl"></param>
        /// <param name="sr"></param>
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
        /// Retorna true el elemento en la posicion s del heap deberia intercambiarse con el
        /// elemento en la posicion f del heap. Se supone que heap[s] es hijo (izquierdo o 
        /// derecho) de heap[f], aunque el metodo no valida ese supuesto. La comprobacion se
        /// realiza de acuerdo al tipo de heap: si es un heap ascendente, se retornara true si
        /// heap[s] menor a heap[f], pero si el heap es descendente se retornara true si heap[s] mayor a heap[f].
        /// </summary>
        /// <param name="s"></param>
        /// <param name="f"></param>
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
