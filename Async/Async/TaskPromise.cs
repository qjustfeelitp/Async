namespace Async;

public class TaskPromise
{
    private Task<T> Run<T>(Func<T> func)
    {
        var tcs = new TaskCompletionSource<T>();

        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                tcs.TrySetResult(func());
            }
            catch (Exception e)
            {
                tcs.SetException(e);
            }
        });

        return tcs.Task;
    }
}
