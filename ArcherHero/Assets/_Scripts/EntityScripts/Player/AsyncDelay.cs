using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public static class AsyncDelay
{
    public static async UniTaskVoid Delay(TimeSpan time, Action afterStart, Action afterEndTime, Action afterDelayInterruption = null, CancellationToken token = default)
    {
        afterStart?.Invoke();

        if (time.TotalMilliseconds <= 0)
        {
            return;
        }

        if (await UniTask.Delay(time, cancellationToken: token).SuppressCancellationThrow())
        {
            afterDelayInterruption?.Invoke();
            return;
        }

        afterEndTime?.Invoke();
    }
}
