using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DatabaseAnalysis.WPF.InnerLogger
{
    public class Awaiter
    {
        public static readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        public static readonly Dictionary<string, SemaphoreSlim> Semaphores = new Dictionary<string,SemaphoreSlim>();

        public static async Task<T> ResultAsync<T>(string key, Func<Task<T>> task, int count = 1)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                if (!Semaphores.ContainsKey(key))
                    Semaphores.Add(key, new SemaphoreSlim(count, count));
            }
            finally
            {
                semaphoreSlim.Release();
            }
            var semaphore = Semaphores[key];
            await semaphore.WaitAsync();
            try
            {
                return await task();
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static async Task Async(string key, Func<Task> task, int count = 1)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                if (!Semaphores.ContainsKey(key))
                    Semaphores.Add(key, new SemaphoreSlim(count, count));
            }
            finally
            {
                semaphoreSlim.Release();
            }
            var semaphore = Semaphores[key];
            await semaphore.WaitAsync();
            try
            {
                await task();
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
