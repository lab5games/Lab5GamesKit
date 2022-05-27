using System;
using UnityEngine;

namespace Lab5Games
{
    public static class AwaitExtensions
    {
        public static AsyncOperationAwaiter GetAwaiter(this AsyncOperation asyncOp)
        {
            return new AsyncOperationAwaiter(asyncOp);
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
    }
}
