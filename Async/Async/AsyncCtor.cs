namespace Async;

public class AsyncCtor
{
    public string SomeData { get; private set; }

    public AsyncCtor()
    {
        Construct();
    }

    private async Task Construct()
    {
        await Task.Delay(1_000);
        this.SomeData = "test";
    }
}

public class AsyncCtorIfNeeded
{
    public string SomeData { get; private set; }

    public AsyncCtorIfNeeded(Action executed)
    {
        Construct().ContinueWith(task => executed());
    }

    private async Task Construct()
    {
        await Task.Delay(1_000);
        this.SomeData = "test";
    }
}

public static class AsyncCtorIfNeededExecutor
{
    public static async Task<AsyncCtorIfNeeded> Create()
    {
        var tcs = new TaskCompletionSource();

        var asyncCtorIfNeeded = new AsyncCtorIfNeeded(() => tcs.TrySetResult());

        await tcs.Task;

        return asyncCtorIfNeeded;
    }
}
