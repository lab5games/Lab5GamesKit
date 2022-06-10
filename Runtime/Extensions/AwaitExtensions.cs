using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Lab5Games
{
    public static class AwaitExtensions
    {
        public static AsyncOperationAwaiter GetAwaiter(this AsyncOperation asyncOp)
        {
            return new AsyncOperationAwaiter(asyncOp);
        }

        public static AsyncOperationHandleAwaiter GetAwaiter(this AsyncOperationHandle handle)
        {
            return new AsyncOperationHandleAwaiter(handle);
        }

        public static AsyncOperationHandleAwaiter<T> GetAwaiter<T>(this AsyncOperationHandle<T> handle)
        {
            return new AsyncOperationHandleAwaiter<T>(handle);
        }

        public class AsyncOperationAwaiter : IAwaiter
        {
            AsyncOperation _asyncOp;

            public AsyncOperationAwaiter(AsyncOperation asyncOp)
            {
                _asyncOp = asyncOp;
            }

            public bool IsCompleted => _asyncOp.isDone;

            public void GetResult()
            {
            }

            public void OnCompleted(Action continuation)
            {
                _asyncOp.completed += x => continuation?.Invoke();
            }
        }

        public class AsyncOperationHandleAwaiter : IAwaiter
        {
            AsyncOperationHandle _handle;

            public AsyncOperationHandleAwaiter(AsyncOperationHandle handle)
            {
                _handle = handle;
            }

            public bool IsCompleted => _handle.IsDone;

            public void GetResult()
            {
            }

            public void OnCompleted(Action continuation)
            {
                _handle.Completed += x => continuation?.Invoke();
            }
        }

        public class AsyncOperationHandleAwaiter<T> : IAwaiter<T>
        {
            AsyncOperationHandle<T> _handle;

            public AsyncOperationHandleAwaiter(AsyncOperationHandle<T> handle)
            {
                _handle = handle;
            }

            public bool IsCompleted => _handle.IsDone;

            public T GetResult() => _handle.Result;

            public void OnCompleted(Action continuation)
            {
                _handle.Completed += x => continuation?.Invoke();
            }
        }
    }
}
