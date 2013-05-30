using System;
using System.Collections.Generic;

namespace Naovigate.Util
{
    /// <summary>
    /// An implementation of a simple Queue interface, while linking each item to an integer representing a priority.
    /// Items are dequeued in order of highest to lowest priority.
    /// </summary>
    /// <typeparam name="T">Any object type.</typeparam>
    public class PriorityQueue<T>
    {
        private Queue<T>[] queues;
        private int maxPriority;
        private int size = 0;

        /// <summary>
        /// Defaul constructor.
        /// Maximum priority set to 10 by default.
        /// </summary>
        public PriorityQueue() : this(10) { }

        /// <summary>
        /// Create a new PriorityQueue with given maximum priority.
        /// </summary>
        /// <param name="maxPriority">The maximum priority possible for this queue.</param>
        public PriorityQueue(int maxPriority)
        {
            this.maxPriority = maxPriority;
            queues = new Queue<T>[maxPriority];
        }

        /// <summary>
        /// Returns the highest priority item in the queue, or a default value if queue is empty.
        /// The item is not removed from the queue.
        /// </summary>
        /// <returns>The highest priority item in the queue.</returns>
        public T Peek()
        {
            return Element(false);
        }

        /// <summary>
        /// Returns the highest priority item in the queue, or a default value if queue is empty.
        /// The item is removed from the queue.
        /// </summary>
        /// <returns>The highest priority item in the queue.</returns>
        public T Dequeue()
        {
            return Element(true);
        }

        /// <summary>
        /// Returns the highest priority item in the queue, or a default value if queue is empty.
        /// The item may or may not be removed, depending on the argument used.
        /// </summary>
        /// <returns>The highest priority item in the queue.</returns>
        private T Element(bool remove)
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
                        if(remove) size--;
                        T t = remove ? q.Dequeue() : q.Peek();

                        // no more elements in the queue, save some memory
                        if (q.Count == 0) queues[i] = null;
                        return t;
                    }
                }
            }
            return default(T);
        }

        /// <summary>
        /// Adds an item to the queue at given priority.
        /// </summary>
        /// <param name="t">An item to add to the queue.</param>
        /// <param name="priority">An integer specifying priority.</param>
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

        /// <summary>
        /// Returns true if there are no items in the queue.
        /// </summary>
        /// <returns>A boolean.</returns>
        public Boolean IsEmpty()
        {
            return size == 0;
        }

        /// <summary>
        /// Returns the number of items in the queue.
        /// </summary>
        /// <returns>The queue size.</returns>
        public int Size()
        {
            return size;
        }

        /// <summary>
        /// Removes all items from the queue.
        /// </summary>
        public void Clear()
        {
            while (!IsEmpty())
            {
                Dequeue();
            }
        }

        /// <summary>
        /// Returns a human readable representation of the queue.
        /// </summary>
        /// <returns>A human-readable string.</returns>
        public override String ToString()
        {
            String res = "PriorityQueue<";
            foreach (Queue<T> q in queues)
            {
                if (q != null)
                {
                    foreach (T t in q)
                    {
                        res += t + ", ";
                    }
                }
            }
            res += ">";
            return res;
        }
    }
}
