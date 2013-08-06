using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;

namespace AGENT.Contrib.Util
{
    public static class ThreadUtil
    {
        #region Data

        /// <summary>
        /// Synchronizes thread queue actions
        /// </summary>
        private static readonly object lockObject = new object();

        /// <summary>
        /// List storing our available threadpool threads
        /// </summary>
        private static readonly ArrayList _availableThreads = new ArrayList();

        /// <summary>
        /// Queue of actions for our threadpool
        /// </summary>
        private static readonly Queue _threadActions = new Queue();

        /// <summary>
        /// Wait handle for us to synchronize de-queuing thread actions
        /// </summary>
        private static readonly ManualResetEvent _threadSynch = new ManualResetEvent(false);

        /// <summary>
        /// Maximum size of our thread pool
        /// </summary>
        private const int MaxThreads = 3;

        #endregion

        #region Thread Start


        /// <summary>
        /// Starts a new thread with an action
        /// </summary>
        /// <param name="start"></param>
        public static void Start(ThreadStart start)
        {
            try
            {
                var t = new Thread(start);
                t.Start();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }

        #endregion

        #region ThreadPooling

        /// <summary>
        /// Queues an action into the threadpool
        /// </summary>
        /// <param name="start"></param>
        public static void SafeQueueWorkItem(ThreadStart start)
        {
            lock (lockObject)
            {
                _threadActions.Enqueue(start);

                // if we haven't spun all the threads up, create a new one
                // and add it to our available threads

                if (_availableThreads.Count < MaxThreads)
                {
                    var t = new Thread(ActionConsumer);
                    _availableThreads.Add(t);
                    t.Start();
                }

                // pulse all waiting threads
                _threadSynch.Set();
            }
        }

        /// <summary>
        /// Main body of a threadpool thread.  Indefinitely wait until
        /// an action is queued. When an action is de-queued safely execute it
        /// </summary>
        private static void ActionConsumer()
        {
            while (true)
            {
                // wait on action pulse
                _threadSynch.WaitOne();

                ThreadStart action = null;

                // try and de-queue an action
                lock (lockObject)
                {
                    if (_threadActions.Count > 0)
                    {
                        action = _threadActions.Dequeue() as ThreadStart;
                    }
                    else
                    {
                        // the queue is empty and we are in a critical section
                        // safely reset the mutex so that everyone waits 
                        // until the next action is queued

                        _threadSynch.Reset();
                    }
                }

                // if we got an action execute it
                if (action != null)
                {
                    try
                    {
                        action();
                    }
                    catch (Exception ex)
                    {
                        Debug.Print("Unhandled error in thread pool: " + ex);
                    }
                }
            }
        }

        #endregion
    }
}
