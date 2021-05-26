
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TelEnvyXmlLib
{
  

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Queue of blockings. </summary>
    ///
    /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
    ///
    /// <typeparam name="T">    Generic type parameter.</typeparam>
    ///-------------------------------------------------------------------------------------------------

    public class BlockingQueue<T>
    {
    
        /// <summary>   The queue. </summary>
        private readonly Queue<T> queue = new Queue<T>();   /* The queue */
        /// <summary>   The maximum size of the. </summary>
        private readonly int maxSize;   /* The maximum size of the  */



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.BlockingQueue&lt;T&gt; class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="maxSize">  The maximum size of the.</param>
        ///-------------------------------------------------------------------------------------------------

        public BlockingQueue(int maxSize) { this.maxSize = maxSize; }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes a new instance of the TelEnvyXmlLib.BlockingQueue&lt;T&gt; class.
        ///             </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public BlockingQueue()
        {
        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds an object onto the end of this queue. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="item"> The item.</param>
        ///-------------------------------------------------------------------------------------------------

        public void Enqueue(T item)
        {
            lock (queue)
            {
                while (queue.Count >= maxSize)
                {
                    Monitor.Wait(queue);
                }
                queue.Enqueue(item);
                if (queue.Count == 1)
                {
                    // wake up any blocked dequeue
                    Monitor.PulseAll(queue);
                }
            }
        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Removes the head object from this queue. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <returns>   The head object from this queue. </returns>
        ///-------------------------------------------------------------------------------------------------

        public T Dequeue()
        {
            lock (queue)
            {
                while (queue.Count == 0)
                {
                    Monitor.Wait(queue);
                }
                T item = queue.Dequeue();
                if (queue.Count == maxSize - 1)
                {
                    // wake up any blocked enqueue
                    Monitor.PulseAll(queue);
                }
                return item;
            }
        }

     

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the number of. </summary>
        ///
        /// <value> The count. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Count
        {
            
            get { return queue.Count; }
            
        }
        
        /// <summary>   True to closing. </summary>
        bool closing;   /* True to closing */

      

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Closes this BlockingQueue&lt;T&gt; </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void Close()
        {
            lock (queue)
            {
                closing = true;
                Monitor.PulseAll(queue);
            }
        }



        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Attempts to dequeue. </summary>
        ///
        /// <remarks>   Timothy Peer, eNVy Systems Inc., 6/26/2019. </remarks>
        ///
        /// <param name="value">    [out] The value.</param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool TryDequeue(out T value)
        {
            lock (queue)
            {
                while (queue.Count == 0)
                {
                    if (closing)
                    {
                        value = default(T);
                        return false;
                    }
                    Monitor.Wait(queue);
                }
                value = queue.Dequeue();
                if (queue.Count == maxSize - 1)
                {
                    // wake up any blocked enqueue
                    Monitor.PulseAll(queue);
                }
                return true;
            }
        }
    }
}
