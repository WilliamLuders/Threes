using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threes
{
    //stack for saving the last n game states
    class ShortStack<T> :LinkedList<T>
    {
        private int maxCount=3;
        private T last;

        public ShortStack(int maxSize)
        {
            LinkedList<T> stack = new LinkedList<T>();
        }

        public void Push(T item)
        {
            AddLast(item);

            while (this.Count() >= maxCount+1) // Count includes an extra null node because it is linked
            {
                RemoveFirst(); // remove oldest move
            }
        }

        public T Pop()
        {
            if (this.Count() > 1)
            {
                last = Last.Value;
                RemoveLast();
                return last; //must return the .value field of Last
            }
            return Last.Value;
        }
    }
}
