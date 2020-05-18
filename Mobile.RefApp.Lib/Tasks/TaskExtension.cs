using System;
using System.Threading.Tasks;

/*
 * Based on James Montemangno MvvmHelpers Util class
 */

namespace Mobile.RefApp
{
    public static class Utils
    {
        /// <summary>
        /// Task extension to add a timeout.
        /// </summary>
        /// <returns>The task with timeout.</returns>
        /// <param name="task">Task.</param>
        /// <param name="timeoutInMilliseconds">Timeout duration in Milliseconds.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async static Task<T> WithTimeout<T>(this Task<T> task, int timeoutInMilliseconds)
        {
            var retTask = await Task.WhenAny(task, Task.Delay(timeoutInMilliseconds))
                .ConfigureAwait(false);

            return retTask is Task<T> ? task.Result : default(T);
        }

        /// <summary>
        /// Task extension to add a timeout.
        /// </summary>
        /// <returns>The task with timeout.</returns>
        /// <param name="task">Task.</param>
        /// <param name="timeout">Timeout Duration.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout) =>
            WithTimeout(task, (int)timeout.TotalMilliseconds);
 
    }
}
