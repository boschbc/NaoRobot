using System;
using System.Collections.Generic;

namespace Naovigate.Util
{
    public class PriorityQueue<T>
    {
        private Queue<T>[] queues;
        private int maxPriority;
        private int size = 0;

        public PriorityQueue() : this(10) { }

        public PriorityQueue(int maxPriority)
        {
            this.maxPriority = maxPriority;
            queues = new Queue<T>[maxPriority];
        }

        /*
         * return the first, highest priority item
         */
        public T Dequeue()
        {
            // if its empty, why search array of queues
            if (IsEmpty()) return default(T);

            // look for the highest priority queue
            for (int i = maxPriority - 1; i >= 0; i--)
            {
                // queue exists
                if (queues[i] != null)
                {
                    // check if its empty
                    Queue<T> q = queues[i];
                    if (q.Count > 0)
                    {
                        size--;
                        T t = q.Dequeue();

                        // no more elements in the queue, save some memory
                        if (q.Count == 0) queues[i] = null;
                        return t;
                    }
                }
            }
            return default(T);
        }

        public void Enqueue(T t, int priority)
        {
            if (priority > maxPriority) throw new ArgumentException("Priority above maximum");
            if (queues[priority] == null)
            {
                // the queue for this priority has not been used yet, or has been emptied, create it
                queues[priority] = new Queue<T>();
            }

            // add the item to the apropriate queue, it should've been created now
            queues[priority].Enqueue(t);
            size++;
        }

        public Boolean IsEmpty()
        {
            return size == 0;
        }

        public int Size()
        {
            return size;
        }

        public void Clear()
        {
            while (!IsEmpty())
            {
                Dequeue();
            }
        }

        public override String ToString()
        {
            String res = "";
            foreach (Queue<T> q in queues)
            {
                if (q != null)
                {
                    res += "Q:";
                    foreach (T t in q)
                    {
                        res += t + ",";
                    }
                    res += "\n";
                }
            }
            return res;
        }
    }
}
